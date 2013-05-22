using System;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Common.Interfaces
{
    public enum ElementTypes
    {
        Category,
        Data,
        Unset,
    }

    public class JcwComposite : IJcwComposite
    {
        public static string NodeSeparator = "/";

        private ElementTypes m_elementType;
        public ElementTypes ElementType
        {
            get { return m_elementType; }
            set { m_elementType = value; }
        }

        private string m_nodeName;
        public string NodeName
        {
            get { return m_nodeName; }
            set { m_nodeName = value; }
        }

        private string m_nodePath;
        public string NodePath
        {
            get { return m_nodePath; }
        }

        private string m_fullName;
        public string FullName
        {
            get { return m_fullName; }
        }

        private IJcwComposite m_parent;
        public IJcwComposite Parent
        {
            get { return m_parent; }
        }

        private List<IJcwComposite> m_children = new List<IJcwComposite> ();
        public List<IJcwComposite> Children
        {
            get { return m_children; }
        }

        public void AddToParent ( IJcwComposite parent )
        {
            this.m_fullName = parent.FullName + NodeSeparator + parent.NodeName;
            this.m_nodePath = parent.FullName;
            this.m_parent = parent;

            parent.Children.Add ( this );
        }
    }

    public interface IJcwComposite
    {
        ElementTypes ElementType
        {
            get;
            set;
        }

        string NodeName
        {
            get;
            set;
        }

        string NodePath
        {
            get;
        }

        string FullName
        {
            get;
        }

        IJcwComposite Parent
        {
            get;
        }

        List<IJcwComposite> Children
        {
            get;
        }

        void AddToParent ( IJcwComposite parent );
    }
}
