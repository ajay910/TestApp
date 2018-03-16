using Android.App;
using Android.Widget;
using System;
using System.Collections.Specialized;

namespace TestApp.Droid
{
    [Activity(Label = "@string/app_name")]
    public class MainActivity : BaseActivity
    {
        protected override int LayoutId => Resource.Layout.Main;
        
        private Button _vehicleList, _addNew;
        private TextView _totalVehicles;
        protected override void InitializeUI()
        {
            base.InitializeUI();
            _vehicleList = FindViewById<Button>(Resource.Id.VehicleList);
            _totalVehicles = FindViewById<TextView>(Resource.Id.TotalVehicles);
            _addNew = FindViewById<Button>(Resource.Id.AddNew);
        }

        protected override void InitializeEvents()
        {
            base.InitializeEvents();
            _vehicleList.Click += VehicleClick;
            _addNew.Click += AddNewClick;
            FirebaseDBContext.Current.List.CollectionChanged += NotifyCollectionChangedEventHandler;
        }

        protected override void BindData()
        {
            base.BindData();
            SetTotalVehicles();
        }

        private void VehicleClick(object sender, EventArgs e)
        {
            StartActivity(typeof(UserListActivity));
            //await FirebaseDBContext.Current.InsertVehicle(Guid.NewGuid());
            //Button button = FindViewById<Button>(Resource.Id.myButton);
            //button.Text = "Row inserted " + FirebaseDBContext.Current.List.Count;
        }

        private void AddNewClick(object sender, EventArgs e)
        {
            StartActivity(typeof(NewUserActivity));
            //await FirebaseDBContext.Current.InsertVehicle(Guid.NewGuid());
            //Button button = FindViewById<Button>(Resource.Id.myButton);
            //button.Text = "Row inserted " + FirebaseDBContext.Current.List.Count;
        }

        private void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetTotalVehicles();
        }

        private void SetTotalVehicles()
        {
            RunOnUiThread(() =>
            {
                _totalVehicles.Text = "Total Vehicles : " + FirebaseDBContext.Current.List.Count;
            });
        }
    }
}

