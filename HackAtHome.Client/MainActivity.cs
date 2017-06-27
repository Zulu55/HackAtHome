using Android.App;
using Android.Widget;
using Android.OS;
using HackAtHome.SAL;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : Activity
    {
        EditText editTextEmail;
        EditText editTextPassword;
        Button buttonValidate;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Login);

            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            buttonValidate = FindViewById<Button>(Resource.Id.buttonValidate);

            buttonValidate.Click += ButtonValidate_Click;
        }

        async void ButtonValidate_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(editTextEmail.Text))
            {
                ShowMessage(Resources.GetString(Resource.String.Error), 
                            Resources.GetString(Resource.String.NoEmail));
                return;
            }

            if (string.IsNullOrEmpty(editTextPassword.Text))
            {
                ShowMessage(Resources.GetString(Resource.String.Error),
                            Resources.GetString(Resource.String.NoPassword));
                return;
            }

            var client = new ServiceClient();
            var result = await client.AutenticateAsync(editTextEmail.Text, editTextPassword.Text);
            
            if (result.Status != Entities.Status.Success)
            {
                ShowMessage(Resources.GetString(Resource.String.Error),
                            Resources.GetString(Resource.String.UserOrPasswordWrong));
                return;
            }

            ShowMessage("Ok", result.FullName);
        }

        void ShowMessage(string title, string message)
        {
            var builder = new AlertDialog.Builder(this);
            var alert = builder.Create();
            alert.SetTitle(title);
            alert.SetIcon(Resource.Drawable.ic_launcher);
            alert.SetMessage(message);
            alert.SetButton(Resources.GetString(Resource.String.Accept), (s, ev) => { });
            alert.Show();
        }

    }
}