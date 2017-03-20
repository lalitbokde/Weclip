using Foundation;
using System;
using UIKit;
using Facebook.LoginKit;
using Facebook.CoreKit;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using WebClip.IOS.PCL;
using Xamarin.Auth;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using WebClipIos.Social;
using WeClip.Core.Model;


namespace WebClipIos
{
	public partial class LoginWithSocialViewController : UIViewController
	{
		partial void Btn_LogInWithGmail_TouchUpInside(UIButton sender)
		{
			var auth = new OAuth2Authenticator(SocialConstants.gClientId, SocialConstants.gClientSecret,
			   "https://www.googleapis.com/auth/userinfo.email", new Uri("https://accounts.google.com/o/oauth2/auth"),
			   new Uri(SocialConstants.gRedirectUri), new Uri("https://accounts.google.com/o/oauth2/token"));

			auth.Completed += (bsender, eventArgs) =>
			{
				// We presented the UI, so it's up to us to dimiss it on iOS
				DismissViewController(true, null);
				if (eventArgs.IsAuthenticated)
				{
					var gPlusAccessToken = eventArgs.Account.Properties["access_token"];

					try
					{
						var gPlusRequests =
							WebRequest.Create(
								string.Format(@"https://www.googleapis.com/plus/v1/people/me?access_token=" +
											  gPlusAccessToken));
						gPlusRequests.ContentType = "application/json";
						gPlusRequests.Method = "GET";

						using (var response = gPlusRequests.GetResponse() as HttpWebResponse)
						{
							Console.Out.WriteLine("Stautus Code is: {0}", response.StatusCode);

							using (var reader = new StreamReader(response.GetResponseStream()))
							{
								var content = reader.ReadToEnd();
								if (!string.IsNullOrWhiteSpace(content))
								{
									Console.Out.WriteLine(content);
								}
								var gPlusResult = JsonConvert.DeserializeObject<RootObjectGooglePlus>(content);
								var gc = new GoogleInfo();
								gc.name = gPlusResult.name.givenName;
								gc.email = gPlusResult.emails[0].value;
								gc.gender = gPlusResult.gender;
								gc.picture = gPlusResult.image.url;

								//for call ap
								googleSocialUser(gc);
							}
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
			};
			PresentViewController(auth.GetUI(), true, null);
		}


		partial void Bttn_LoginWithFacebook_TouchUpInside(UIButton sender)
		{
			//Facebook Login call
			var loginManager = new LoginManager();
			loginManager.LogInWithReadPermissions(SocialConstants.fbPermissionsRead, null, (result, error) =>
			 {
				 if (error != null)
				 {
					 Console.WriteLine(error.ToString());
					 new UIAlertView("Facebook",
						 "We were not able to authenticate you with Facebook. Please login again.", null, "OK", null)
						 .Show();
				 }
				 else if (result.IsCancelled)
				 {
					 Console.WriteLine("Result was cancelled");
					 new UIAlertView("Facebook", "You cancelled the Facebook login process. Please login again.", null,
						 "OK", null).Show();

				 }
				 else if (!result.GrantedPermissions.ToString().Contains("email"))
				 {
					 // Check that we have email as a permission, otherwise show that we can't signup
					 Console.WriteLine("Email permission not found");
					 new UIAlertView("Facebook", "Email permission is required to sign in, Please login again.", null,
						 "OK", null).Show();
					 loginManager.LogOut();
				 }
				 else
				 {
					 var meRequest = new GraphRequest("/me", new NSDictionary("fields", "gender,email,name"), "GET");
					 var requestConnection = new GraphRequestConnection();
					 requestConnection.AddRequest(meRequest, (connection, meResult, meError) =>
					 {
						 if (meError != null)
						 {
							 Console.WriteLine(meError.ToString());
							 new UIAlertView("Facebook", "Unable to login to facebook.", null, "OK", null).Show();
							 return;
						 }

						 var user = meResult as NSDictionary;
						 var fb = new faceBookProfileInfo();
						 fb.ProfileName = user.ValueForKey(new NSString("name")).Description;
						 fb.email = user.ValueForKey(new NSString("email")).Description;
						 fb.gender = user.ValueForKey(new NSString("gender")).Description;
						 fb.Birthdate = null;
						 fb.Password = null;
						 //facebook api
						 fbSocialUser(fb);
					 });
					 requestConnection.Start();

				 }
			 });
		}



		public override void ViewDidLoad()
		{


		}

		private async void googleSocialUser(GoogleInfo gc)
		{
			try
			{
				Token _ObjTokenResult = null;
				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await UserAccountService.googleUserLogin(gc);

				//redirect to main screen
				if (_ObjTokenResult != null && _ObjTokenResult.access_token != null && _ObjTokenResult.Success == true)
				{
					
					LoginUserDataModel.EmailID = _ObjTokenResult.EmailID;
					LoginUserDataModel.UserName = _ObjTokenResult.UserName;
					LoginUserDataModel.UserId = _ObjTokenResult.UserID;
					LoginUserDataModel.AccessToken = _ObjTokenResult.access_token;
					continuetoMainScreen();
				}
				else
				{
					new UIAlertView("Error", _ObjTokenResult.Message, null,
					"OK", null).Show();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		private async void fbSocialUser(faceBookProfileInfo sc)
		{
			try
			{
				Token _ObjTokenResult = null;

				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await UserAccountService.fbUserLogin(sc);

				//redirect to main screen
				if (_ObjTokenResult != null && _ObjTokenResult.access_token != null && _ObjTokenResult.Success == true)
				{

 					LoginUserDataModel.EmailID = _ObjTokenResult.EmailID;
					LoginUserDataModel.UserName = _ObjTokenResult.UserName;
					LoginUserDataModel.UserId = _ObjTokenResult.UserID;
					LoginUserDataModel.AccessToken = _ObjTokenResult.access_token;
					continuetoMainScreen();
				}
				else
				{
					new UIAlertView("Error", _ObjTokenResult.Message, null,
					"OK", null).Show();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		void continuetoMainScreen()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var mainStoryboard = appDelegate.MainStoryboard;
			var rootViewMainController = appDelegate.GetViewController(mainStoryboard, "RootViewController");

			appDelegate.SetRootViewController(rootViewMainController, true);

		}

		//partial void SignInButton_TouchUpInside(UIButton sender)
		//{
		//	var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;

		//	//Get an instance of our MainStoryboard.storyboard
		//	var mainStoryboard = appDelegate.MainStoryboard;

		//	//Get an instance of our Login Page View Controller
		//	var loginPageViewController = appDelegate.GetViewController(mainStoryboard, "LoginWithEmailViewController") as LoginWithEmailViewController;

		//	//Wire our event handler to show the MainTabBarController after we successfully logged in.
		//	loginPageViewController.OnLoginSuccess += (s, e) =>
		//	{
		//		var tabBarController = appDelegate.GetViewController(mainStoryboard, "RootViewController");
		//		appDelegate.SetRootViewController(tabBarController, true);
		//	};

		//	//Set the Login Page as our RootViewController
		//	appDelegate.SetRootViewController(loginPageViewController, true);
		//}

		public LoginWithSocialViewController(IntPtr handle) : base(handle)
		{
		}
	}
}