using System;
using Foundation;
using UIKit;

// Code in this example adopted from http://waynehartman.com/posts/how-to-do-ntlm-authentication-in-ios.html
namespace NTLMExample.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            // Pass in your ULR instead of "someurl". 
            NSUrlRequest request = new NSUrlRequest(NSUrl.FromString("someurl"));
            NSUrlConnection connection = NSUrlConnection.FromRequest(request, new NativeUrlDelegate());

        }            
    }
    // For a more complete Delegate implementation, take a look at this sample: https://github.com/xamarin/monotouch-samples/blob/master/CustomInputStream/NativeUploader.cs#L97
    public class NativeUrlDelegate : NSUrlConnectionDataDelegate
    {
        public override void ReceivedAuthenticationChallenge(NSUrlConnection connection, NSUrlAuthenticationChallenge challenge)
        {
            if (challenge.ProtectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodNTLM)
            {
                // Important to check! You could end up locking out your user based on your security polices after entering wrong credentials too many times!
                if (challenge.PreviousFailureCount > 0)
                {
                    challenge.Sender.CancelAuthenticationChallenge(challenge);
                    new UIAlertView("Invalid Credentials", "The credentials you saved for your account are invalid", null, "Ok").Show();
                }
                else
                {
                    // Credentials are hardcoded. Instead, you may want to show some UI to the user to enter them, etc.
                    NSUrlCredential credentials = new NSUrlCredential("username", "password", NSUrlCredentialPersistence.ForSession);
                    challenge.Sender.UseCredential(credentials, challenge);
                }
            }
            else
            {
                // Do whatever you want when it's not NTLM. For this sample, we just cancel the challenge.
                challenge.Sender.CancelAuthenticationChallenge(challenge);
            }
        }
    }
}

