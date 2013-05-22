using System.Collections;
using System.Collections.Specialized;

namespace Jcw.Common.CmdLineParser
{
	public abstract class ValueContainer
    {
        private IList m_values;

        public virtual IList Values
        {
            get { return m_values; }
        }

        protected ValueContainer ()
        {
            m_values = new StringCollection ();
        }
	}
}
