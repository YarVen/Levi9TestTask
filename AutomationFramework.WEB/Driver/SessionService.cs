namespace AutomationFramework.WEB.Driver
{
    public enum Tools
    {
        Protractor,
        Selenium
    }


    public static class SessionService
    {
        private static Session _session;

        public static Session Session
        {
            get
            {
                if (_session == null)
                    _session = new Session();

                return _session;
            }
        }
    }
}