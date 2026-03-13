using System.Xml.Linq;

namespace HelperExtensions;

public static class XmlExtensions {

    #region XAttribute

    extension(XAttribute a)
    {
        #region GetSignedByteValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="sbyte"/>. If the value cannot be parsed as a signed byte, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed signed byte value.</returns>
        public sbyte GetSignedByteValue() =>
            SByte.Parse(a?.Value);
        #endregion

        #region GetSignedByteValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="sbyte"/>. If the value cannot be parsed as a signed byte, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed signed byte value or the default value.</returns>
        public sbyte GetSignedByteValueOrDefault(sbyte defaultValue = 0) =>
            SByte.TryParse(a?.Value, out sbyte value) ? value : defaultValue;
        #endregion

        #region GetByteValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="byte"/>. If the value cannot be parsed as a byte, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed byte value.</returns>
        public byte GetByteValue() =>
            Byte.Parse(a?.Value);
        #endregion

        #region GetByteValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="byte"/>. If the value cannot be parsed as a byte, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed byte value or the default value.</returns>
        public byte GetByteValueOrDefault(byte defaultValue = 0) =>
            Byte.TryParse(a?.Value, out byte value) ? value : defaultValue;
        #endregion

        #region GetShortValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="short"/> (Int16). If the value cannot be parsed as a short, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed short value.</returns>
        public short GetShortValue() =>
            Int16.Parse(a?.Value);
        #endregion

        #region GetShortValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="short"/> (Int16). If the value cannot be parsed as a short, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed short value or the default value.</returns>
        public short GetShortValueOrDefault(short defaultValue = 0) =>
            Int16.TryParse(a?.Value, out short value) ? value : defaultValue;
        #endregion

        #region GetUnsignedShortValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="ushort"/> (UInt16). If the value cannot be parsed as an unsigned short, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed unsigned short value.</returns>
        public ushort GetUnsignedShortValue() =>
            UInt16.Parse(a?.Value);
        #endregion

        #region GetUnsignedShortValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="ushort"/> (UInt16). If the value cannot be parsed as an unsigned short, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed unsigned short value or the default value.</returns>
        public ushort GetUnsignedShortValueOrDefault(ushort defaultValue = 0) =>
            UInt16.TryParse(a?.Value, out ushort value) ? value : defaultValue;
        #endregion

        #region GetIntValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="int"/> (Int32). If the value cannot be parsed as an integer, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed integer value.</returns>
        public int GetIntValue() =>
            Int32.Parse(a?.Value);
        #endregion

        #region GetIntValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="int"/> (Int32). If the value cannot be parsed as an integer, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed integer value or the default value.</returns>
        public int GetIntValueOrDefault(int defaultValue = 0) =>
            Int32.TryParse(a?.Value, out int value) ? value : defaultValue;
        #endregion

        #region GetUnsignedIntValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="uint"/> (UInt32). If the value cannot be parsed as an unsigned integer, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed unsigned integer value.</returns>
        public uint GetUnsignedIntValue() =>
            UInt32.Parse(a?.Value);
        #endregion

        #region GetUnsignedIntValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="uint"/> (UInt32). If the value cannot be parsed as an unsigned integer, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed unsigned integer value or the default value.</returns>
        public uint GetUnsignedIntValueOrDefault(uint defaultValue = 0u) =>
            UInt32.TryParse(a?.Value, out uint value) ? value : defaultValue;
        #endregion

        #region GetLongValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="long"/> (Int64). If the value cannot be parsed as a long, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed long value.</returns>
        public long GetLongValue() =>
            Int64.Parse(a?.Value);
        #endregion

        #region GetLongValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="long"/> (Int64). If the value cannot be parsed as a long, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed long value or the default value.</returns>
        public long GetLongValueOrDefault(long defaultValue = 0L) =>
            Int64.TryParse(a?.Value, out long value) ? value : defaultValue;
        #endregion

        #region GetUnsignedLongValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="ulong"/> (UInt64). If the value cannot be parsed as an unsigned long, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed unsigned long value.</returns>
        public ulong GetUnsignedLongValue() =>
            UInt64.Parse(a?.Value);
        #endregion

