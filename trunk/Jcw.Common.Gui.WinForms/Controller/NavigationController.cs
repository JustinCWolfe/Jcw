using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Jcw.Common;
using Jcw.Common.Gui.Interfaces;
using Jcw.Common.Gui.WinForms.Applications;
using Jcw.Common.Gui.WinForms.Forms;

namespace Jcw.Common.Gui.WinForms.Controller
{
    public abstract class NavigationControllerBase<T> : IDisposable, INavigationController<T> where T : Form, INavigationBehavior
    {
        #region Properties

        private Dictionary<T, EventHandler<JcwEventArgs<T>>> MdiChildFormPopCallbacks { get; set; }
        private Stack<T> NavigationStack { get; set; }
        protected NavigationMdiParentBaseFrm<T> MdiParentForm { get; set; }

        #endregion

        #region Singleton Implementation

        protected static NavigationControllerBase<T> instance = null;
        protected static void ValidateControllerState (NavigationControllerBase<T> controller)
        {
            if (controller.RootForm == null)
            {
                throw new InvalidOperationException ("You must have a root form set for the NavigationController.");
            }
        }

        #endregion

        #region Constructors

        protected NavigationControllerBase ()
        {
            MdiChildFormPopCallbacks = new Dictionary<T, EventHandler<JcwEventArgs<T>>> ();
            NavigationStack = new Stack<T> ();
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose ()
        {
            foreach (T form in NavigationStack)
            {
                form.Dispose ();
            }

            MdiChildFormPopCallbacks.Clear ();
            NavigationStack.Clear ();
        }

        #endregion

        #region INavigationController Implementation

        public EventHandler ApplicationStartCallback { protected get; set; }

        public Bitmap Artwork { protected get; set; }

        private T rootForm;
        public T RootForm
        {
            protected get { return rootForm; }
            set
            {
                if (Artwork != null)
                {
                    MdiParentForm.Artwork = Artwork;
                }

                rootForm = value;

                PushInternal (rootForm, null);

                JcwSingleton.Run (MdiParentForm, ApplicationStartCallback);
            }
        }

        public void Push (Type formType)
        {
            ValidateControllerState (instance);

            T form = (T)Factory.Instance.Create (formType, null, null);
            PushInternal (form, null);
        }

        public void Push (Type formType, EventHandler<JcwEventArgs<T>> popCallback, Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors)
        {
            ValidateControllerState (instance);

            T form = (T)Factory.Instance.Create (formType, propertyDescriptors);
            PushInternal (form, popCallback);
        }

        public void Pop ()
        {
            ValidateControllerState (instance);

            if (NavigationStack.Count > 1)
            {
                PopInternal ();

                // Display the previous form from the navigation stack.
                T previousForm = NavigationStack.Peek ();
                DisplayForm (previousForm);
            }
        }

        public void PopToRoot ()
        {
            ValidateControllerState (instance);

            // Display the root form which is the only one left in the navigation stack.
            DisplayForm (RootForm);

            // Remove all forms from the navigation stack except for the root form.
            while (NavigationStack.Count > 1)
            {
                PopInternal ();
            }
        }

        #endregion

        #region Private Methods

        private void DisplayForm (T form)
        {
            MdiParentForm.SetForm (form);
        }

        private void PushInternal (T form, EventHandler<JcwEventArgs<T>> popCallback)
        {
            NavigationStack.Push (form);
            if (popCallback != null)
            {
                MdiChildFormPopCallbacks.Add (form, popCallback);
            }

            DisplayForm (form);
        }

        private void PopInternal ()
        {
            // Remove the current form from the navigation stack and dispose of it.
            T form = NavigationStack.Pop ();

            // If the form being popped set a pop callback when it was pushed onto the navigation
            // stack, execute the callback now.
            if (MdiChildFormPopCallbacks.ContainsKey (form))
            {
                EventHandler<JcwEventArgs<T>> handler = MdiChildFormPopCallbacks[form];
                if (handler != null)
                {
                    handler (this, new JcwEventArgs<T> (form));
                }
            }

            // Dispose of all forms that are popped except for the root form.
            if (form != RootForm)
            {
                form.Dispose ();
            }
        }

        #endregion
    }

    public class NavigationController : NavigationControllerBase<JcwBaseNavigationFrm>
    {
        private static readonly object padlock = new object ();
        public static NavigationControllerBase<JcwBaseNavigationFrm> Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NavigationController ();
                    }
                    return instance;
                }
            }
        }

        public NavigationController ()
        {
            MdiParentForm = new NavigationMdiParentFrm ();
        }
    }
}