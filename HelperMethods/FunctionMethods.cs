using System;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class FunctionMethods
{
    #region RepeatExecutions
    /// <summary>
    /// Runs the action the specified number of times.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="repetitions"></param>
    /// <param name="interval">Time interval (in milliseconds) between action calls.</param>
    public static async void RepeatExecutions(Action action, int repetitions, int interval)
    {
        for (int i = 0; i < repetitions; i++)
        {
            action?.Invoke();
            await Wait(interval);
        }
    }
    #endregion

    #region Wait
    /// <summary>
    /// Awaits the specified time.
    /// </summary>
    /// <param name="interval"></param>
    public static async Task Wait(int interval) =>
        await Task.Run(() => Thread.Sleep(interval));
    #endregion
}