        #region GetUnsignedLongValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="ulong"/> (UInt64). If the value cannot be parsed as an unsigned long, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed unsigned long value or the default value.</returns>
        public ulong GetUnsignedLongValueOrDefault(ulong defaultValue = 0ul) =>
            UInt64.TryParse(a?.Value, out ulong value) ? value : defaultValue;
        #endregion

        #region GetFloatValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="float"/> (Single). If the value cannot be parsed as a float, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed float value.</returns>
        public float GetFloatValue() =>
            Single.Parse(a?.Value);
        #endregion

        #region GetFloatValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="float"/> (Single). If the value cannot be parsed as a float, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed float value or the default value.</returns>
        public float GetFloatValueOrDefault(float defaultValue = 0f) =>
            Single.TryParse(a?.Value, out float value) ? value : defaultValue;
        #endregion

        #region GetDoubleValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="double"/> (Double). If the value cannot be parsed as a double, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed double value.</returns>
        public double GetDoubleValue() =>
            Double.Parse(a?.Value);
        #endregion

        #region GetDoubleValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="double"/>. If the value cannot be parsed as a double, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed double value or the default value.</returns>
        public double GetDoubleValueOrDefault(double defaultValue = 0d) =>
            Double.TryParse(a?.Value, out double value) ? value : defaultValue;
        #endregion

        #region GetDecimalValue
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="decimal"/>. If the value cannot be parsed as a decimal, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed decimal value.</returns>
        public decimal GetDecimalValue() =>
            Decimal.Parse(a?.Value);
        #endregion

        #region GetDecimalValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a <see cref="decimal"/>. If the value cannot be parsed as a decimal, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed decimal value or the default value.</returns>
        public decimal GetDecimalValueOrDefault(decimal defaultValue = 0m) =>
            Decimal.TryParse(a?.Value, out decimal value) ? value : defaultValue;
        #endregion

        #region GetValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XAttribute"/> as a string. If the XAttribute is null, the specified default value will be returned instead. If no default value is provided, it defaults to null.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the XAttribute is null.</param>
        /// <returns>The value of the XAttribute or the default value.</returns>
        public string GetValueOrDefault(string? defaultValue = null) =>
            a?.Value ?? defaultValue;
        #endregion

        #region TryGetSignedByteValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="sbyte"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetSignedByteValue(out sbyte value) =>
            SByte.TryParse(a?.Value, out value);
        #endregion

        #region TryGetByteValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="byte"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetByteValue(out byte value) =>
            Byte.TryParse(a?.Value, out value);
        #endregion

        #region TryGetShortValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="short"/> (Int16). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetShortValue(out short value) =>
            Int16.TryParse(a?.Value, out value);
        #endregion

        #region TryGetUnsignedShortValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="ushort"/> (UInt16). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetUnsignedShortValue(out ushort value) =>
            UInt16.TryParse(a?.Value, out value);
        #endregion

        #region TryGetIntValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as an <see cref="int"/> (Int32). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetIntValue(out int value) =>
            Int32.TryParse(a?.Value, out value);
        #endregion

        #region TryGetUnsignedIntValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="uint"/> (UInt32). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetUnsignedIntValue(out uint value) =>
            UInt32.TryParse(a?.Value, out value);
        #endregion

        #region TryGetLongValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="long"/> (Int64). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetLongValue(out long value) =>
            Int64.TryParse(a?.Value, out value);
        #endregion

        #region TryGetUnsignedLongValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as an <see cref="ulong"/> (UInt64). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetUnsignedLongValue(out ulong value) =>
            UInt64.TryParse(a?.Value, out value);
        #endregion

        #region TryGetFloatValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="float"/> (Single). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetFloatValue(out float value) =>
            Single.TryParse(a?.Value, out value);
        #endregion

        #region TryGetDoubleValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="double"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetDoubleValue(out double value) =>
            Double.TryParse(a?.Value, out value);
        #endregion

        #region TryGetDecimalValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XAttribute"/> as a <see cref="decimal"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetDecimalValue(out decimal value) =>
            Decimal.TryParse(a?.Value, out value);
        #endregion
    }

    #endregion

    #region XElement

    extension(XElement e)
    {
        #region GetSignedByteValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="sbyte"/>. If the value cannot be parsed as a signed byte, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed signed byte value.</returns>
        public sbyte GetSignedByteValue() =>
            SByte.Parse(e?.Value);
        #endregion

