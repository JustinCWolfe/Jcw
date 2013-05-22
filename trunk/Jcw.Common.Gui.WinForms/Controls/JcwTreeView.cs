using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Jcw.Common;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwTreeView : TreeView, IJcwTreeCtl<JcwTreeNode>
    {
        #region Fields

        private bool m_isInitialized = false;

        #endregion

        #region Constructors

        public JcwTreeView ()
            : base ()
        {
            this.SuspendLayout ();
            // 
            // JcwTreeView
            // 
            this.AfterCheck += new System.Windows.Forms.TreeViewEventHandler (JcwTreeView_AfterCheck);
            this.BackColor = Jcw.Common.JcwStyle.JcwStyleControlBackColor;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            this.Layout += new System.Windows.Forms.LayoutEventHandler (JcwTreeView_Layout);
            this.Padding = new System.Windows.Forms.Padding (3);
            this.VisibleChanged += new System.EventHandler (JcwTreeView_VisibleChanged);
            this.ResumeLayout (false);
        }

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);
        }

        #endregion

        #region Static Methods

        private static void ToggleTreeNode (TreeNode currentNode, bool isChecked)
        {
            if (currentNode.Checked != isChecked)
                currentNode.Checked = isChecked;
        }

        #endregion

        #region Virtual Methods

        protected virtual void InitializeTreeView ()
        {
            m_isInitialized = true;
        }

        #endregion

        #region Overrides

        protected override void WndProc (ref Message m)
        {
            switch (m.Msg)
            {
                // disable double click expand/collapse of treeview
                case (int)NativeMethods.WindowMessages.WM_LBUTTONDBLCLK:
                    break;
                default:
                    base.WndProc (ref m);
                    break;
            }
        }

        #endregion

        #region Event Handlers

        private void JcwTreeView_VisibleChanged (object sender, EventArgs e)
        {
            if (m_isInitialized == false)
            {
                InitializeTreeView ();

                this.CheckBoxes = m_multiSelect;
                // if we are in multiselect mode (checkboxes) we should hide the selection
                this.HideSelection = m_multiSelect;
            }
        }

        private void JcwTreeView_Layout (object sender, LayoutEventArgs e)
        {
            if (this.DesignMode)
                return;
        }

        private void JcwTreeView_AfterCheck (object sender, TreeViewEventArgs tvea)
        {
            for (int i = 0 ; i < tvea.Node.Nodes.Count ; i++)
                ToggleTreeNode (tvea.Node.Nodes[i], tvea.Node.Checked);

            TreeNode parentNode = tvea.Node.Parent;

            if (parentNode != null)
            {
                this.AfterCheck -= JcwTreeView_AfterCheck;
                ToggleParentNodes (tvea.Node, tvea.Node.Checked);
                this.AfterCheck += JcwTreeView_AfterCheck;
            }
        }

        #endregion

        #region IJcwTreeCtl Implementation

        private bool m_multiSelect = true;
        public bool MultiSelect
        {
            get { return m_multiSelect; }
            set { m_multiSelect = value; }
        }

        public void ClearNodes ()
        {
            this.Nodes.Clear ();
        }

        public void RemoveNode (string nodeText)
        {
            TreeNode[] matchingNodes = this.Nodes.Find (nodeText, true);
            foreach (TreeNode matchingNode in matchingNodes)
                this.Nodes.Remove (matchingNode);
        }

        public JcwTreeNode AddRootNode ()
        {
            JcwTreeNode node = new JcwTreeNode ();
            this.Nodes.Add (node);
            return node;
        }

        public JcwTreeNode AddRootNode (string newNodeText)
        {
            JcwTreeNode node = new JcwTreeNode (newNodeText);
            this.Nodes.Add (node);
            return node;
        }

        public JcwTreeNode AddTreeNode (JcwTreeNode parentNode)
        {
            JcwTreeNode node = new JcwTreeNode ();
            parentNode.Nodes.Add (node);
            return node;
        }

        public JcwTreeNode AddTreeNode (JcwTreeNode parentNode, string newNodeText)
        {
            JcwTreeNode node = new JcwTreeNode (newNodeText);
            parentNode.Nodes.Add (node);
            return node;
        }

        /// <summary>
        /// loop through all nodes to find the one matching node text
        /// </summary>
        /// <param name="nodeText"></param>
        public void ToggleTreeNode (string nodeText, bool isChecked)
        {
            TreeNode[] matchingNodes = this.Nodes.Find (nodeText, true);
            foreach (TreeNode matchingNode in matchingNodes)
                ToggleTreeNode (matchingNode, isChecked);
        }

        /// <summary>
        /// recurse through all child nodes, reporting which are checked
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllCheckedNodeNames ()
        {
            List<string> checkedNodes = new List<string> ();

            for (int i = 0 ; i < this.Nodes.Count ; i++)
            {
                TreeNode currentNode = this.Nodes[i];

                if (currentNode.Checked)
                    checkedNodes.Add (currentNode.Text);

                if (currentNode.Nodes.Count != 0)
                    GetCheckedChildNodeNames (currentNode, ref checkedNodes);
            }

            return checkedNodes;
        }

        /// <summary>
        /// recurse through all child nodes, reporting which are checked
        /// </summary>
        /// <returns></returns>
        public List<JcwTreeNode> GetAllCheckedNodes ()
        {
            List<JcwTreeNode> checkedNodes = new List<JcwTreeNode> ();

            for (int i = 0 ; i < this.Nodes.Count ; i++)
            {
                JcwTreeNode currentNode = this.Nodes[i] as JcwTreeNode;

                if (currentNode.Checked)
                    checkedNodes.Add (currentNode);

                if (currentNode.Nodes.Count != 0)
                    GetCheckedChildNodes (currentNode, ref checkedNodes);
            }

            return checkedNodes;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// backtrack through hierarchy of parent nodes toggling the state of any that do not match the passed-in state
        /// </summary>
        /// <param name="node"></param>
        private void ToggleParentNodes (TreeNode node, bool isChecked)
        {
            TreeNode parentNode = node.Parent;

            // we checked a child node under a parent where all other child nodes are checked, check all parent nodes
            if (isChecked == true)
            {
                // check all children of this parent to see if all other children are already checked - in which case we should check the parent
                bool allChildrenChecked = true;
                foreach (TreeNode childNode in parentNode.Nodes)
                    if (childNode.Checked == false)
                        allChildrenChecked = false;

                if (!allChildrenChecked)
                    return;
            }

            if (parentNode.Checked != isChecked)
                parentNode.Checked = isChecked;

            TreeNode higherLevelParentNode = parentNode.Parent;
            if (higherLevelParentNode != null)
                ToggleParentNodes (higherLevelParentNode, isChecked);
        }

        private void GetCheckedChildNodeNames (TreeNode currentNode, ref List<string> checkedNodes)
        {
            for (int i = 0 ; i < currentNode.Nodes.Count ; i++)
            {
                TreeNode childNode = currentNode.Nodes[i];

                if (childNode.Checked)
                    checkedNodes.Add (childNode.Text);

                if (childNode.Nodes.Count != 0)
                    GetCheckedChildNodeNames (childNode, ref checkedNodes);
            }
        }

        private void GetCheckedChildNodes (TreeNode currentNode, ref List<JcwTreeNode> checkedNodes)
        {
            for (int i = 0 ; i < currentNode.Nodes.Count ; i++)
            {
                TreeNode childNode = currentNode.Nodes[i];

                if (childNode.Checked)
                    checkedNodes.Add (childNode as JcwTreeNode);

                if (childNode.Nodes.Count != 0)
                    GetCheckedChildNodes (childNode, ref checkedNodes);
            }
        }

        #endregion
    }

    public class JcwTreeNode : TreeNode
    {
        #region Constructors

        public JcwTreeNode ()
            : base ()
        {
        }

        public JcwTreeNode (string nodeText)
            : base (nodeText)
        {
            this.Name = nodeText;
            this.Text = nodeText;
        }

        #endregion
    }

    public interface IJcwTreeCtl<T>
    {
        bool MultiSelect
        {
            get;
            set;
        }

        void ClearNodes ();
        void RemoveNode (string nodeText);
        T AddRootNode ();
        T AddRootNode (string newNodeText);
        T AddTreeNode (T parentNode);
        T AddTreeNode (T parentNode, string newNodeText);
        void ToggleTreeNode (string nodeText, bool isChecked);
        List<string> GetAllCheckedNodeNames ();
        List<T> GetAllCheckedNodes ();
    }
}
