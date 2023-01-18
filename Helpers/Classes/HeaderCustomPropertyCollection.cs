using System.Collections.Generic;

namespace HelpersClasses.Classes
{
    // ReSharper disable once UnusedMember.Global
    public class HeaderCustomPropertyCollection
    {
        #region Properties

        public HeaderCustomProperty this[int i]
        {
            get => _properties[i];
            set => _properties[i] = value;
        }

        /// <summary>
        /// Number of properties in the collection.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public int Count => _properties.Count;

        #endregion

        private readonly List<HeaderCustomProperty> _properties = new List<HeaderCustomProperty>();

        #region Add
        /// <summary>
        /// Adds new property to the collection.
        /// </summary>
        /// <param name="property"></param>
        // ReSharper disable once UnusedMember.Global
        public void Add(HeaderCustomProperty property) =>
            _properties.Add(property);
        #endregion

        #region Add
        /// <summary>
        /// Adds new property to the collection.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        // ReSharper disable once UnusedMember.Global
        public void Add(string name, string value) =>
            _properties.Add(new HeaderCustomProperty(name, value));
        #endregion

        #region Remove
        /// <summary>
        /// Removes a property from the collection.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public bool Remove(HeaderCustomProperty property) =>
            _properties.Remove(property);
        #endregion

        #region RemoveAt
        /// <summary>
        /// Removes a property from the collection at a specified position.
        /// </summary>
        /// <param name="index"></param>
        // ReSharper disable once UnusedMember.Global
        public void RemoveAt(int index) =>
            _properties.RemoveAt(index);
        #endregion
    }
}