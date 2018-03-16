namespace Aglive.Business.Infrastructure.Utilities
{
    using System;

    public interface IDeviceUtility
    {
        string DeviceType { get; }
        bool IsTablet { get; }
        string AppVersion { get; }
        string DeviceIdentity { get; }

        DeviceOrientations CurrentOrientation { get; }

        void SubscribeNotification();

        Action<Action> RunOnUIThread { get; set; }

        void SetLanguages(Language language);
    }
}