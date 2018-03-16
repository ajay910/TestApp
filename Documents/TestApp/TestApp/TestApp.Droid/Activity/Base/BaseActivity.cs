using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using System.Collections.Generic;
using System.Linq;
using TestApp.Droid.Utilities;

namespace TestApp.Droid
{
    public abstract class BaseActivity : ExtendedActionBarBase
    {
        protected abstract int LayoutId { get; }

        protected bool ProgressDialogShow;

        protected BaseActivity()
        {
            Dependencies.Container.Inject(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.SetSoftInputMode(SoftInput.StateHidden);
            SetContentView(LayoutId);
            SetCustomActionBar();
            InitializeUI(savedInstanceState);
            ConfigureBindings();
            InitializeEvents();
            BindData();
        }

        #region UI and Events

        protected virtual void InitializeUI(Bundle savedInstanceState)
        {
            InitializeUI();
        }

        protected virtual void InitializeUI()
        {
          
        }

        protected virtual void InitializeEvents()
        {
        }

        protected virtual void ConfigureBindings()
        {
        }

        protected virtual void BindData()
        {
        }

     

        #endregion UI and Events
    }
}