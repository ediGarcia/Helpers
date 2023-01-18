using System;
using System.Linq;

namespace HelperMethods
{
    public static class NumberMethods
    {
        #region Properties

        /// <summary>
        /// Random number generator.
        /// </summary>
        public static Random Rand { get; } = new();

        #endregion

        #region Public Methods

        #region Average

        #region Average (sbyte[])
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params sbyte[] values)
        {
            CheckArray(values);
            return Sum(Array.ConvertAll(values, _ => (double)_)) / values.Length;
        }
        #endregion

        #region Average (byte)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params byte[] values)
        {
            CheckArray(values);
            return Sum(Array.ConvertAll(values, _ => (double)_)) / values.Length;
        }
        #endregion

        #region Average (short)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params short[] values)
        {
            CheckArray(values);
            return Sum(Array.ConvertAll(values, _ => (double)_)) / values.Length;
        }
        #endregion

        #region Average (ushort)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params ushort[] values)
        {
            CheckArray(values);
            return Sum(Array.ConvertAll(values, _ => (double)_)) / values.Length;
        }
        #endregion

        #region Average (int)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params int[] values)
        {
            CheckArray(values);
            return values.Average();
        }
        #endregion

        #region Average (uint)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params uint[] values)
        {
            CheckArray(values);
            return Sum(Array.ConvertAll(values, _ => (double)_)) / values.Length;
        }
        #endregion

        #region Average (long)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params long[] values)
        {
            CheckArray(values);
            return values.Average();
        }
        #endregion

        #region Average (ulong)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params ulong[] values)
        {
            CheckArray(values);
            return Sum(Array.ConvertAll(values, _ => (double)_)) / values.Length;
        }
        #endregion

        #region Average (float)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static float Average(params float[] values)
        {
            CheckArray(values);
            return values.Average();
        }
        #endregion

        #region Average (double)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Average(params double[] values)
        {
            CheckArray(values);
            return values.Average();
        }
        #endregion

        #region Average (decimal)
        /// <Averagemary>
        /// Returns the average value of the parameters.
        /// </Averagemary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static decimal Average(params decimal[] values)
        {
            CheckArray(values);
            return values.Average();
        }
        #endregion

        #endregion

        #region Get Random

        #region GetRandomDouble
        /// <summary>
        /// Returns a random double value between 0 and 1.
        /// </summary>
        /// <returns></returns>
        public static double GetRandomDouble() =>
            Rand.NextDouble();
        #endregion

        #region GetRandomDouble (double)
        /// <summary>
        /// Returns a random double value between 0 and [maximum].
        /// </summary>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static double GetRandomDouble(double maximum) =>
            maximum < 0
                ? throw new ArgumentOutOfRangeException($"{nameof(maximum)} must be equal or greater than 0.")
                : Rand.NextDouble() * maximum;
        #endregion

        #region GetRandomDouble (double, double)
        /// <summary>
        /// Returns a random double value between [minimum] and [maximum].
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        public static double GetRandomDouble(double minimum, double maximum) =>
            maximum < minimum
                ? throw new ArgumentOutOfRangeException($"{nameof(maximum)} must be equal or greater than {nameof(minimum)}.")
                : Rand.NextDouble() * (maximum - minimum) + minimum;
        #endregion

        #region GetRandomFloat
        /// <summary>
        /// Returns a random float value between 0 and 1.
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static float GetRandomFloat() =>
            (float)GetRandomDouble();
        #endregion

        #region GetRandomFloat (float)
        /// <summary>
        /// Returns a random float value between 0 and [maximum].
        /// </summary>
        /// <returns></returns>
        public static float GetRandomFloat(float maximum) =>
            (float)GetRandomDouble(maximum);
        #endregion

        #region GetRandomFloat (float, float)
        /// <summary>
        /// Returns a random double value between [minimum] and [maximum].
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        // ReSharper disable once UnusedMember.Global
        public static float GetRandomFloat(double minimum, double maximum) =>
            (float)GetRandomDouble(minimum, maximum);
        #endregion

        #region GetRandomInt
        /// <summary>
        /// Returns a random int value.
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static int GetRandomInt() =>
            Rand.Next();
        #endregion

