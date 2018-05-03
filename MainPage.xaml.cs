using App5.UWP.BackgroundTaskHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App5.UWP
{
   
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

          
            LoadApplication(new App5.App());
            BackgroundTasksFactory.RegisterBackgroundTask("MyBackgroundTask", new SystemTrigger(SystemTriggerType.InternetAvailable, true), null);
        }
    }
}
