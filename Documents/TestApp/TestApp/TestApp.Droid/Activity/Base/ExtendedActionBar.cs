using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace TestApp.Droid
{
    public abstract class ExtendedActionBarBase : AppCompatActivity
    {
        private bool _canShowActionbar = true;
        private string _actionbarTitle;

        protected bool CanShowActionbar
        {
            get
            {
                return _canShowActionbar;
            }
            set
            {
                if (value)
                    SupportActionBar?.Show();
                else
                    SupportActionBar?.Hide();
                _canShowActionbar = value;
            }
        }

        protected string ActionbarTitle
        {
            get
            {
                return _actionbarTitle;
            }
            set
            {
                _actionbarTitle = value;
                var title = FindViewById<TextView>(Resource.Id.ActionbarTitle);
                if (title != null)
                    title.Text = _actionbarTitle;
            }
        }

        //protected void SetRightButtonIcon(int resId)
        //{
        //    var rightButton = SupportActionBar.CustomView.FindViewById<ImageView>(Resource.Id.RightButton);
        //    rightButton.SetImageResource(resId);
        //}

        #region Private Methods

        protected void SetCustomActionBar()
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                toolbar.SetPadding(0, GetStatusBarHeight(), 0, 0);
                SetSupportActionBar(toolbar);

                ActionbarTitle = Title;
            }
        }



        protected int GetStatusBarHeight()
        {
            var result = 0;
            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            //{
            //    var resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            //    if (resourceId > 0)
            //    {
            //        result = Resources.GetDimensionPixelSize(resourceId);
            //    }
            //}
            return result;
        }

        #endregion Private Methods
    }
}