using System;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class FunctionMethods
{
    #region Wait
    /// <summary>
    /// Awaits the specified time.
    /// </summary>
    /// <param name="interval"></param>
    public static async Task Wait(int interval)
    {
        switch (interval)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(interval), "Interval must be a non-negative number.");

            case 0:
                return;

            default:
                await Task.Run(() => Thread.Sleep(interval));
                break;
        }
    }
    #endregion
}