using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ManagerUnitTest
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using NUnit.Framework;
    using PlaceMint.Manager;

    [TestFixture]
    [SuppressMessage("Microsoft.Performance", "CA1812")]
    class TestXmlSerialize : ITest
    {
        private List<TitleList> _expectedMatch = new List<TitleList>();
        private List<Process> _proccesses = new List<Process>();
        private List<IntPtr> _hwnds = new List<IntPtr>();
        private List<string> _groupTitles = new List<string>();
        private const int PROC_COUNT = 3;
        private const int RECT_COUNT = 3;
        private const int X_BASE = 25;
        private const int Y_BASE = 25;
        private const int W_BASE = 400;
        private const int H_BASE = 300;
        private const int POS_INCR = 15;
        private const int SIZE_INCR = 20;

        public void RunTests()
        {
            System.Console.WriteLine("Test XmlSerialize class.");
            Init();
            testXmlSerialize();
            System.Console.WriteLine("    xml serialize Success.");
            Fini();
            System.Console.WriteLine("--- XmlSerialize Success.");
        }

        [SetUp]
        private void Init()
        {
            TitleList regList = new TitleList();
            regList.Add(new TitleRegex("Find Nothing", "^--Find--Nothing--$", false));
            _expectedMatch.Add(regList);
            regList = new TitleList();
            regList.Add(new TitleRegex("Notepad", "Notepad", false));
            _expectedMatch.Add(regList);
            for (int i = 0; i < PROC_COUNT; i++)
            {
                Process proc = Process.Start("notepad", String.Format(CultureInfo.InvariantCulture, "text{0}.txt", i));
                proc.WaitForInputIdle();
                _proccesses.Add(proc);
                _hwnds.Add(Program.NativeMethods.FindWindow(null, proc.MainWindowTitle));
            }
            _groupTitles.Add("Find Nothing");
            _groupTitles.Add("Notepad");
        }
        [TearDown]
        private void Fini()
        {
            foreach (Process proc in _proccesses)
            {
                if (!proc.HasExited)
                {
                    proc.Kill();
                }
                proc.Dispose();
            }
        }

        [Test]
        private void testXmlSerialize()
        {
            string filename = "WindowGroups_UnitTest.config";
            List<RectangleWrap> rects = new List<RectangleWrap>(RECT_COUNT);
            for (int i = 0; i < RECT_COUNT; i++)
            {
                rects.Add(new RectangleWrap(X_BASE + (i * POS_INCR), Y_BASE + (i * POS_INCR), W_BASE + (i * SIZE_INCR), H_BASE + (i * SIZE_INCR))); 
            }
            SlotList slots = new SlotList(RECT_COUNT);
            for (int i = 0; i < RECT_COUNT; i++)
            {
                slots.Add(new Slot(rects[i], new Hotkey(), 1));
            }
            GroupConfiguration config = new GroupConfiguration();
            WindowGroup wg = new WindowGroup(_expectedMatch[0], new ClassList(), slots, _groupTitles[0]);
            XmlReadWrite<WindowGroup>.Save(wg, filename, new Type[] {typeof(ClassRegex), typeof(TitleRegex)});
            WindowGroup wg2 = XmlReadWrite<WindowGroup>.Load(filename, new Type[] { typeof(ClassRegex), typeof(TitleRegex) });
            Assert.AreEqual(wg, wg2);
            Slot s = new Slot(rects[0], new Hotkey(), 1);
            s.Hotkey = new Hotkey(ModifyingKeys.Alt | ModifyingKeys.Shift, System.Windows.Forms.Keys.F12);
            XmlReadWrite<Slot>.Save(s, filename);
            Slot s2 = XmlReadWrite<Slot>.Load(filename);
            Assert.AreEqual(s, s2);
            wg.Slots.Add(s);
            config.Groups.Add(wg);
            config.Groups.Add(new WindowGroup(_expectedMatch[1], new ClassList(), slots, _groupTitles[1]));
            GroupConfiguration.Save(config, filename);
            GroupConfiguration config2;
            GroupConfiguration.Load(filename, out config2);
            Assert.AreEqual(config, config2);

            SlotTemplateList slotTempList = new SlotTemplateList(2);
            for (int i = 0; i < _groupTitles.Count; i++)
            {
                SlotTemplate sTemp = new SlotTemplate();
                sTemp.Title = _groupTitles[i];
                sTemp.Height = H_BASE + ((i + 1) * 200);
                sTemp.Height = W_BASE + ((i + 1) * 250);
                slotTempList.Add(sTemp);
            }
            filename = "SlotTemplateList_UnitTest.config";
            XmlReadWrite<SlotTemplateList>.Save(slotTempList, filename);
            SlotTemplateList slotTempList2 = XmlReadWrite<SlotTemplateList>.Load(filename);
            Assert.AreEqual(slotTempList, slotTempList2);

        }
    }
}
