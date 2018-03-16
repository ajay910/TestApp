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
using Ninject.Modules;
using TestApp.Utilities;

namespace TestApp.Droid.Utilities
{
    public class DeviceDependencies : NinjectModule
    {
        public override void Load()
        {
            Bind<IPathProvider>().To<PathProvider>().InSingletonScope();
        }
    }
}