        #region GetSignedByteValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="sbyte"/>. If the value cannot be parsed as a signed byte, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed signed byte value or the default value.</returns>
        public sbyte GetSignedByteValueOrDefault(sbyte defaultValue = 0) =>
            SByte.TryParse(e?.Value, out sbyte value) ? value : defaultValue;
        #endregion

        #region GetByteValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="byte"/>. If the value cannot be parsed as a byte, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed byte value.</returns>
        public byte GetByteValue() =>
            Byte.Parse(e?.Value);
        #endregion

        #region GetByteValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="byte"/>. If the value cannot be parsed as a byte, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed byte value or the default value.</returns>
        public byte GetByteValueOrDefault(byte defaultValue = 0) =>
            Byte.TryParse(e?.Value, out byte value) ? value : defaultValue;
        #endregion

        #region GetShortValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="short"/> (Int16). If the value cannot be parsed as a short, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed short value.</returns>
        public short GetShortValue() =>
            Int16.Parse(e?.Value);
        #endregion

        #region GetShortValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="short"/> (Int16). If the value cannot be parsed as a short, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed short value or the default value.</returns>
        public short GetShortValueOrDefault(short defaultValue = 0) =>
            Int16.TryParse(e?.Value, out short value) ? value : defaultValue;
        #endregion

        #region GetUnsignedShortValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="ushort"/> (UInt16). If the value cannot be parsed as an unsigned short, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed unsigned short value.</returns>
        public ushort GetUnsignedShortValue() =>
            UInt16.Parse(e?.Value);
        #endregion

        #region GetUnsignedShortValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="ushort"/> (UInt16). If the value cannot be parsed as an unsigned short, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed unsigned short value or the default value.</returns>
        public ushort GetUnsignedShortValueOrDefault(ushort defaultValue = 0) =>
            UInt16.TryParse(e?.Value, out ushort value) ? value : defaultValue;
        #endregion

        #region GetIntValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="int"/> (Int32). If the value cannot be parsed as an integer, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed integer value.</returns>
        public int GetIntValue() =>
            Int32.Parse(e?.Value);
        #endregion

        #region GetIntValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="int"/> (Int32). If the value cannot be parsed as an integer, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed integer value or the default value.</returns>
        public int GetIntValueOrDefault(int defaultValue = 0) =>
            Int32.TryParse(e?.Value, out int value) ? value : defaultValue;
        #endregion

        #region GetUnsignedIntValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="uint"/> (UInt32). If the value cannot be parsed as an unsigned integer, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed unsigned integer value.</returns>
        public uint GetUnsignedIntValue() =>
            UInt32.Parse(e?.Value);
        #endregion

        #region GetUnsignedIntValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="uint"/> (UInt32). If the value cannot be parsed as an unsigned integer, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed unsigned integer value or the default value.</returns>
        public uint GetUnsignedIntValueOrDefault(uint defaultValue = 0u) =>
            UInt32.TryParse(e?.Value, out uint value) ? value : defaultValue;
        #endregion

        #region GetLongValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="long"/> (Int64). If the value cannot be parsed as a long, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed long value.</returns>
        public long GetLongValue() =>
            Int64.Parse(e?.Value);
        #endregion

        #region GetLongValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="long"/> (Int64). If the value cannot be parsed as a long, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed long value or the default value.</returns>
        public long GetLongValueOrDefault(long defaultValue = 0L) =>
            Int64.TryParse(e?.Value, out long value) ? value : defaultValue;
#endregion

        #region GetUnsignedLongValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="ulong"/> (UInt64). If the value cannot be parsed as an unsigned long, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed unsigned long value.</returns>
        public ulong GetUnsignedLongValue() =>
            UInt64.Parse(e?.Value);
        #endregion

        #region GetUnsignedLongValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="ulong"/> (UInt64). If the value cannot be parsed as an unsigned long, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed unsigned long value or the default value.</returns>
        public ulong GetUnsignedLongValueOrDefault(ulong defaultValue = 0ul) =>
            UInt64.TryParse(e?.Value, out ulong value) ? value : defaultValue;
        #endregion

