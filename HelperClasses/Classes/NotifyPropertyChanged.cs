using System.ComponentModel;
using System.Runtime.CompilerServices;
// ReSharper disable UnusedMember.Global

namespace HelperClasses.Classes;

public class NotifyPropertyChanged : INotifyPropertyChanged
{
    #region Custom Events

    /// <summary>
    /// Notifies whether a property's value was changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Protected Methods

    #region Notify
    /// <summary>
    /// Notifies whether the property has changed.
    /// </summary>
    /// <param name="propertyName"></param>
    protected void Notify([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new(propertyName));
    #endregion

    #region SetField
    /// <summary>
    /// Sets the specified field's value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        Notify(propertyName);

        return true;
    }
    #endregion

    #endregion
}
