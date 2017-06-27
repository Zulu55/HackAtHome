﻿using Android.App;
using Android.Widget;
using Android.OS;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Login);
        }
    }
}
