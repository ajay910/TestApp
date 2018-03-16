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
using Ninject;

namespace TestApp.Droid.Utilities
{
   public class Dependencies
    {
        #region Public Properties

        public static StandardKernel Container
        {
            get; private set;
        }

        #endregion Public Properties

        #region Constructors

        static Dependencies()
        {
            Container = new StandardKernel(new DeviceDependencies(), new BusinessDependencies());
        }

        #endregion Constructors
    }
}