namespace HackAtHome.Client
{
    using System.Collections.Generic;
    using Android.App;
    using Android.OS;
    using HackAtHome.Entities;

    public class Complex : Fragment
    {
        public List<Evidence> ListOfEvidences { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}