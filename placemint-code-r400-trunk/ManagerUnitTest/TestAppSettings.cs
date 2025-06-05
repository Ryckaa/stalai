using System.Collections.Generic;

namespace ManagerUnitTest
{
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;
    using PlaceMint.Manager;

    [SuppressMessage("Microsoft.Performance", "CA1812")]
    class TestAppSettings : ITest
    {
        private List<Dictionary<string, string>> _expectedString = new List<Dictionary<string, string>>();
        private List<Dictionary<string, bool>> _expectedBool = new List<Dictionary<string, bool>>();
        private List<RectangleWrap> _expectedWindowRect = new List<RectangleWrap>();
        private List<int> _expectedUpdateFrequency = new List<int>();
        private List<LoggingLevel> _expectedLoggingLevel = new List<LoggingLevel>();

        public void RunTests()
        {
            System.Console.WriteLine("Test GroupConfiguration class.");
            Init();
            testDeepCopy();
            System.Console.WriteLine("    deep copy Success.");
            Fini();
            System.Console.WriteLine("--- GroupConfiguration Success.");

        }

        [SetUp]
        void Init()
        {
            //set 1
            _expectedBool.Add(new Dictionary<string,bool>(2));
            _expectedBool[0].Add("MinToTray", true);
            _expectedBool[0].Add("DragDropSwap", true);
            _expectedString.Add(new Dictionary<string, string>(2));
            _expectedString[0].Add("ConfigFileName", "somthing.txt");
            _expectedWindowRect.Add(new RectangleWrap(20, 25, 30, 45));
            _expectedLoggingLevel.Add(LoggingLevel.Fatal);
            _expectedUpdateFrequency.Add(100);

            //set 2
            _expectedBool.Add(new Dictionary<string, bool>(2));
            _expectedBool[1].Add("MinToTray", false);
            _expectedBool[1].Add("DragDropSwap", false);
            _expectedString.Add(new Dictionary<string, string>(2));
            _expectedString[1].Add("ConfigFileName", "somthingELSE.txt");
            _expectedWindowRect.Add(new RectangleWrap(50, 55, 60, 65));
            _expectedLoggingLevel.Add(LoggingLevel.Debug);
            _expectedUpdateFrequency.Add(1200);
        }

        [TearDown]
        [SuppressMessage("Microsoft.Performance", "CA1822")]
        void Fini() { }

        [Test]
        void testDeepCopy()
        {
            AppSettings appS1 = new AppSettings();
            appS1.ConfigFileName = _expectedString[0]["ConfigFileName"];
            appS1.DragDropSwap = _expectedBool[0]["DragDropSwap"];
            appS1.LogLevel = _expectedLoggingLevel[0];
            appS1.MinToTray = _expectedBool[0]["MinToTray"];
            appS1.UpdateFrequency = _expectedUpdateFrequency[0];
            appS1.WindowRect = _expectedWindowRect[0];

            AppSettings appS2 = appS1.DeepClone();
            appS2.ConfigFileName = _expectedString[1]["ConfigFileName"];
            appS2.DragDropSwap = _expectedBool[1]["DragDropSwap"];
            appS2.LogLevel = _expectedLoggingLevel[1];
            appS2.MinToTray = _expectedBool[1]["MinToTray"];
            appS2.UpdateFrequency = _expectedUpdateFrequency[1];
            appS2.WindowRect = _expectedWindowRect[1];

            Assert.AreNotEqual(appS1.ConfigFileName, appS2.ConfigFileName);
            Assert.AreNotEqual(appS1.DragDropSwap, appS2.DragDropSwap);
            Assert.AreNotEqual(appS1.LogLevel, appS2.LogLevel);
            Assert.AreNotEqual(appS1.MinToTray, appS2.MinToTray);
            Assert.AreNotEqual(appS1.UpdateFrequency, appS2.UpdateFrequency);
            Assert.AreNotEqual(appS1.WindowRect, appS2.WindowRect);
            appS1.WindowRect = _expectedWindowRect[0];
        }
    }
}
