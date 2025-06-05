using System;
using System.Windows.Forms;

//cs1591: Missing XML comment
#pragma warning disable 1591

//Implementation is based off of the code posted at
//http://www.liensberger.it/web/blog/?p=207

namespace PlaceMint.Manager
{
    using PMException;
    /// <summary>
    /// Encompasing implementation for hooking into keyboard events
    /// </summary>
    public sealed class KeyboardHook : IDisposable
    {
        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                // create the handle for the window.
                this.CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                //check for correct message and event is initialized
                if (m.Msg == WM_HOTKEY && KeyPressed != null)
                {
                    // get the keys.
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifyingKeys modifier = (ModifyingKeys)((int)m.LParam & 0xFFFF);

                    KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            #region IDisposable Members

            public void Dispose()
            {
                this.DestroyHandle();
            }

            #endregion
        }

        private Window _window = new Window();
        private int _currentId;

        public KeyboardHook()
        {
            // register the event of the inner native window.
            _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
            {
                if (KeyPressed != null)
                {
                    KeyPressed(this, args);
                }
            };
        }

        /// <summary>
        /// Registers a hot key in the system.
        /// </summary>
        /// <param name="hotkey">hot key to register.</param>
        /// <exception cref="HotkeyAlreadyExistsException">Thrown if RegisterHotkey fails</exception>
        public void RegisterHotkey(Hotkey hotkey)
        {
            if (!hotkey.IsSet)
            {
                return;
            }
            // increment the counter.
            _currentId = _currentId + 1;

            // register the hot key.
            if (!WindowsApi.RegisterHotKey(_window.Handle, _currentId, (uint)hotkey.ModKeys, (uint)hotkey.Key))
            {
                throw new HotkeyAlreadyExistsException("Couldn’t register the hotkey: " + hotkey.ToString());
            }
        }

        /// <summary>
        /// Unregister all Hotkeys and reset the count
        /// </summary>
        public void ClearAllHotkeys()
        {
            // unregister all the registered hot keys.
            for (int i = _currentId; i > 0; i--)
            {
                WindowsApi.UnregisterHotKey(_window.Handle, i);
            }
            _currentId = 0;
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public bool HasListeners
        {
            get { return (KeyPressed != null); }
        }

        #region IDisposable Members
        public void Dispose()
        {
            ClearAllHotkeys();
            // dispose the inner native window.
            _window.Dispose();
        }
        #endregion
    }

    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        private Hotkey _hotkey;

        internal KeyPressedEventArgs(ModifyingKeys modifier, Keys key)
        {
            _hotkey = new Hotkey(modifier, key);
        }

        public Hotkey Hotkey
        {
            get { return _hotkey; }
        }
    }
}