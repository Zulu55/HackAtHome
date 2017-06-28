using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using HackAtHome.SAL;
using HackAtHome.CustomAdapters;
using System.Collections.Generic;
using HackAtHome.Entities;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", MainLauncher = false, Icon = "@drawable/ic_launcher")]
    public class EvidencesActivity : Activity
    {
        TextView textViewFullName;
        ListView listViewEvidences;
        List<Evidence> evidences;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
            var dataEvidence = (Complex)this.FragmentManager.FindFragmentByTag("DataEvidence");

            if (dataEvidence == null)
            {
                dataEvidence = new Complex();
                var fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Add(dataEvidence, "DataEvidence");
                fragmentTransaction.Commit();
                var client = new ServiceClient();
                evidences = await client.GetEvidencesAsync(token);
                dataEvidence.ListOfEvidences = evidences;
            }
            else
            {
                evidences = dataEvidence.ListOfEvidences;
            }

            listViewEvidences.Adapter = new EvidencesAdapter(
                this,
                evidences,
                Resource.Layout.ListItem,
                Resource.Id.textViewLabName,
                Resource.Id.textViewLabStatus);
        }
    }
}