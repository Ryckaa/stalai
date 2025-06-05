namespace PlaceMint.Manager
{
    /// <summary>
    /// A simple read-only iterator.
    /// </summary>
    public interface IRIterator<T>
    {
        /// <summary>
        /// Resets this iterator to the first element.
        /// </summary>
        void IteratorReset();

        /// <summary>
        /// Moves to the next element.
        /// </summary>
        void IteratorNext();

        /// <summary>
        /// Are there more elements.
        /// </summary>
        bool IteratorHasMore();

        /// <summary>
        /// The current element (read-only access).
        /// </summary>
        T IteratorCurrent { get; }
    }

    /// <summary>
    /// Interface for a writable iterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWIterator<T> : IRIterator<T>
    {
        /// <summary>
        /// The current element (writeable access).
        /// </summary>
        new T IteratorCurrent { get; set; }
    }
}