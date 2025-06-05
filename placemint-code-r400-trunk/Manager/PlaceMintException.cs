using System;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager.PMException
{
    /// <summary>
    /// Base PlaceMint Exception Class
    /// </summary>
    public class PlaceMintException : Exception
    {
        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="msg">Message describing error.</param>
        protected PlaceMintException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="msg">Message describing error.</param>
        /// <param name="innerException">Exception being wrapped.</param>
        protected PlaceMintException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }

        /// <summary>
        /// Trace log: Set to default {objectType} because {Message}
        /// </summary>
        /// <param name="objectType"></param>
        public void Log(string objectType)
        {
            Logger.Trace("Set to default {0} because {1}", objectType, this.Message);
        }
    }
    #region Error Exceptions
    /// <summary>
    /// Thrown when there was an attempt to access an EmptySlot's hWND
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704")]
    public class NoHwndException : PlaceMintException
    {
        public NoHwndException(string msg) : base(msg) { }
    }
    /// <summary>
    /// Thrown when a call to user32.dll returned an error.
    /// </summary>
    public class Win32Exception : PlaceMintException
    {
        public Win32Exception(string msg) : base(msg) { }
    }
    /// <summary>
    /// Thrown when a call to user32.dll returned an error.
    /// </summary>
    public class OutOfRangeException : PlaceMintException
    {
        public OutOfRangeException(string msg) : base(msg) { }
    }
    /// <summary>
    /// Thrown when calling a constructor with Illegal arguments.
    /// </summary>
    public class IllegalInitializationException : PlaceMintException
    {
        public IllegalInitializationException(string msg) : base(msg) { }
    }
    /// <summary>
    /// Thrown when a file cannot be found in the filesystem.
    /// </summary>
    public class PMFileNotFoundException : PlaceMintException
    {
        public PMFileNotFoundException(string msg) : base(msg) { }

        public PMFileNotFoundException(string msg, Exception innerException)
            : base(msg, innerException) { }
    }
    /// <summary>
    /// Thrown when a file cannot be found in the filesystem.
    /// </summary>
    public class WrongXmlFormatException : PlaceMintException
    {
        public WrongXmlFormatException(string msg) : base(msg) { }
        public WrongXmlFormatException(string msg, Exception innerException)
            : base(msg, innerException) { }
    }
    /// <summary>
    /// Thrown when an event is triggered by an illegal object.
    /// </summary>
    public class IllegalSenderException : PlaceMintException
    {
        public IllegalSenderException(string msg) : base(msg) { }
    }
    /// <summary>
    /// Thrown when a case statement reaches default when not expected.
    /// </summary>
    public class IllegalFallThroughException : PlaceMintException
    {
        public IllegalFallThroughException(string msg) : base(msg) { }
    }
    /// <summary>
    /// Thrown when a PMRegex match string is not a valid Regex.
    /// </summary>
    public class InvalidRegexException : PlaceMintException
    {
        public InvalidRegexException(string msg) : base(msg) { }
        public InvalidRegexException(string msg, Exception innerException)
            : base(msg, innerException) { }
    }
    /// <summary>
    /// Thrown when a PMRegex match string is not a valid Regex.
    /// </summary>
    public class HotkeyAlreadyExistsException : PlaceMintException
    {
        public HotkeyAlreadyExistsException(string msg) : base(msg) { }
    }
    /// <summary>
    /// Thrown when a file path has too many characters.
    /// </summary>
    public class PMPathTooLongException : PlaceMintException
    {
        public PMPathTooLongException(string msg) : base(msg) { }
        public PMPathTooLongException(string msg, Exception innerException)
            : base(msg, innerException) { }
    }
    /// <summary>
    /// Thrown when an XML file has a value that can not be converted into the correspoding object.
    /// </summary>
    public class InvalidXmlValueException : PlaceMintException
    {
        public InvalidXmlValueException(string msg) : base(msg) { }
        public InvalidXmlValueException(string msg, Exception innerException)
            : base(msg, innerException) { }
    }

    /// <summary>
    /// Thrown when looking for an EmptySlot and none exist.
    /// </summary>
    public class StackFullException : PlaceMintException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg"></param>
        public StackFullException(string msg) : base(msg) { }
    }

    /// <summary>
    /// Thrown when trying to loading a empty path.
    /// </summary>
    public class EmptyPathException : PlaceMintException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmptyPathException() : base("A valid path must be supplied.") { }
    }

    /// <summary>
    /// Thrown when trying to loading a empty path.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704")]
    public class HwndMissingException : PlaceMintException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public HwndMissingException(IntPtr hwnd)
            : base(string.Format("{0} is no longer in _windows, throwing exception", hwnd)) { }
    }

    #endregion
    #region Non-Error Exceptions
    //All non-error exception aren't appended with "Exception"

    /// <summary>
    /// Thrown when looking for an EmptySlot and none exist.
    /// </summary>
    public class AllSlotsFullException : PlaceMintException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AllSlotsFullException() : base("") { }
    }
    #endregion
}