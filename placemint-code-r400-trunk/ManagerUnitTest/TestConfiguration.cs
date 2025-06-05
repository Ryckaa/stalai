using System.Collections.Generic;

namespace ManagerUnitTest
{
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;
    using PlaceMint.Manager;

    [TestFixture]
    [SuppressMessage("Microsoft.Performance", "CA1812")]
    class TestConfiguration : ITest
    {
        private List<TitleList> _expectedMatch = new List<TitleList>();
        private List<Dictionary<string, bool>> _expectedBoool = new List<Dictionary<string, bool>>();
        private List<RectangleWrap> _rects = new List<RectangleWrap>();
        private SlotList _slots = new SlotList();
        private List<string> _groupTitles = new List<string>();
        private const int PROC_COUNT = 2;
        private const int RECT_COUNT = 3;
        private const int RECT_LIST_COUNT = 5;
        private const int X_BASE = 25;
        private const int Y_BASE = 25;
        private const int W_BASE = 400;
        private const int H_BASE = 300;
        private const int POS_INCR = 15;
        private const int SIZE_INCR = 20;

        public void RunTests()
        {
            System.Console.WriteLine("Test GroupConfiguration class.");
            Init();
            testConstruction();
            System.Console.WriteLine("    construction Success.");
            testDeepCopy();
            System.Console.WriteLine("    deep copy Success.");
            Fini();
            System.Console.WriteLine("--- GroupConfiguration Success.");
        }

        [SetUp]
        private void Init()
        {
            TitleList regList = new TitleList();
            regList.Add(new TitleRegex("Find Nothing", "^--Find--Nothing--$", false));
            _expectedMatch.Add(regList);
            for (int i = 0; i < RECT_COUNT; i++)
            {
                _rects.Add(new RectangleWrap(X_BASE + (i * POS_INCR), Y_BASE + (i * POS_INCR), W_BASE + (i * SIZE_INCR), H_BASE + (i * SIZE_INCR)));
                _slots.Add(new Slot(_rects[i], new Hotkey(), 1));
            }
            _groupTitles.Add("Title1");

            _expectedBoool.Add(new Dictionary<string, bool>(4));
            _expectedBoool[0].Add("move", true);
            _expectedBoool[0].Add("size", true);
            _expectedBoool[0].Add("min", true);
            _expectedBoool[0].Add("max", true);
        }

        [TearDown]
        [SuppressMessage("Microsoft.Performance", "CA1822")]
        private void Fini()
        {
        }

        [Test]
        static private void testConstruction()
        {
            GroupConfiguration config = new GroupConfiguration();
            Assert.AreEqual(0, config.Groups.Count);
        }

        [Test]
        private void testDeepCopy()
        {
            GroupConfiguration config1 = new GroupConfiguration();
            config1.Groups.Add(new WindowGroup(_expectedMatch[0], new ClassList(), _slots, _groupTitles[0],
                _expectedBoool[0]["move"], _expectedBoool[0]["size"], _expectedBoool[0]["min"], _expectedBoool[0]["max"]));
            GroupConfiguration config2 = config1.DeepClone();
            Assert.AreEqual(config1, config2);
            config2.Groups.Clear();
            Assert.AreNotEqual(config1.Groups, config2.Groups);
        }
    }
}
