using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestApp.Domain;

namespace TestApp.Droid
{
    public class UserListAdapter : BaseAdapter<Users>
    {
        public List<Users> Items;

        private readonly Activity _context;
        public EventHandler EditEvent;
        public EventHandler DeleteEvent;

        public UserListAdapter(Activity context, List<Users> list)
        {
            _context = context;
            Items = list;
        }

        public override Users this[int position] => Items[position];

        public override int Count => Items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = _context.LayoutInflater.Inflate(Resource.Layout.UserList_Item, null);

            var data = Items[position];
          
            view.FindViewById<TextView>(Resource.Id.Name).Text = data.Name;
            view.FindViewById<TextView>(Resource.Id.Email).Text = data.Email;
            
            view.FindViewById<ImageView>(Resource.Id.delete).Click += (s, e) =>
            {
                displayConfirmation(position);
            };

            view.Click += (s, e) =>
            {
                EditEvent?.Invoke(position, null);
            };

            return view;
        }

       
        private void displayConfirmation(int position)
        {
            var builder = new AlertDialog.Builder(_context,Resource.Style.AlertDialogTheme);
            builder.SetMessage("Are you sure want to delete?");
            builder.SetPositiveButton("Yes", (s, e) => { DeleteEvent?.Invoke(position, null); });
            builder.SetNegativeButton("No", (s, e) => {  });
            builder.Create().Show();
        }

       
    }
}