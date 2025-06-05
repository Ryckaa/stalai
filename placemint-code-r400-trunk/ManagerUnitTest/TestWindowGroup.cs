using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ManagerUnitTest
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using NUnit.Framework;
    using PlaceMint.Manager;
    using PlaceMint.Manager.PMException;

    [TestFixture]
    [SuppressMessage("Microsoft.Performance", "CA1812")]
    public class TestWindowGroup : ITest
    {
        private List<TitleList> _expectedMatch = new List<TitleList>();
        private List<Dictionary<string, bool>> _expectedBoool = new List<Dictionary<string, bool>>();
        private List<Process> _proccesses = new List<Process>();
        private List<List<IntPtr>> _hwnds = new List<List<IntPtr>>();
        private List<List<RectangleWrap>> _rects = new List<List<RectangleWrap>>();
        private List<string> _groupTitles = new List<string>();
        private List<SlotList> _slots = new List<SlotList>();
        private const int PROC_COUNT = 3;
        private const int RECT_COUNT = 4;
        private const int RECT_LIST_COUNT = 5;
        private const int X_BASE = 25;
        private const int Y_BASE = 25;
        private const int W_BASE = 400;
        private const int H_BASE = 300;
        private const int POS_INCR = 15;
        private const int SIZE_INCR = 20;

        public void RunTests()
        {
            System.Console.WriteLine("Test WindowGroup class.");
            Init();
            testConstruction();
            System.Console.WriteLine("    construction Success.");
            testSettings();
            System.Console.WriteLine("    settings Success.");
            testDeepClone();
            System.Console.WriteLine("    deepclone Success.");
            testForeach();
            System.Console.WriteLine("    foreach Success.");
            testRefresh();
            System.Console.WriteLine("    refresh Success.");
            testRippleForward();
            System.Console.WriteLine("    ripple forward Success.");
            testStackSwapping();
            System.Console.WriteLine("    stack swap Success.");
            Fini();
            System.Console.WriteLine("--- WindowGroup Success.");
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
            for (int i = 0; i < RECT_LIST_COUNT; i++)
            {
                _rects.Add(new List<RectangleWrap>(RECT_COUNT));
                _slots.Add(new SlotList(RECT_COUNT));
            }
            for (int i = 0; i < RECT_COUNT; i++)
            {
                _rects[0].Add(new RectangleWrap(X_BASE + (i * POS_INCR), Y_BASE + (i * POS_INCR), W_BASE + (i * SIZE_INCR), H_BASE + (i * SIZE_INCR)));
                _rects[1].Add(new RectangleWrap(X_BASE + 1 + (i * POS_INCR), Y_BASE + (i * POS_INCR), W_BASE + (i * SIZE_INCR), H_BASE + (i * SIZE_INCR)));
                _rects[2].Add(new RectangleWrap(X_BASE + (i * POS_INCR), Y_BASE + 1 + (i * POS_INCR), W_BASE + (i * SIZE_INCR), H_BASE + (i * SIZE_INCR)));
                _rects[3].Add(new RectangleWrap(X_BASE + (i * POS_INCR), Y_BASE + (i * POS_INCR), W_BASE + 1 + (i * SIZE_INCR), H_BASE + (i * SIZE_INCR)));
                _rects[4].Add(new RectangleWrap(X_BASE + (i * POS_INCR), Y_BASE + (i * POS_INCR), W_BASE + (i * SIZE_INCR), H_BASE + 1 + (i * SIZE_INCR)));
            }
            for (int i = 0; i < RECT_LIST_COUNT; i++)
            {
                for (int j = 0; j < RECT_COUNT; j++)
                {
                    _slots[i].Add(new Slot(_rects[i][j], new Hotkey(), 1));
                }
            }

            _groupTitles.Add("Title1");
            _groupTitles.Add("Title2");

            _expectedBoool.Add(new Dictionary<string, bool>(4));
            _expectedBoool[0].Add("move", true);
            _expectedBoool[0].Add("size", true);
            _expectedBoool[0].Add("min", true);
            _expectedBoool[0].Add("max", true);
            _expectedBoool.Add(new Dictionary<string, bool>(4));
            _expectedBoool[1].Add("move", false);
            _expectedBoool[1].Add("size", false);
            _expectedBoool[1].Add("min", false);
            _expectedBoool[1].Add("max", false);
            _expectedBoool.Add(new Dictionary<string, bool>(4));
            _expectedBoool[2].Add("move", true);
            _expectedBoool[2].Add("size", false);
            _expectedBoool[2].Add("min", false);
            _expectedBoool[2].Add("max", false);
            _expectedBoool.Add(new Dictionary<string, bool>(4));
            _expectedBoool[3].Add("move", false);
            _expectedBoool[3].Add("size", true);
            _expectedBoool[3].Add("min", false);
            _expectedBoool[3].Add("max", false);
        }

        [TearDown]
        [SuppressMessage("Microsoft.Performance", "CA1822")]
        private void Fini() { }

        [Test]
        private void testConstruction()
        {
            WindowGroup wg = new WindowGroup(_expectedMatch[0], new ClassList(), _slots[0], _groupTitles[0]);
            Assert.AreEqual(RECT_COUNT, wg.SlotsCount);
            for (int i = 0; i < RECT_COUNT; i++)
            {
                Assert.IsTrue(wg.Slots[i].Equals(_slots[0][i]));
            }
            Assert.AreEqual(_groupTitles[0], wg.WindowGroupTitle);
            Assert.AreEqual(0, wg.OccupiedCount);
        }

        private void testDeepClone()
        {
            WindowGroup group1 = new WindowGroup(_expectedMatch[0], new ClassList(), _slots[0], _groupTitles[0], _expectedBoool[0]["move"],
                _expectedBoool[0]["size"], _expectedBoool[0]["min"], _expectedBoool[0]["max"]);
            WindowGroup group2 = group1.DeepClone();
            Assert.AreEqual(group1, group2);
            group2.WinTitleRegexList = _expectedMatch[1];
            group2.Slots.Clear();
            group2.WindowGroupTitle = _groupTitles[1];
            group2.Movable = _expectedBoool[1]["move"];
            group2.Sizable = _expectedBoool[1]["size"];
            group2.Minable = _expectedBoool[1]["min"];
            Assert.AreNotEqual(group1.WinTitleRegexList, group2.WinTitleRegexList);
            Assert.AreNotEqual(group1.Slots, group2.Slots);
            Assert.AreNotEqual(group1.WindowGroupTitle, group2.WindowGroupTitle);
            Assert.AreNotEqual(group1.Movable, group2.Movable);
            Assert.AreNotEqual(group1.Sizable, group2.Sizable);
            Assert.AreNotEqual(group1.Minable, group2.Minable);
        }

        private void testSettings()
        {
            WindowGroup wg;
            for (int i = 0; i < _expectedBoool.Count; i++)
            {
                wg = new WindowGroup(_expectedMatch[0], new ClassList(), _slots[0], _groupTitles[0], _expectedBoool[i]["move"],
                     _expectedBoool[i]["size"], _expectedBoool[i]["min"], _expectedBoool[i]["max"]);
                Assert.AreEqual(_expectedBoool[i]["move"], wg.Movable);
                Assert.AreEqual(_expectedBoool[i]["size"], wg.Sizable);
                Assert.AreEqual(_expectedBoool[i]["min"], wg.Minable);
            }
        }

        [Test]
        private void testForeach()
        {
            WindowGroup wg = new WindowGroup(_expectedMatch[0], new ClassList(), _slots[0], _groupTitles[0], _expectedBoool[0]["move"],
                     _expectedBoool[0]["size"], _expectedBoool[0]["min"], _expectedBoool[0]["max"]);
            WindowGroup wg1 = new WindowGroup(_expectedMatch[0], new ClassList(), _slots[0], _groupTitles[0], _expectedBoool[0]["move"],
                     _expectedBoool[0]["size"], _expectedBoool[0]["min"], _expectedBoool[0]["max"]);
            int i = 0;
            //Clear Slots that don't have windows anymore
            wg1.IteratorReset();
            while (wg1.IteratorHasMore())
            {
                wg1.IteratorCurrent = new Slot(_rects[1][i], new Hotkey(), 1);
                i++;
                wg1.IteratorNext();
            }
            for (int j = 0; j < wg.Slots.Count; j++)
            {
                Assert.AreNotEqual(wg.Slots[j], wg1.Slots[j]);
            }
        }

        [Test]
        private void testRefresh()
        {
            windowSetup();

            WindowGroup wg = new WindowGroup(_expectedMatch[1], new ClassList(), _slots[0], _groupTitles[0]);
            GroupConfiguration config = new GroupConfiguration();
            wg.Parent = config;
            AppSettings appSetting = new AppSettings();
            wg.Refresh(appSetting);
            //It can not be garunteed that the notepad windows will be found
            //in the same order in which they were created, so itterate over both lists 
            //for matching hWNDs and assert compareCount
            int compareCount = 0;
            for (int i = 0; i < PROC_COUNT; i++) //itterate hWNDs
            {
                for (int j = 0; j < RECT_COUNT; j++) //itterate Slots
                {
                    if (!wg.Slots[j].IsEmpty && _hwnds[i][0].Equals(wg.Slots[j].Hwnds[0]))
                    {
                        compareCount++;
                        Slot tempSlot = new Slot(_rects[0][j], new Hotkey(), 1, _hwnds[i]);
                        Assert.IsTrue(wg.Slots[j].Equals(tempSlot));
                    }
                }
            }
            Assert.AreEqual(PROC_COUNT, compareCount);
            Assert.AreEqual(RECT_COUNT, wg.SlotsCount);
            Assert.AreEqual(PROC_COUNT, wg.OccupiedCount);

            windowTearDown();
        }

        /*
         * NOTE: Equals removed because two identical calls to constructor won't necessarily 
         * produce the same WindowGroup.
        [Test]
        public void testEquals()
        {
            WindowGroup wg1 = new WindowGroup(_expectedMatch[0], _rects[0], _groupTitles[0]);
            WindowGroup wg2 = new WindowGroup(_expectedMatch[0], _rects[0], _groupTitles[0]);
            WindowGroup wg3 = new WindowGroup(_expectedMatch[0], _rects[0], _groupTitles[0]);

            Assert.IsFalse(wg1.Equals(null));
            Assert.IsFalse(wg1.Equals(1));
            Assert.IsTrue(wg1.Equals(wg1));
            Assert.IsTrue(wg1.winTitleMatch.ToString().Equals(wg2.winTitleMatch.ToString()));
            Assert.IsTrue(wg1.Equals(wg2));
            Assert.IsTrue(wg2.Equals(wg1));
            Assert.IsTrue(wg1.Equals(wg3));
            Assert.IsTrue(wg2.Equals(wg3));

            wg2 = new WindowGroup(_expectedMatch[1], _rects[0], _groupTitles[0]);
            Assert.IsFalse(wg1.Equals(wg2));

            wg2 = new WindowGroup(_expectedMatch[0], _rects[1], _groupTitles[0]);
            Assert.IsFalse(wg1.Equals(wg2));

            wg2 = new WindowGroup(_expectedMatch[0], _rects[2], _groupTitles[0]);
            Assert.IsFalse(wg1.Equals(wg2));

            wg2 = new WindowGroup(_expectedMatch[0], _rects[3], _groupTitles[0]);
            Assert.IsFalse(wg1.Equals(wg2));

            wg2 = new WindowGroup(_expectedMatch[0], _rects[4], _groupTitles[0]);
            Assert.IsFalse(wg1.Equals(wg2));

            wg2 = new WindowGroup(_expectedMatch[0], _rects[0], _groupTitles[1]);
            Assert.IsFalse(wg1.Equals(wg2));
        }
        */

        [Test]
        private void testRippleForward()
        {
            windowSetup();

            WindowGroup wg = new WindowGroup(_expectedMatch[1], new ClassList(), _slots[0], _groupTitles[0]);
            GroupConfiguration config = new GroupConfiguration();
            wg.Parent = config;
            config.Groups.Add(wg);
            AppSettings appSetting = new AppSettings();
            wg.Refresh(appSetting);

            Assert.Greater(wg.Slots.Count, PROC_COUNT);
            //Test: move first window to last slot
            List<IntPtr> originalOrder = new List<IntPtr>(wg.Slots.Count);
            for (int i = 0; i < wg.Slots.Count; i++)
            {
                if (i < PROC_COUNT)
                {
                    Assert.IsFalse(wg.Slots[i].IsEmpty);
                    originalOrder.Add(wg.Slots[i].Hwnds[0]);
                }
                else
                {
                    Assert.IsTrue(wg.Slots[i].IsEmpty);
                    originalOrder.Add(new IntPtr(0));
                }
            }
            List<IntPtr> expectedOrder = new List<IntPtr>(wg.Slots.Count);
            for (int i = 0; i < wg.Slots.Count; i++)
            {
                if (i != wg.Slots.Count - 1)
                {
                    if (!originalOrder[i].Equals(new IntPtr(0)) &&
                        originalOrder[i + 1].Equals(new IntPtr(0)))
                    {
                        expectedOrder.Add(originalOrder[0]);
                    }
                    else
                    {
                        expectedOrder.Add(originalOrder[i + 1]);
                    }
                }
                else
                {
                    expectedOrder.Add(new IntPtr(0));
                }
            }
            wg.SwapHwnds(0, 0, wg.Slots.Count - 1);
            Slot.MoveAndSizeWin(wg.Slots[wg.Slots.Count - 1].Hwnds[0], wg.Slots[wg.Slots.Count - 1].Shape,
                wg.Movable, wg.Sizable, false);
            Assert.IsTrue(wg.Slots[0].IsEmpty);
            Assert.IsFalse(wg.Slots[wg.Slots.Count - 1].IsEmpty);
            appSetting.RippleForward = true;
            wg.Refresh(appSetting);
            originalOrder.Clear();
            for (int i = 0; i < wg.Slots.Count; i++)
            {
                if (i < PROC_COUNT)
                {
                    Assert.IsFalse(wg.Slots[i].IsEmpty);
                    Assert.AreEqual(expectedOrder[i], wg.Slots[i].Hwnds[0]);
                    originalOrder.Add(wg.Slots[i].Hwnds[0]);
                }
                else
                {
                    Assert.IsTrue(wg.Slots[i].IsEmpty);
                    Assert.AreEqual(expectedOrder[i], new IntPtr(0));
                    originalOrder.Add(new IntPtr(0));
                }
            }

            windowTearDown();
        }

        [Test]
        private void testStackSwapping()
        {
            Assert.Greater(_rects[0].Count, PROC_COUNT);
            SlotList sslots = new SlotList();
            AppSettings appSetting = new AppSettings();
            GroupConfiguration config = new GroupConfiguration();

            //single->empty
            windowSetup(1);
            for (int i = 0; i < 2; i++)
                sslots.Add(new Slot(_rects[0][i], new Hotkey(), 1));

            WindowGroup wg = new WindowGroup(_expectedMatch[1], new ClassList(), sslots, _groupTitles[0]);
            wg.Parent = config;
            config.Groups.Add(wg);
            wg.Refresh(appSetting);

            IntPtr singleHwnd0 = wg.Slots[0].Hwnds[0];
            Assert.IsTrue(wg.Slots[1].IsEmpty);
            Assert.IsTrue(wg.SwapHwnds(0, 0, 1));
            Assert.IsTrue(wg.Slots[0].IsEmpty);
            Assert.AreEqual(singleHwnd0, wg.Slots[1].Hwnds[0]);

            windowTearDown();
            wg.Refresh(appSetting);
            foreach (Slot s in wg.Slots)
                Assert.IsTrue(s.IsEmpty);

            //single->single
            windowSetup();
            wg.Refresh(appSetting);

            singleHwnd0 = wg.Slots[0].Hwnds[0];
            IntPtr singleHwnd1 = wg.Slots[1].Hwnds[0];
            Assert.IsTrue(wg.SwapHwnds(0, 0, 1));
            Assert.AreEqual(singleHwnd1, wg.Slots[0].Hwnds[0]);
            Assert.AreEqual(singleHwnd0, wg.Slots[1].Hwnds[0]);

            windowTearDown();

            //single->empty stack
            sslots = new SlotList();
            for (int i = 0; i < PROC_COUNT; i++)
                sslots.Add(new Slot(_rects[0][i], new Hotkey(), 1 + i));
            wg = new WindowGroup(_expectedMatch[1], new ClassList(), sslots, _groupTitles[0]);
            wg.Parent = config;
            config.Groups.Add(wg);
            windowSetup();
            wg.Refresh(appSetting);

            IntPtr singleHwnd = wg.Slots[0].Hwnds[0];
            List<IntPtr> stackHwnds = new List<IntPtr>();
            foreach (IntPtr hwnd in wg.Slots[2].Hwnds)
                stackHwnds.Add(new IntPtr(hwnd.ToInt32()));
            int singleCount = wg.Slots[0].Count;
            int stackCount = wg.Slots[2].Count;
            Assert.IsTrue(wg.SwapHwnds(0, 0, 2));
            Assert.AreEqual(1, wg.Slots[0].Hwnds.RemoveAll(Slot.MatchEmpty));
            Assert.IsTrue(wg.Slots[0].IsEmpty);
            for (int i = 0; i < stackHwnds.Count; i++)
                Assert.IsTrue(wg.Slots[2].Hwnds.Contains(stackHwnds[i]));
            Assert.IsTrue(wg.Slots[2].Hwnds.Contains(singleHwnd));
            Assert.AreEqual(singleCount - 1, wg.Slots[0].Count);
            Assert.AreEqual(stackCount + 1, wg.Slots[2].Count);

            windowTearDown();

            //single->full stack
            sslots = new SlotList();
            for (int i = 0; i < PROC_COUNT; i++)
                sslots.Add(new Slot(_rects[0][i], new Hotkey(), 1 + i));
            wg = new WindowGroup(_expectedMatch[1], new ClassList(), sslots, _groupTitles[0]);
            wg.Parent = config;
            config.Groups.Add(wg);
            windowSetup();
            wg.Refresh(appSetting);

            singleHwnd = wg.Slots[0].Hwnds[0];
            stackHwnds = new List<IntPtr>();
            foreach (IntPtr hwnd in wg.Slots[1].Hwnds)
                stackHwnds.Add(new IntPtr(hwnd.ToInt32()));
            singleCount = wg.Slots[0].Count;
            stackCount = wg.Slots[1].Count;
            Assert.IsFalse(wg.SwapHwnds(0, 0, 1));
            Assert.AreEqual(singleHwnd, wg.Slots[0].Hwnds[0]);
            for (int i = 0; i < stackHwnds.Count; i++)
                Assert.IsTrue(wg.Slots[1].Hwnds.Contains(stackHwnds[i]));
            Assert.AreEqual(singleCount, wg.Slots[0].Count);
            Assert.AreEqual(stackCount, wg.Slots[1].Count);

            windowTearDown();

            //single->non-full/non-empty stack
            sslots = new SlotList();
            for (int i = 0; i < PROC_COUNT; i++)
                sslots.Add(new Slot(_rects[0][i], new Hotkey(), 1 + i));
            wg = new WindowGroup(_expectedMatch[1], new ClassList(), sslots, _groupTitles[0]);
            wg.Parent = config;
            config.Groups.Add(wg);
            windowSetup(2);
            wg.Refresh(appSetting);

            singleHwnd = wg.Slots[0].Hwnds[0];
            stackHwnds = new List<IntPtr>();
            foreach (IntPtr hwnd in wg.Slots[2].Hwnds)
                stackHwnds.Add(new IntPtr(hwnd.ToInt32()));
            singleCount = wg.Slots[0].Count;
            stackCount = wg.Slots[1].Count;
            Assert.IsTrue(wg.SwapHwnds(0, 0, 1));
            Assert.AreEqual(1, wg.Slots[0].Hwnds.RemoveAll(Slot.MatchEmpty));
            Assert.IsTrue(wg.Slots[0].IsEmpty);
            for (int i = 0; i < stackHwnds.Count; i++)
                Assert.IsTrue(wg.Slots[1].Hwnds.Contains(stackHwnds[i]));
            Assert.IsTrue(wg.Slots[1].Hwnds.Contains(singleHwnd));
            Assert.AreEqual(singleCount - 1, wg.Slots[0].Count);
            Assert.AreEqual(stackCount + 1, wg.Slots[1].Count);

            windowTearDown();
        }

        private void windowSetup()
        {
            windowSetup(PROC_COUNT);
        }

        private void windowSetup(int count)
        {
            if (count > PROC_COUNT)
                throw new IllegalInitializationException("attempt to start too many procedures");
            _proccesses = new List<Process>();
            _hwnds = new List<List<IntPtr>>();
            for (int i = 0; i < count; i++)
            {
                Process proc = Process.Start("notepad", String.Format(CultureInfo.InvariantCulture, "text{0}.txt", i));
                proc.WaitForInputIdle();
                _proccesses.Add(proc);
                _hwnds.Add(new List<IntPtr>(1));
                _hwnds[i].Add(Program.NativeMethods.FindWindow(null, proc.MainWindowTitle));
            }
        }

        private void windowTearDown()
        {
            foreach (Process proc in _proccesses)
            {
                if (!proc.HasExited)
                {
                    proc.Kill();
                }
                proc.Dispose();
            }
            _proccesses.Clear();
        }
    }
}