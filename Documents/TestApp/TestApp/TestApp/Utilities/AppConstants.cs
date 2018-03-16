namespace Aglive.Business.Infrastructure.Utilities
{
    using Model.ViewModel;

    public static class AppConstants
    {
        public const string AppNameOnDisk = "eNVDPro";

        public const string MultipartContentBoundary = "-------------------------acebdf13572468";

        public const int BackgroundPollingFrequencySecs = 0;

        public static class Folders
        {
            public const string Documents = "Documents";
            public const string Images = "Images";
            public const string Temp = "Temp";
        }

        public static class Files
        {
            public const string MainDatabase = "AppDatabase.db3";
            public const string DefaultDatabaseName = "IntegriData.db3";
            public const string TermsOfService = "termsofservice.html";
        }

        public static class ContactUs
        {
            public const string Telephone = "1 300 893 531";
            public const string Email = "support@aglive.com";
            public const string Website = "www.aglive.com";
            public const string Abn = "32 111 343 670";
            public const string Address = "Suite 1A, Balyang Business Park, 45 Riversdale road, Newtown, 3220 VIC";
        }

        public static class Color
        {
            public static ColorRGB White = new ColorRGB(255, 255, 255);
            public static ColorRGB Purple = new ColorRGB(100, 99, 138);
            public static ColorRGB Orange = new ColorRGB(254, 195, 79);
            public static ColorRGB Cyan = new ColorRGB(136, 209, 209);
            public static ColorRGB Green = new ColorRGB(95, 171, 120);
            public static ColorRGB Gray = new ColorRGB(153, 153, 137);
            public static ColorRGB StrongRed = new ColorRGB(195, 95, 72);
            public static ColorRGB LightRed = new ColorRGB(255, 144, 107);
            public static ColorRGB DarkGreen = new ColorRGB(118, 146, 119);
            public static ColorRGB DarkBlue = new ColorRGB(55, 68, 75);
        }
    }
}