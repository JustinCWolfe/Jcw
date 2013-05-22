using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Jcw.Common;
using Jcw.Common.Gui.Interfaces;
using Jcw.Common.Gui.WinForms.Controller;
using Jcw.Common.Gui.WinForms.Controls;

using U = Jcw.Common.Utilities;

namespace Jcw.Common.Gui.WinForms.Forms
{
    public partial class NavigationMdiParentFrm : NavigationMdiParentBaseFrm<JcwBaseNavigationFrm>
    {
        #region Properties

        private JcwToolStripButton BackMenuButton = null;

        #endregion

        #region Constructors

        public NavigationMdiParentFrm ()
        {
            InitializeComponent ();

            BackMenuButton = new JcwToolStripButton ();
            BackMenuButton.Alignment = ToolStripItemAlignment.Right;
            BackMenuButton.Click += new EventHandler (BackMenuButton_Click);
            BackMenuButton.Enabled = true;
            BackMenuButton.Image = (Bitmap)JcwResources.GetObject ("BackButton");
            BackMenuButton.ImageAlign = ContentAlignment.MiddleLeft;
            BackMenuButton.Text = JcwResources.GetString ("BackMenuButtonText");
            BackMenuButton.TextAlign = ContentAlignment.MiddleRight;
            BackMenuButton.Visible = true;

            MdiMenuStrip.Items.Add (BackMenuButton);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
                NavigationController.Instance.Dispose ();

                BackMenuButton.Dispose ();

                if (components != null)
                {
                    components.Dispose ();
                }
            }

            base.Dispose (disposing);
        }

        #endregion

        #region Overrides

        private Bitmap artwork;
        public override Bitmap Artwork
        {
            protected get
            {
                return artwork;
            }
            set
            {
                artwork = value;
                ArtworkPictureBox.Visible = true;
            }
        }

        public override void SetForm(JcwBaseNavigationFrm form)
        {
            Size newClientSize;

            SuspendDrawing ();
            try
            {
                // If there is no child form, since we are adding the first (root) form, set the text for the MDI parent
                // form to be the text from the root form.
                if (ChildForm == null)
                {
                    Text = form.Text;
                }
                // If there already is a visible form hosted in the mdi parent, hide it before showing the new form.
                else
                {
                    ChildForm.Hide ();
                    ChildForm.MdiParent = null;
                }

                ChildForm = form;
                newClientSize = CustomizeForms (ChildForm);
            }
            finally
            {
                ResumeDrawing ();
            }

            // Calculate values needed to center newly sized form.
            int boundWidth = Screen.PrimaryScreen.Bounds.Width;
            int boundHeight = Screen.PrimaryScreen.Bounds.Height;
            int x = boundWidth - newClientSize.Width;
            int y = boundHeight - newClientSize.Height;

            // Set properties on the parent form.
            ClientSize = newClientSize;
            Icon = form.Icon;
            Location = new Point (x / 2, y / 2);

            ChildForm.Show ();
        }

        protected override void JcwBaseFrm_PostLoad(object sender, EventArgs e)
        {
            base.JcwBaseFrm_PostLoad (sender, e);

            Activate ();
        }

        protected override Size CustomizeForms (JcwBaseNavigationFrm form)
        {
            // Set properties on the child form.
            form.Dock = DockStyle.Top;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Location = new Point (0, 0);
            form.MdiParent = this;
            form.Padding = new Padding (0);
            form.StartPosition = FormStartPosition.Manual;
            form.WindowState = FormWindowState.Maximized;

            // For some reason we need an extra 4 pixels here or else we get a horizontal scroll bar.
            int mdiParentWidth = form.ClientSize.Width + 4;
            // For some reason we need an extra 4 pixels here or else we get a vertical scroll bar.
            int mdiParentHeight = form.ClientSize.Height + 4;

            // If the mdi child form can navigate back, the menu strip should be visible with a back button displayed on it.
            MdiMenuStrip.Visible = form.CanNavigateBack;
            if (form.CanNavigateBack)
            {
                mdiParentHeight += MdiMenuStrip.Height;
            }

            if (Artwork != null)
            {
                // Resize the artwork to fit along the bottom edge of the mdi parent form.
                ArtworkPictureBox.Image = U.ResizeImage (mdiParentWidth, Artwork.Height, Artwork);
                ArtworkPictureBox.Size = new Size (mdiParentWidth, Artwork.Height);

                mdiParentHeight += Artwork.Height;
            }

            return new Size (mdiParentWidth, mdiParentHeight);
        }

        #endregion

        #region Event Handlers

        private void BackMenuButton_Click (object sender, EventArgs e)
        {
            NavigationController.Instance.Pop ();
        }

        #endregion
    }

    public abstract class NavigationMdiParentBaseFrm<T> : JcwBaseFixedStyleFrm where T : Form, INavigationBehavior
    {
        public abstract Bitmap Artwork { protected get; set; }
        protected T ChildForm { get; set; }

        public abstract void SetForm (T form);
        protected abstract Size CustomizeForms (T form);
    }
}
