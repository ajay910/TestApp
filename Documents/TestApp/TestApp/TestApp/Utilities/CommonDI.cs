namespace Aglive.Business.Infrastructure.Utilities
{
    using Languages;

    public class CommonDI
    {
        public IDeviceUtility DeviceUtility { get; }
        public ILog Log { get; }
        public ICurrentLanguage Language { get; private set; }

        public CommonDI(IDeviceUtility deviceUtility, ILog log, ICurrentLanguage language)
        {
            DeviceUtility = deviceUtility;
            Log = log;
            Language = language;
        }

        public void ChangeLanguage(ICurrentLanguage language)
        {
            Language = language;
        }
    }
}