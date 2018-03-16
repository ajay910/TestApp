using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Ninject;
using TestApp.Domain;
using TestApp.Repository;

namespace TestApp.Droid
{
    [Activity(Label = "New User")]
    public class NewUserActivity : BaseActivity
    {
        [Inject]
        public IUserRepository UserRepository { get; set; }

        protected override int LayoutId => Resource.Layout.NewUser;
        private EditText _name, _email, _password;
        private Button _save;
        private Users _data;
        private TextView PasswordLength, AlphaNumeric, UpperCase, LowerCase, SpecialCase, SequenceCase;
        protected override void InitializeUI()
        {
            base.InitializeUI();
            _name = FindViewById<EditText>(Resource.Id.Name);
            _email = FindViewById<EditText>(Resource.Id.Email);
            _password = FindViewById<EditText>(Resource.Id.Password);
            _save = FindViewById<Button>(Resource.Id.Save);
            PasswordLength = FindViewById<TextView>(Resource.Id.PasswordLength);
            AlphaNumeric = FindViewById<TextView>(Resource.Id.AlphaNumeric);
            UpperCase = FindViewById<TextView>(Resource.Id.UpperCase);
            LowerCase = FindViewById<TextView>(Resource.Id.LowerCase);
            SpecialCase = FindViewById<TextView>(Resource.Id.SpecialCase);
            SequenceCase = FindViewById<TextView>(Resource.Id.SequenceCase);
            HideAllValidationMessage();
        }

        private void HideAllValidationMessage()
        {
            PasswordLength.Visibility = ViewStates.Gone;
            AlphaNumeric.Visibility = ViewStates.Gone;
            UpperCase.Visibility = ViewStates.Gone;
            LowerCase.Visibility = ViewStates.Gone;
            SpecialCase.Visibility = ViewStates.Gone;
            SequenceCase.Visibility = ViewStates.Gone;
        }

        protected override void InitializeEvents()
        {
            base.InitializeEvents();
            _save.Click += SaveClick;
            _password.TextChanged += PasswordChangedEvent;
        }

        private void PasswordChangedEvent(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_password.Text))
                IsValidPassword();
            else
                HideAllValidationMessage();
        }

        protected async override void BindData()
        {
            base.BindData();
            var id = Intent.GetStringExtra("id");
            _data = await UserRepository.GetById(id);
            if (_data != null)
            {
                ActionbarTitle = "Edit User";
                _name.Text = _data.Name;
                _email.Text = _data.Email;
                _password.Text = _data.Password;
            }
        }


        private async void SaveClick(object sender, EventArgs e)
        {
            if (await IsValid())
            {
                if (_data == null)
                {
                    _data = new Users
                    {
                        Email = _email.Text,
                        Password = _password.Text,
                        Name = _name.Text,
                        Id = Guid.NewGuid().ToString()
                    };
                }
                else
                {
                    _data.Email = _email.Text;
                    _data.Password = _password.Text;
                    _data.Name = _name.Text;
                }

                await UserRepository.InsertOrUpdate(_data);

                Toast.MakeText(this, "User saved successfully.", ToastLength.Long).Show();
                SetResult(Result.Ok);
                Finish();

            }
        }

        private async Task<bool> IsValid()
        {
            if (string.IsNullOrEmpty(_name.Text))
            {
                Toast.MakeText(this, "Please enter name", ToastLength.Long).Show();
                return false;
            }

            if (string.IsNullOrEmpty(_email.Text))
            {
                Toast.MakeText(this, "Please enter email", ToastLength.Long).Show();
                return false;
            }
            else if (!Regex.IsMatch(_email.Text, Constant.EmailValidator, RegexOptions.IgnoreCase))
            {
                Toast.MakeText(this, "Please enter valid email address", ToastLength.Long).Show();
                return false;
            }
            else if (await UserRepository.IsEmailExist(_data == null ? "" : _data.Id, _email.Text))
            {
                Toast.MakeText(this, "User already exist", ToastLength.Long).Show();
                return false;
            }

            HideAllValidationMessage();
            if (string.IsNullOrEmpty(_password.Text))
            {
                Toast.MakeText(this, "Please enter password", ToastLength.Long).Show();
                return false;
            }
            else if (!IsValidPassword())
            {
                Toast.MakeText(this, "Please enter strong password", ToastLength.Long).Show();
                return false;
            }

            return true;
        }

        private bool IsValidPassword()
        {
            var isValid = true;

            if (_password.Text.Length < 5 || _password.Text.Length > 12)
            {
                PasswordLength.Visibility = ViewStates.Visible;
                isValid = false;
            }
            else
            {
                PasswordLength.Visibility = ViewStates.Gone;
            }
            if (!_password.Text.Any(Char.IsLetter) || !_password.Text.Any(Char.IsNumber))
            {
                AlphaNumeric.Visibility = ViewStates.Visible;
                isValid = false;
            }
            else
            {
                AlphaNumeric.Visibility = ViewStates.Gone;
            }

            if (!_password.Text.Any(Char.IsUpper))
            {
                UpperCase.Visibility = ViewStates.Visible;
                isValid = false;
            }
            else
            {
                UpperCase.Visibility = ViewStates.Gone;
            }

            if (!_password.Text.Any(Char.IsLower))
            {
                LowerCase.Visibility = ViewStates.Visible;
                isValid = false;
            }
            else
            {
                LowerCase.Visibility = ViewStates.Gone;
            }

            if (!_password.Text.Any(k => !Char.IsLetterOrDigit(k)))
            {
                SpecialCase.Visibility = ViewStates.Visible;
                isValid = false;
            }
            else
            {
                SpecialCase.Visibility = ViewStates.Gone;
            }

            if (HasCharactersSequence(_password.Text))
            {
                SequenceCase.Visibility = ViewStates.Visible;
                isValid = false;
            }
            else
            {
                SequenceCase.Visibility = ViewStates.Gone;
            }

            return isValid;
        }

        private bool HasCharactersSequence(string password)
        {
            var chars = password.ToLower().ToList();
            var asciiValues = Encoding.ASCII.GetBytes(chars.ToArray());
            for (var i = 0; i < asciiValues.Length; i++)
            {
                if ((i + 1) >= asciiValues.Length)
                    break;

                var firstChar = asciiValues[i];
                var secondChar = asciiValues[i + 1];

                if ((firstChar + 1) == secondChar)
                {
                    return true;
                }
            }
            return false;
        }
    }
}