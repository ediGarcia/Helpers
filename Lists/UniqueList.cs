using System;
using System.Collections.Generic;
using System.Linq;
using HelperExtensions;
// ReSharper disable UnusedMember.Global

namespace Lists
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a list than cannot contains the same item more than once.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UniqueList<T> : List<T>
    {
        #region Properties

        /// <summary>
        /// Gets and sets whether an exception should be throw when an already existing item is added again. If set to false, the new item is ignored.
        /// </summary>
        public bool ThrowExceptionOnConflict { get; set; } = true;

        #endregion

        private readonly IComparer<T> _comparer;
        private readonly Comparison<T> _comparison;

        public UniqueList() { }

        public UniqueList(int capacity, bool throwExceptionOnConflict = true) : base(capacity) =>
            ThrowExceptionOnConflict = throwExceptionOnConflict;

        public UniqueList(IComparer<T> comparer, bool throwExceptionOnConflict = true)
        {
            ThrowExceptionOnConflict = throwExceptionOnConflict;
            _comparer = comparer;
        }

        public UniqueList(int capacity, IComparer<T> comparer, bool throwExceptionOnConflict = true) : base(capacity)
        {
            ThrowExceptionOnConflict = throwExceptionOnConflict;
            _comparer = comparer;
        }

        public UniqueList(Comparison<T> comparison, bool throwExceptionOnConflict = true)
        {
            ThrowExceptionOnConflict = throwExceptionOnConflict;
            _comparison = comparison;
        }

        public UniqueList(int capacity, Comparison<T> comparison, bool throwExceptionOnConflict = true) : base(capacity)
        {
            ThrowExceptionOnConflict = throwExceptionOnConflict;
            _comparison = comparison;
        }

        #region Public methods

        #region Add
        /// <inheritdoc cref="List{T}.Add" />
        /// <summary>
        /// Add an object to the end of the <see cref="UniqueList{T}"/>, if it does not exist.
        /// </summary>
        /// <param name="item">An object to be added to the end of <see cref="UniqueList{T}"/>.</param>
        public new void Add(T item)
        {
            if (Contains(item))
            {
                if (ThrowExceptionOnConflict)
                    throw new ConflictException("Cannot add already existing item.");
            }
            else
                base.Add(item);
        }
        #endregion

        #region AddRange
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="UniqueList{T}"/>.
        /// </summary>
        /// <param name="collection"></param>
        public new void AddRange(IEnumerable<T> collection) =>
            collection?.ForEach(Add);
        #endregion

        #region Contains

        ///<inheritdoc cref="List{T}.Contains"/>
        /// <summary>
        /// Determines whether an item exist in the <see cref="UniqueList{T}"/>.
        /// </summary>
        /// <param name="item">Tle element to locate in <see cref="UniqueList{T}"/>.</param>
        public new bool Contains(T item) =>
            this.Any(_ => base.Contains(item)
                || _comparer?.Compare(item, _) == 0
                || _comparison?.Invoke(item, _) == 0
                || (item as IComparable<T>)?.CompareTo(_) == 0
                || (item as IComparable)?.CompareTo(_) == 0);

        #endregion

        #endregion
    }
}