        #region GetRandomInt (int, [bool])
        /// <summary>
        /// Returns a random int value between 0 and [maximum].
        /// </summary>
        /// <param name="maximum"></param>
        /// <param name="isInclusive"></param>
        /// <returns></returns>
        public static int GetRandomInt(int maximum, bool isInclusive = false)
        {
            if (isInclusive)
                maximum++;

            return Rand.Next(maximum);
        }
        #endregion

        #region GetRandomInt (int, int, [bool])
        /// <summary>
        /// Returns a random int value between [minimum] and [maximum].
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="isInclusive"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        public static int GetRandomInt(int minimum, int maximum, bool isInclusive = false)
        {
            if (isInclusive && maximum < minimum)
                maximum++;

            return Rand.Next(minimum, maximum);
        }
        #endregion

        #endregion

        #region Max

        #region Max (sbyte)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static sbyte Max(params sbyte[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (byte)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static byte Max(params byte[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (short)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static short Max(params short[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (ushort)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static ushort Max(params ushort[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (int)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static int Max(params int[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (uint)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static uint Max(params uint[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (long)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static long Max(params long[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (ulong)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static ulong Max(params ulong[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (float)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static float Max(params float[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (double)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Max(params double[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #region Max (decimal)
        /// <summary>
        /// Returns the larger value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static decimal Max(params decimal[] values)
        {
            CheckArray(values);
            return values.Max();
        }
        #endregion

        #endregion

        #region Min

        #region Min (sbyte)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static sbyte Min(params sbyte[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (byte)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static byte Min(params byte[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (short)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static short Min(params short[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (ushort)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static ushort Min(params ushort[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (int)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static int Min(params int[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (uint)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static uint Min(params uint[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (long)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static long Min(params long[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (ulong)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static ulong Min(params ulong[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (float)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static float Min(params float[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (double)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static double Min(params double[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #region Min (decimal)
        /// <summary>
        /// Returns the smaller value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static decimal Min(params decimal[] values)
        {
            CheckArray(values);
            return values.Min();
        }
        #endregion

        #endregion

        #region Sum

        #region Sum (sbyte)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static sbyte Sum(params sbyte[] values)
        {
            CheckArray(values);
            return values.Aggregate<sbyte, sbyte>(0, (current, value) => (sbyte)(current + value));
        }
        #endregion

        #region Sum (byte)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static byte Sum(params byte[] values)
        {
            CheckArray(values);
            return values.Aggregate<byte, byte>(0, (current, value) => (byte)(current + value));
        }
        #endregion

        #region Sum (short)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static short Sum(params short[] values)
        {
            CheckArray(values);
            return values.Aggregate<short, short>(0, (current, value) => (short)(current + value));
        }
        #endregion

        #region Sum (ushort)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static ushort Sum(params ushort[] values)
        {
            CheckArray(values);
            return values.Aggregate<ushort, ushort>(0, (current, value) => (ushort)(current + value));
        }
        #endregion

        #region Sum (int)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int Sum(params int[] values)
        {
            CheckArray(values);
            return values.Sum();
        }
        #endregion

        #region Sum (uint)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static uint Sum(params uint[] values)
        {
            CheckArray(values);
            return values.Aggregate<uint, uint>(0, (current, value) => current + value);
        }
        #endregion

        #region Sum (long)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static long Sum(params long[] values)
        {
            CheckArray(values);
            return values.Sum();
        }
        #endregion

        #region Sum (ulong)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static ulong Sum(params ulong[] values)
        {
            CheckArray(values);
            return values.Aggregate<ulong, ulong>(0, (current, value) => current + value);
        }
        #endregion

        #region Sum (float)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static float Sum(params float[] values)
        {
            CheckArray(values);
            return values.Sum();
        }
        #endregion

        #region Sum (double)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double Sum(params double[] values)
        {
            CheckArray(values);
            return values.Sum();
        }
        #endregion

        #region Sum (decimal)
        /// <summary>
        /// Returns the sum of all the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static decimal Sum(params decimal[] values)
        {
            CheckArray(values);
            return values.Sum();
        }
        #endregion

        #endregion

        #endregion

        #region Private Methods

        #region CheckArray
        /// <summary>
        /// Throws exception whether the array is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        private static void CheckArray<T>(T[] values)
        {
            if (values.Length == 0)
                throw new ArgumentException("No values to compare.");
        }
        #endregion

        #endregion
    }
}
