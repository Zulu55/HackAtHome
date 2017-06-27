using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using HackAtHome.SAL;
using HackAtHome.CustomAdapters;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", MainLauncher = false, Icon = "@drawable/ic_launcher")]
    public class EvidencesActivity : Activity
    {
        TextView textViewFullName;
        ListView listViewEvidences;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Evidences);

            textViewFullName = FindViewById<TextView>(Resource.Id.textViewFullName);
            listViewEvidences = FindViewById<ListView>(Resource.Id.listViewEvidences);

            var fullName = Intent.GetStringExtra("FullName");
            var token = Intent.GetStringExtra("Token");

            LoadEvidences(token);
            textViewFullName.Text = fullName;
        }

        async void LoadEvidences(string token)
        {
            var client = new ServiceClient();
            var evidences = await client.GetEvidencesAsync(token);
            listViewEvidences.Adapter = new EvidencesAdapter(
                this,
                evidences,
                Resource.Layout.ListItem, 
                Resource.Id.textViewLabName, 
                Resource.Id.textViewLabStatus);
        }
    }
}