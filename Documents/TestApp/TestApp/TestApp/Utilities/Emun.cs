namespace Aglive.Business.Infrastructure.Utilities
{
    public enum DateTimeFormat
    {
        OnlyDate,
        DateTime,
        DateTimeWithUtc
    }

    public enum MenuItem
    {
        Pending,
        InTransit,
        Received,
        Search,
        ClosestPIC,
        FieldActivity,
        Property,
        Enclosure,
        Mob,
        Envds,
        Weigh,
        Treatment,
        Archive,
        Auction,
        Feed,
        Fleece,
        Induction,
        BuyStock,
        Task
    }

    public enum SidebarItem
    {
        Dashboard,
        SyncData,
        ConnectScanner,
        RecentActivity,
        Settings,
        GetHelp,
        Signout
    }

    public enum PageActionItem
    {
        Edit,
        Delete,
        Print,
        Pickup,
        Deliver,
        Fax,
        Email,
        More,
        Less,
        Fence,
        ViewEnclosures,
        ViewMob,
        SettoDefault,
        Submit,
        Error,
        MoveMob,
        MoveEnclosure,
        StartNVD,
        SplitMob,
        MergeMob,
        Treatment,
        Weight,
        Fleece,
        Photo,
        ViewEID,
        Status
    }

    public enum DeviceOrientations
    {
        Portrait,
        Landscape
    }

    public enum Language
    {
        English,
        Chinese
    }

    public enum BluetoothConnectorType
    {
        Bluetooth = 0,
        Mock = 1
    }
}