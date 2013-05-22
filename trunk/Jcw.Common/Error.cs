namespace Jcw.Common
{
    public static class Error
    {
        private static string m_last;

        public static string LastError
        {
            get { return m_last; }
            set { m_last = value; }
        }
    }
}

