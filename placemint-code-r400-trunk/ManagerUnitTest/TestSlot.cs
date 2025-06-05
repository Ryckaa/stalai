using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ManagerUnitTest
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using NUnit.Framework;
    using PlaceMint.Manager;
    using PlaceMint.Manager.PMException;

    [TestFixture]
    [SuppressMessage("Microsoft.Performance", "CA1812")]
    public class TestSlot : ITest
    {
        private List<Dictionary<string, int>> _expected = new List<Dictionary<string, int>>();
        private List<RectangleWrap> _expectedRect = new List<RectangleWrap>();
        private List<Dictionary<string, bool>> _expectedBoool = new List<Dictionary<string, bool>>();
        private List<IntPtr> _expectedHWND = new List<IntPtr>();
        private List<Hotkey> _expectedHotkey = new List<Hotkey>();
        private List<int> _expectedSize = new List<int>();
        private List<Process> _proccesses = new List<Process>();
        private List<Dictionary<string, int>> _winStart = new List<Dictionary<string, int>>();
        private Dictionary<string, int> _winNow = new Dictionary<string, int>();
        private List<IntPtr> _winHWNDs = new List<IntPtr>();
        private WindowsApi.RECT _winRect;
        private const int PROC_COUNT = 3;

        public void RunTests()
        {
            System.Console.WriteLine("Test Slot class.");
            Init();
            testConstruction();
            System.Console.WriteLine("    construction Success.");
            testEquals();
            System.Console.WriteLine("    equals Success.");
            testDeepClone();
            System.Console.WriteLine("    deepclone Success.");
            testMoveAndSize();
            System.Console.WriteLine("    move/size Success.");
            testMinAndRestore();
            System.Console.WriteLine("    min/restore Success.");
            testHotkey();
            System.Console.WriteLine("    hotkey Success.");
            Fini();
            System.Console.WriteLine("--- Slot Success.");
        }

        [SetUp]
        private void Init()
        {
            //First data set
            _expected.Add(new Dictionary<string, int>(4));
            _expectedRect.Add(new RectangleWrap(0, 0, 153, 163));
            _expected[0].Add("x", 0);
            _expected[0].Add("y", 0);
            _expected[0].Add("w", 153);
            _expected[0].Add("h", 163);
            _expectedHWND.Add((IntPtr)0);
            _expectedBoool.Add(new Dictionary<string, bool>(3));
            _expectedBoool[0].Add("move", true);
            _expectedBoool[0].Add("size", true);
            _expectedBoool[0].Add("min", true);
            _expectedHotkey.Add(new Hotkey(ModifyingKeys.None, Keys.None));
            _expectedSize.Add(0);

            //Second data set
            _expected.Add(new Dictionary<string, int>(4));
            _expectedRect.Add(new RectangleWrap(50, 150, 350, 250));
            _expected[1].Add("x", 50);
            _expected[1].Add("y", 150);
            _expected[1].Add("w", 350);
            _expected[1].Add("h", 250);
            _expectedHWND.Add((IntPtr)1234);
            _expectedBoool.Add(new Dictionary<string, bool>(3));
            _expectedBoool[1].Add("move", false);
            _expectedBoool[1].Add("size", false);
            _expectedBoool[1].Add("min", false);
            _expectedHotkey.Add(new Hotkey(ModifyingKeys.Alt, Keys.F11));
            _expectedSize.Add(1);

            //Third data set
            _expected.Add(new Dictionary<string, int>(4));
            _expectedRect.Add(new RectangleWrap(150, 250, 450, 350));
            _expected[2].Add("x", 150);
            _expected[2].Add("y", 250);
            _expected[2].Add("w", 450);
            _expected[2].Add("h", 350);
            _expectedHWND.Add((IntPtr)2341);
            _expectedBoool.Add(new Dictionary<string, bool>(3));
            _expectedBoool[2].Add("move", true);
            _expectedBoool[2].Add("size", false);
            _expectedBoool[2].Add("min", false);
            _expectedHotkey.Add(new Hotkey(ModifyingKeys.Alt | ModifyingKeys.Shift, Keys.F12));
            _expectedSize.Add(2);

            //Fourth data set
            _expected.Add(new Dictionary<string, int>(4));
            _expectedRect.Add(new RectangleWrap(250, 350, 550, 650));
            _expected[3].Add("x", 250);
            _expected[3].Add("y", 350);
            _expected[3].Add("w", 550);
            _expected[3].Add("h", 650);
            _expectedHWND.Add((IntPtr)3412);
            _expectedBoool.Add(new Dictionary<string, bool>(3));
            _expectedBoool[3].Add("move", false);
            _expectedBoool[3].Add("size", true);
            _expectedBoool[3].Add("min", false);
            _expectedHotkey.Add(new Hotkey(ModifyingKeys.Alt, Keys.F11));
            _expectedSize.Add(3);
        }

        [TearDown]
        [SuppressMessage("Microsoft.Performance", "CA1822")]
        private void Fini() { }

        [Test]
        private void testConstruction()
        {
            // Assure that EMPTY is defined to IntPtr.Zero
            Assert.AreEqual(Slot.EMPTY, IntPtr.Zero);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Slot emptySlot = new Slot(_expectedRect[i], new Hotkey(), _expectedSize[i]);
                    if (_expectedSize[i] < 1)
                    {
                        Assert.Fail("Constructing an Slot with a size less than 1 should throw an exception.");
                    }
                    else
                    {
                        Assert.IsTrue(emptySlot.IsEmpty);
                        Assert.AreEqual(0, emptySlot.Count);
                        Assert.IsTrue(emptySlot.HasFree);
                        Assert.AreEqual(_expectedRect[i].X, emptySlot.Shape.X);
                        Assert.AreEqual(_expectedRect[i].Y, emptySlot.Shape.Y);
                        Assert.AreEqual(_expectedRect[i].Width, emptySlot.Shape.Width);
                        Assert.AreEqual(_expectedRect[i].Height, emptySlot.Shape.Height);
                        Assert.AreEqual(_expectedSize[1], emptySlot.Size);
                    }
                }
                catch (PlaceMintException e)
                {
                    Assert.IsInstanceOfType(typeof(IllegalInitializationException), e);
                }


                Slot fullSlot = null;
                List<IntPtr> hwnds = new List<IntPtr>(1);
                hwnds.Add(_expectedHWND[i]);
                try
                {
                    fullSlot = new Slot(_expectedRect[i], new Hotkey(), _expectedSize[i], hwnds); //Test fill Slot by value
                    if (_expectedHWND[i] == Slot.EMPTY)
                    {
                        Assert.Fail("Constructing an empty Slot by passing Slot.EMPTY should throw an exception.");
                    }
                }
                catch (PlaceMintException e)
                {
                    Assert.IsInstanceOfType(typeof(IllegalInitializationException), e);
                    Assert.IsNull(fullSlot);
                    continue; //No need to contiue as the object is null
                }

                //Test properties
                Assert.IsFalse(fullSlot.IsEmpty);
                Assert.AreEqual(fullSlot.Shape.X, _expectedRect[i].X);
                Assert.AreEqual(fullSlot.Shape.Y, _expectedRect[i].Y);
                Assert.AreEqual(fullSlot.Shape.Width, _expectedRect[i].Width);
                Assert.AreEqual(fullSlot.Shape.Height, _expectedRect[i].Height);
                Assert.AreNotEqual(fullSlot.Hwnds[0], Slot.EMPTY);
                Assert.AreEqual(fullSlot.Hwnds[0], _expectedHWND[i]);
                if (_expectedSize[i] > 1)
                {
                    Assert.IsTrue(fullSlot.IsStack);
                }
                else
                {
                    Assert.IsFalse(fullSlot.IsStack);
                }
            }
        }

        [Test]
        private void testDeepClone()
        {
            List<IntPtr> hwnd = new List<IntPtr>(1);
            hwnd.Add(_expectedHWND[1]);
            Slot slot1 = new Slot(_expectedRect[1], new Hotkey(), 1, hwnd);
            Slot slot2 = slot1.DeepClone();
            Assert.AreEqual(slot1, slot2);
            slot2.Hwnds[0] = _expectedHWND[2];
            slot2.Shape = _expectedRect[2];
            Assert.AreNotEqual(slot1.Hwnds[0], slot2.Hwnds[0]);
            Assert.AreNotEqual(slot1.Shape.X, slot2.Shape.X);
            Assert.AreNotEqual(slot1.Shape.Y, slot2.Shape.Y);
            Assert.AreNotEqual(slot1.Shape.Width, slot2.Shape.Width);
            Assert.AreNotEqual(slot1.Shape.Height, slot2.Shape.Height);
        }

        [Test]
        private void testEquals()
        {
            List<List<IntPtr>> hwnds = new List<List<IntPtr>>(3);
            hwnds.Add(new List<IntPtr>(1));
            hwnds[0].Add(_expectedHWND[0]);
            hwnds.Add(new List<IntPtr>(1));
            hwnds[1].Add(_expectedHWND[1]);
            hwnds.Add(new List<IntPtr>(1));
            hwnds[2].Add(_expectedHWND[2]);
            Slot slot1 = new Slot(_expectedRect[1], new Hotkey(), 1, hwnds[1]);
            Slot slot2 = new Slot(_expectedRect[1], new Hotkey(), 1, hwnds[1]);
            Slot slot3 = new Slot(_expectedRect[1], new Hotkey(), 1, hwnds[1]);

            Assert.IsFalse(slot1.Equals(null));
            Assert.IsFalse(slot1.Equals(1));
            Assert.IsTrue(slot1.Equals(slot1));
            Assert.IsTrue(slot1.Equals(slot2));
            Assert.IsTrue(slot2.Equals(slot1));
            Assert.IsTrue(slot1.Equals(slot3));
            Assert.IsTrue(slot2.Equals(slot3));

            RectangleWrap tempRW = new RectangleWrap(_expectedRect[1]);
            tempRW.X = _expectedRect[2].X;
            slot2 = new Slot(tempRW, new Hotkey(), _expectedSize[1], hwnds[1]);
            Assert.IsFalse(slot1.Equals(slot2));

            tempRW.X = _expectedRect[1].X;
            tempRW.Y = _expectedRect[2].Y;
            slot2 = new Slot(tempRW, new Hotkey(), _expectedSize[1], hwnds[1]);
            Assert.IsFalse(slot1.Equals(slot2));

            tempRW.Y = _expectedRect[1].Y;
            tempRW.Width = _expectedRect[2].Width;
            slot2 = new Slot(tempRW, new Hotkey(), _expectedSize[1], hwnds[1]);
            Assert.IsFalse(slot1.Equals(slot2));

            tempRW.Width = _expectedRect[1].Width;
            tempRW.Height = _expectedRect[2].Height;
            slot2 = new Slot(tempRW, new Hotkey(), _expectedSize[1], hwnds[1]);
            Assert.IsFalse(slot1.Equals(slot2));

            slot2 = new Slot(_expectedRect[1], new Hotkey(), _expectedSize[2], hwnds[1]);
            Assert.IsFalse(slot1.Equals(slot2));

            slot2 = new Slot(_expectedRect[1], new Hotkey(), _expectedSize[1], hwnds[2]);
            Assert.IsFalse(slot1.Equals(slot2));
        }

        [Test]
        private void testMoveAndSize()
        {
            Slot slot;
            for (int i = 0; i < _expectedBoool.Count; i++)
            {
                windowSetup();

                Assert.AreNotEqual(_winStart[0]["x"], _expected[i]["x"]);
                Assert.AreNotEqual(_winStart[0]["y"], _expected[i]["y"]);
                Assert.AreNotEqual(_winStart[0]["w"], _expected[i]["w"]);
                Assert.AreNotEqual(_winStart[0]["h"], _expected[i]["h"]);

                slot = new Slot(new RectangleWrap(_expected[i]["x"], _expected[i]["y"], _expected[i]["w"], _expected[i]["h"]), new Hotkey(), 1, _winHWNDs);
                //change shape
                Slot.MoveAndSizeWin(slot.Hwnds[0], slot.Shape, _expectedBoool[i]["move"], _expectedBoool[i]["size"], false);
                WindowsApi.GetWindowRect(_winHWNDs[0], out _winRect);
                _winNow.Clear();
                _winNow.Add("x", _winRect.Left);
                _winNow.Add("y", _winRect.Top);
                _winNow.Add("w", _winRect.Right - _winRect.Left);
                _winNow.Add("h", _winRect.Bottom - _winRect.Top);
                if (_expectedBoool[i]["move"])
                {
                    Assert.AreNotEqual(_winStart[0]["x"], _winNow["x"]);
                    Assert.AreNotEqual(_winStart[0]["y"], _winNow["y"]);
                    Assert.AreEqual(_expected[i]["x"], _winNow["x"]);
                    Assert.AreEqual(_expected[i]["y"], _winNow["y"]);
                }
                else
                {
                    Assert.AreEqual(_winStart[0]["x"], _winNow["x"]);
                    Assert.AreEqual(_winStart[0]["y"], _winNow["y"]);
                    Assert.AreNotEqual(_expected[i]["x"], _winNow["x"]);
                    Assert.AreNotEqual(_expected[i]["y"], _winNow["y"]);
                }
                if (_expectedBoool[i]["size"])
                {
                    Assert.AreNotEqual(_winStart[0]["w"], _winNow["w"]);
                    Assert.AreNotEqual(_winStart[0]["h"], _winNow["h"]);
                    Assert.AreEqual(_expected[i]["w"], _winNow["w"]);
                    Assert.AreEqual(_expected[i]["h"], _winNow["h"]);
                }
                else
                {
                    Assert.AreEqual(_winStart[0]["w"], _winNow["w"]);
                    Assert.AreEqual(_winStart[0]["h"], _winNow["h"]);
                    Assert.AreNotEqual(_expected[i]["w"], _winNow["w"]);
                    Assert.AreNotEqual(_expected[i]["h"], _winNow["h"]);
                }

                windowTearDown();
            }
            //Stack
            for (int i = 0; i < _expectedBoool.Count; i++)
            {
                windowSetupStack();

                slot = new Slot(new RectangleWrap(_expected[i]["x"], _expected[i]["y"], _expected[i]["w"], _expected[i]["h"]), new Hotkey(),
                    PROC_COUNT, _winHWNDs);
                //change shape
                for (int j = 0; j < slot.Size; j++)
                {
                    Assert.AreNotEqual(_winStart[j]["x"], _expected[i]["x"]);
                    Assert.AreNotEqual(_winStart[j]["y"], _expected[i]["y"]);
                    Assert.AreNotEqual(_winStart[j]["w"], _expected[i]["w"]);
                    Assert.AreNotEqual(_winStart[j]["h"], _expected[i]["h"]);

                    Slot.MoveAndSizeWin(slot.Hwnds[j], slot.Shape, _expectedBoool[i]["move"], _expectedBoool[i]["size"], false);
                    WindowsApi.GetWindowRect(_winHWNDs[j], out _winRect);
                    _winNow.Clear();
                    _winNow.Add("x", _winRect.Left);
                    _winNow.Add("y", _winRect.Top);
                    _winNow.Add("w", _winRect.Right - _winRect.Left);
                    _winNow.Add("h", _winRect.Bottom - _winRect.Top);
                    if (_expectedBoool[i]["move"])
                    {
                        Assert.AreNotEqual(_winStart[j]["x"], _winNow["x"]);
                        Assert.AreNotEqual(_winStart[j]["y"], _winNow["y"]);
                        Assert.AreEqual(_expected[i]["x"], _winNow["x"]);
                        Assert.AreEqual(_expected[i]["y"], _winNow["y"]);
                    }
                    else
                    {
                        Assert.AreEqual(_winStart[j]["x"], _winNow["x"]);
                        Assert.AreEqual(_winStart[j]["y"], _winNow["y"]);
                        Assert.AreNotEqual(_expected[i]["x"], _winNow["x"]);
                        Assert.AreNotEqual(_expected[i]["y"], _winNow["y"]);
                    }
                    if (_expectedBoool[i]["size"])
                    {
                        Assert.AreNotEqual(_winStart[j]["w"], _winNow["w"]);
                        Assert.AreNotEqual(_winStart[j]["h"], _winNow["h"]);
                        Assert.AreEqual(_expected[i]["w"], _winNow["w"]);
                        Assert.AreEqual(_expected[i]["h"], _winNow["h"]);
                    }
                    else
                    {
                        Assert.AreEqual(_winStart[j]["w"], _winNow["w"]);
                        Assert.AreEqual(_winStart[j]["h"], _winNow["h"]);
                        Assert.AreNotEqual(_expected[i]["w"], _winNow["w"]);
                        Assert.AreNotEqual(_expected[i]["h"], _winNow["h"]);
                    }
                }

                windowTearDown();
            }
        }

        [Test]
        private void testMinAndRestore()
        {
            windowSetup();

            Slot slot = new Slot(_expectedRect[0], new Hotkey(), 1, _winHWNDs);
            //change shape
            Slot.MinimizeWin(slot.Hwnds[0]);
            Assert.IsTrue(Slot.IsMinimized(_winHWNDs[0]));
            Assert.IsFalse(Slot.IsMaximized(_winHWNDs[0]));
            Slot.RestoreWin(slot.Hwnds[0]);
            Assert.IsFalse(Slot.IsMinimized(_winHWNDs[0]));
            Assert.IsFalse(Slot.IsMaximized(_winHWNDs[0]));
            Slot.MaximizeWin(slot.Hwnds[0]);
            Assert.IsFalse(Slot.IsMinimized(_winHWNDs[0]));
            Assert.IsTrue(Slot.IsMaximized(_winHWNDs[0]));
            Slot.RestoreWin(slot.Hwnds[0]);
            Assert.IsFalse(Slot.IsMinimized(_winHWNDs[0]));
            Assert.IsFalse(Slot.IsMaximized(_winHWNDs[0]));

            windowTearDown();
        }

        [Test]
        private void testHotkey()
        {
            Slot slot = new Slot(_expectedRect[1],_expectedHotkey[0],1);
            Assert.AreEqual(_expectedHotkey[0], slot.Hotkey);
            Assert.AreNotEqual(_expectedHotkey[0], _expectedHotkey[1]);
            Assert.AreNotEqual(_expectedHotkey[1], slot.Hotkey);
        }

        private void windowSetup()
        {
            windowSetup(1);
        }

        private void windowSetupStack()
        {
            windowSetup(PROC_COUNT);
        }

        private void windowSetup(int pCount)
        {
            _proccesses = new List<Process>();
            _winHWNDs = new List<IntPtr>();
            for (int i = 0; i < pCount; i++)
            {
                Process proc = Process.Start("notepad", String.Format(CultureInfo.InvariantCulture, "text{0}.txt", i));
                proc.WaitForInputIdle();
                _proccesses.Add(proc);
                _winHWNDs.Add(Program.NativeMethods.FindWindow(null, proc.MainWindowTitle));
                WindowsApi.RECT rect;
                WindowsApi.GetWindowRect(_winHWNDs[0], out rect);
                _winStart.Add(new Dictionary<string, int>());
                _winStart[i].Add("x", rect.Left);
                _winStart[i].Add("y", rect.Top);
                _winStart[i].Add("w", rect.Right - rect.Left);
                _winStart[i].Add("h", rect.Bottom - rect.Top);
            }
        }

        private void windowTearDown()
        {
            //reset start and kill process
            for(int i = 0; i < _winHWNDs.Count; i++)
            {
                WindowsApi.SetWindowPos(_winHWNDs[i], WindowsApi.HWND_TOP, _winStart[i]["x"], _winStart[i]["y"],
                    _winStart[i]["w"], _winStart[i]["h"], WindowsApi.SWP_NOZORDER);
            }
            foreach (Process proc in _proccesses)
            {
                proc.Kill();
            }
            _proccesses.Clear();
            _winStart.Clear();
            _winNow.Clear();
        }
    }
}
