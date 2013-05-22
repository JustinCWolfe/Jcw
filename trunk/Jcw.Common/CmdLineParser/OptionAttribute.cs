using System;
using System.Reflection;

namespace Jcw.Common.CmdLineParser
{
    /// <summary>
    /// Models an option specification.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Field )]
    public sealed class CmdLineOptionAttribute : Attribute
    {
        #region Fields

        private string m_uniqueName;
        private string m_shortName;
        private string m_longName;
        private string m_helpText;
        private bool m_required;

        #endregion

        #region Properties

        public string ShortName
        {
            get { return m_shortName; }
        }

        public string LongName
        {
            get { return m_longName; }
        }

        public bool Required
        {
            get { return m_required; }
            set { m_required = value; }
        }

        public string HelpText
        {
            get { return m_helpText; }
            set { m_helpText = value; }
        }

        internal string UniqueName
        {
            get { return m_uniqueName; }
        }

        #endregion

        #region ctors

        public CmdLineOptionAttribute ( string shortName, string longName )
        {
            if ( shortName != null && shortName.Length > 0 )
            {
                m_uniqueName = shortName;
            }

            else if ( longName != null && longName.Length > 0 )
            {
                m_uniqueName = longName;
            }

            if ( m_uniqueName == null )
            {
                throw new InvalidOperationException ();
            }

            this.m_shortName = shortName;
            this.m_longName = longName;
        }

        #endregion

        #region Methods

        internal static CmdLineOptionAttribute FromField ( FieldInfo field )
        {
            object[] attributes = field.GetCustomAttributes ( typeof ( CmdLineOptionAttribute ), false );

            if ( attributes.Length == 1 )
            {
                return (CmdLineOptionAttribute) attributes[0];
            }

            return null;
        }

        #endregion
    }
}
