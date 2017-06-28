using Android.App;
using Android.Content;
using Android.OS;
using Android.Webkit;
using Android.Widget;
using HackAtHome.SAL;
using Koush;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", MainLauncher = false, Icon = "@drawable/ic_launcher")]
    public class EvidenceDetailActivity : Activity
    {
        TextView textViewFullName;
        TextView textViewTitle;
        TextView textViewStatus;
        WebView webViewDescription;
        ImageView imageViewImage;
        string fullName;
        string token;
        string title;
        string status;
        int evidenceId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EvidenceDetail);

            textViewFullName = FindViewById<TextView>(Resource.Id.textViewFullName);
            textViewTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            textViewStatus = FindViewById<TextView>(Resource.Id.textViewStatus);
            webViewDescription = FindViewById<WebView>(Resource.Id.webViewDescription);
            imageViewImage = FindViewById<ImageView>(Resource.Id.imageViewImage);

            fullName = Intent.GetStringExtra("FullName");
            token = Intent.GetStringExtra("Token");
            title = Intent.GetStringExtra("Title");
            status = Intent.GetStringExtra("Status");
            var evidenceIdString = Intent.GetStringExtra("EvidenceId");
            evidenceId = int.Parse(evidenceIdString);

            LoadEvidenceDetail();

            textViewFullName.Text = fullName;
            textViewTitle.Text = title;
            textViewStatus.Text = status;
        }

        async void LoadEvidenceDetail()
        {
            var client = new ServiceClient();
            var result = await client.GetEvidenceByIDAsync(token, evidenceId);
            if (result != null)
            {
                webViewDescription.LoadDataWithBaseURL(null, result.Description, "text/html", "utf-8", null);
                UrlImageViewHelper.SetUrlDrawable(imageViewImage, result.Url);
            }
        }
    }
}