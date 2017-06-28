using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using HackAtHome.SAL;
using HackAtHome.CustomAdapters;
using System.Collections.Generic;
using HackAtHome.Entities;
using System.Linq;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", MainLauncher = false, Icon = "@drawable/ic_launcher")]
    public class EvidencesActivity : Activity
    {
        TextView textViewFullName;
        ListView listViewEvidences;
        List<Evidence> evidences;
        string fullName;
        string token;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Evidences);

            textViewFullName = FindViewById<TextView>(Resource.Id.textViewFullName);
            listViewEvidences = FindViewById<ListView>(Resource.Id.listViewEvidences);

            fullName = Intent.GetStringExtra("FullName");
            token = Intent.GetStringExtra("Token");

            LoadEvidences(token);
            textViewFullName.Text = fullName;

            listViewEvidences.ItemClick += ListViewEvidences_ItemClick;
        }

        private void ListViewEvidences_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(EvidenceDetailActivity));
            var evidence = evidences.Where(ev => ev.EvidenceID == e.Id).FirstOrDefault();
            intent.PutExtra("EvidenceId", e.Id.ToString());
            intent.PutExtra("Title", evidence.Title);
            intent.PutExtra("Status", evidence.Status);
            intent.PutExtra("Token", token);
            intent.PutExtra("FullName", fullName);
            StartActivity(intent);
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