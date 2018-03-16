using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestApp.Droid
{
#if DEBUG

    [Application(Debuggable = true, Theme = "@style/Theme.AppTheme")]
#else
    [Application(Debuggable = false, Theme = "@style/Theme.AppTheme")]
#endif
    public class ApplicationStart : Application
    {
        public static ApplicationStart Current { get; private set; }

        public ApplicationStart(IntPtr handle,
          JniHandleOwnership transfer)
           : base(handle, transfer)
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterErrorHandler();
        }

        public void RegisterErrorHandler()
        {
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentOnUnhandledExceptionRaiser;
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private void AndroidEnvironmentOnUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
        }
    }
}