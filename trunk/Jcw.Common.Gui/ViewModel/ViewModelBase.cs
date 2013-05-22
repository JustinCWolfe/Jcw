using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Jcw.Common.Gui.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public virtual string DisplayName { get; protected set; }
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion

        #region Constructors

        protected ViewModelBase()
        {
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            OnDispose ();
        }

        protected virtual void OnDispose()
        {
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        protected void NotifyPropertyChanged(string propertyName)
        {
            VerifyPropertyName (propertyName);

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs pcea = new PropertyChangedEventArgs (propertyName);
                handler (this, pcea);
            }
        }

        [Conditional ("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, public, instance property on this object.
            if (TypeDescriptor.GetProperties (this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                if (ThrowOnInvalidPropertyName)
                {
                    throw new Exception (msg);
                }
                Debug.Fail (msg);
            }
        }

        #endregion
    }
}
