using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Helpers.Annotations;

namespace HelpersClasses.Classes
{
    [Serializable]
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        #region Events

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        #region OnPropertyChanged
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #endregion
    }
}