        #region GetFloatValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="float"/> (Single). If the value cannot be parsed as a float, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed float value.</returns>
        public float GetFloatValue() =>
            Single.Parse(e?.Value);
        #endregion

        #region GetFloatValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="float"/> (Single). If the value cannot be parsed as a float, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed float value or the default value.</returns>
        public float GetFloatValueOrDefault(float defaultValue = 0f) =>
            Single.TryParse(e?.Value, out float value) ? value : defaultValue;
        #endregion

        #region GetDoubleValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="double"/> (Double). If the value cannot be parsed as a double, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed double value.</returns>
        public double GetDoubleValue() =>
            Double.Parse(e?.Value);
        #endregion

        #region GetDoubleValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="double"/>. If the value cannot be parsed as a double, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed double value or the default value.</returns>
        public double GetDoubleValueOrDefault(double defaultValue = 0d) =>
            Double.TryParse(e?.Value, out double value) ? value : defaultValue;
        #endregion

        #region GetDecimalValue
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="decimal"/>. If the value cannot be parsed as a decimal, an exception will be thrown.
        /// </summary>
        /// <returns>The parsed decimal value.</returns>
        public decimal GetDecimalValue() =>
            Decimal.Parse(e?.Value);
        #endregion

        #region GetDecimalValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a <see cref="decimal"/>. If the value cannot be parsed as a decimal, the specified default value will be returned instead. If no default value is provided, it defaults to 0.
        /// </summary>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>The parsed decimal value or the default value.</returns>
        public decimal GetDecimalValueOrDefault(decimal defaultValue = 0m) =>
            Decimal.TryParse(e?.Value, out decimal value) ? value : defaultValue;
        #endregion

        #region GetValueOrDefault
        /// <summary>
        /// Gets the value of the <see cref="XElement"/> as a string. If the XElement is null, the specified default value will be returned instead. If no default value is provided, it defaults to null.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the XElement is null.</param>
        /// <returns>The value of the XElement or the default value.</returns>
        public string GetValueOrDefault(string? defaultValue = null) =>
            e?.Value ?? defaultValue;
        #endregion

        #region TryGetSignedByteValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="sbyte"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetSignedByteValue(out sbyte value) =>
            SByte.TryParse(e?.Value, out value);
        #endregion

        #region TryGetByteValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="byte"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
    /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetByteValue(out byte value) =>
            Byte.TryParse(e?.Value, out value);
        #endregion

        #region TryGetShortValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="short"/> (Int16). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetShortValue(out short value) =>
            Int16.TryParse(e?.Value, out value);
        #endregion

        #region TryGetUnsignedShortValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="ushort"/> (UInt16). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetUnsignedShortValue(out ushort value) =>
            UInt16.TryParse(e?.Value, out value);
        #endregion

        #region TryGetIntValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as an <see cref="int"/> (Int32). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetIntValue(out int value) =>
            Int32.TryParse(e?.Value, out value);
        #endregion

        #region TryGetUnsignedIntValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="uint"/> (UInt32). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetUnsignedIntValue(out uint value) =>
            UInt32.TryParse(e?.Value, out value);
        #endregion

        #region TryGetLongValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="long"/> (Int64). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetLongValue(out long value) =>
            Int64.TryParse(e?.Value, out value);
        #endregion

        #region TryGetUnsignedLongValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as an <see cref="ulong"/> (UInt64). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetUnsignedLongValue(out ulong value) =>
            UInt64.TryParse(e?.Value, out value);
        #endregion

        #region TryGetFloatValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="float"/> (Single). Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetFloatValue(out float value) =>
            Single.TryParse(e?.Value, out value);
        #endregion

        #region TryGetDoubleValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="double"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetDoubleValue(out double value) =>
            Double.TryParse(e?.Value, out value);
        #endregion

        #region TryGetDecimalValue
        /// <summary>
        /// Attempts to parse the value of the <see cref="XElement"/> as a <see cref="decimal"/>. Returns true if parsing is successful, false otherwise. The parsed value is returned through the out parameter.
        /// </summary>
        /// <param name="value">The parsed value if parsing is successful.</param>
        /// <returns>True if parsing is successful, false otherwise.</returns>
        public bool TryGetDecimalValue(out decimal value) =>
            Decimal.TryParse(e?.Value, out value);
        #endregion
    }

    #endregion
}
