using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Org.Apache.Http.Impl.Client;
using Org.Apache.Http.Authentication;
using Org.Apache.Http.Client.Methods;
using Org.Apache.Http.Entity;

namespace NTLMExample.Droid
{
    [Activity(Label = "NTLMExample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
			
            button.Click += delegate
            {
                SendNTLMRequest();
            };
        }

        void SendNTLMRequest()
        {
            DefaultHttpClient client = new DefaultHttpClient();

            // Credentials are hardcoded here
            NTCredentials credentials = new NTCredentials("username", "password", "workstation", "domain");
            client.CredentialsProvider.SetCredentials(AuthScope.Any, credentials);

            HttpPost post = new HttpPost("serviceurl");
            var content = new StringEntity("requestcontent");
            post.Entity = content;

            var response = client.Execute(post);
            int status = response.StatusLine.StatusCode;

            // Check for communication errors
            if (status != 200 && status != 500)
            {          
                Console.WriteLine("Response error {0}", status);
            }
            else
            {
                var responseContent = response.Entity;

                // Do what you want with the content
                var resultStream = responseContent.Content;
            }
        }
    }
}


