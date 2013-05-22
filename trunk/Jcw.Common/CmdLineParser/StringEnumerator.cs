using System;

namespace Jcw.Common.CmdLineParser
{
	internal sealed class StringEnumerator
    {
        #region Fields

        private int m_index;
        private int m_endIndex;
        private string[] m_data;

        #endregion

        #region Properties

        public bool IsLast
        {
            get { return ( m_index == m_endIndex - 1 ); }
        }

		public string Current
		{
            get
            {
                if ( m_index == -1 )
                {
                    throw new InvalidOperationException ();
                }

                if ( m_index >= m_endIndex )
                {
                    throw new InvalidOperationException ();
                }

                return m_data[m_index];
            }
		}

		public string Next
		{
            get
            {
                if ( m_index == -1 )
                {
                    throw new InvalidOperationException ();
                }

                if ( m_index > m_endIndex )
                {
                    throw new InvalidOperationException ();
                }

                if ( IsLast )
                {
                    return null;
                }

                return m_data[m_index + 1];
            }
		}

        #endregion

        public StringEnumerator ( string[] value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException ( "value" );
            }

            m_index = -1;
            m_data = value;            
            m_endIndex = value.Length;
        }

        public bool MoveNext ()
        {
            if ( m_index < m_endIndex )
            {
                m_index++;
                return ( m_index < m_endIndex );
            }

            return false;
        }
	}
}
