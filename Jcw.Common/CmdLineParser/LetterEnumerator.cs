using System;

namespace Jcw.Common.CmdLineParser
{
	internal sealed class LetterEnumerator
    {
        #region Fields

        private string m_currentElement;
        private string m_data; 
        private int m_index;

        #endregion

        #region Properties

        public string Current
		{
            get
            {
                if ( m_index == -1 )
                {
                    throw new InvalidOperationException ();
                }

                if ( m_index >= m_data.Length )
                {
                    throw new InvalidOperationException ();
                }

                return m_currentElement;
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

                if ( m_index > m_data.Length )
                {
                    throw new InvalidOperationException ();
                }

                if ( IsLast )
                {
                    return null;
                }

                return m_data.Substring ( m_index + 1, 1 );
            }
		}

        public bool IsLast
        {
            get { return ( m_index == m_data.Length - 1 ); }
        }

        #endregion

        #region ctors

        public LetterEnumerator ( string value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException ( "value" );
            }
            
            m_data = value;
            m_index = -1;
        }

        #endregion

        #region Methods

        public bool MoveNext ()
        {
            if ( m_index < ( m_data.Length - 1 ) )
            {
                m_index++;
                m_currentElement = m_data.Substring ( m_index, 1 );
                return true;
            }

            m_index = m_data.Length;
            return false;
        }

        public string SubstringFromNext ()
        {
            if ( m_index == -1 )
            {
                throw new InvalidOperationException ();
            }

            if ( m_index > m_data.Length )
            {
                throw new InvalidOperationException ();
            }

            return m_data.Substring ( m_index + 1 );
        }

        #endregion
    }
}
