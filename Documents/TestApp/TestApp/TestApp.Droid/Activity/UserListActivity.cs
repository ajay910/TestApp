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
using TestApp.Repository;

namespace TestApp.Droid
{
    [Activity(Label = "User List")]
    public class UserListActivity : BaseActivity
    {
        protected override int LayoutId => Resource.Layout.UserList;

        private ListView _userList;
        private Button _addButton;
        private UserListAdapter _adpter;

        [Inject]
        public IUserRepository UserRepository { get; set; }

        protected override void InitializeUI()
        {
            base.InitializeUI();
            _userList = FindViewById<ListView>(Resource.Id.UserList);
            _addButton = FindViewById<Button>(Resource.Id.AddNew);
        }

        protected override void InitializeEvents()
        {
            base.InitializeEvents();
            _addButton.Click += AddButtonClick;
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewUserActivity));
            StartActivityForResult(intent, 1);
        }

        protected override void BindData()
        {
            base.BindData();
            BindList();
        }

        private async void BindList()
        {
            var data = await UserRepository.GetAll();
            RunOnUiThread(() =>
            {
                _adpter = new UserListAdapter(this, data);
                _adpter.EditEvent = EditEvent;
                _adpter.DeleteEvent = DeleteEvent;
                _userList.Adapter = _adpter;
            });
        }

        private void EditEvent(object sender, EventArgs e)
        {
            var position = (int)sender;
            var item = _adpter.Items[position];

            var intent = new Intent(this, typeof(NewUserActivity));
            intent.PutExtra("id", item.Id);
            StartActivityForResult(intent, 2);
        }

        private async void DeleteEvent(object sender, EventArgs e)
        {
            var position = (int)sender;

            var item = _adpter.Items[position];
            await UserRepository.Delete(item.Id);
            _adpter.Items.RemoveAt(position);
            _adpter.NotifyDataSetChanged();
            Toast.MakeText(this, "User successfully deleted.", ToastLength.Long).Show();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                if (requestCode == 1 || requestCode == 2)
                {
                    BindList();
                }
            }
        }
    }
}