namespace HelpersClasses.Classes
{
    public class HeaderCustomProperty
    {
        #region Properties.

        /// <summary>
        /// Property name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property value.
        /// </summary>
        public string Value { get; set; }

        #endregion

        public HeaderCustomProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}