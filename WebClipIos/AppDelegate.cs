using System;
//using Facebook.CoreKit;
//using Foundation;
//using UIKit;

using Foundation;
using UIKit;
using Facebook.CoreKit;

namespace WebClipIos
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//// class-level declarations

		//string appId = "Your-Id-Here";
		//string appName = "Your_App_Display_Name";

		private bool isAuthenticated = false;

		public UIStoryboard MainStoryboard
		{
			get { return UIStoryboard.FromName("Main", NSBundle.MainBundle); }
		}
		public RootViewController RootViewController { get { return Window.RootViewController as RootViewController; } }
		//public override UIWindow Window
		//{
		//	get;
		//	set;
		//}

		public void SetRootViewController(UIViewController rootViewController, bool animate)
		{
			if (animate)
			{
				var transitionType = UIViewAnimationOptions.TransitionFlipFromRight;

				Window.RootViewController = rootViewController;
				UIView.Transition(Window, 0.5, transitionType,
								  () => Window.RootViewController = rootViewController,
								  null);
			}
			else
			{
				Window.RootViewController = rootViewController;
			}
		}

		public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
		{
			return storyboard.InstantiateViewController(viewControllerName);
		}

		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		// Replace here you own Facebook App Id and App Name, if you don't have one go to
		// https://developers.facebook.com/apps
		string appId = SocialConstants.fbClientId;
		string appName = SocialConstants.appName;


		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			//			// Override point for customization after application launch.
			//			// If not required for your application you can safely delete this method

			//			// Code to start the Xamarin Test Cloud Agent
			////#if ENABLE_TEST_CLOUD
			////			Xamarin.Calabash.Start();
			////#endif

			//			//Window = new UIWindow(UIScreen.MainScreen.Bounds);

			//			//// If you have defined a root view controller, set it here:
			//			//Window.RootViewController = new RootViewController();

			//			//// make the window visible
			//			//Window.MakeKeyAndVisible();

			//			Profile.EnableUpdatesOnAccessTokenChange(true);
			//			Settings.AppID = appId;
			//			Settings.DisplayName = appName;

			if (isAuthenticated)
			{
				//Window = new UIWindow(UIScreen.MainScreen.Bounds);

				//// If you have defined a root view controller, set it here:
				//Window.RootViewController = new RootViewController();

				//// make the window visible
				//Window.MakeKeyAndVisible();

				var tabBarController = GetViewController(MainStoryboard, "RootViewController");
				SetRootViewController(tabBarController, false);
			}
			else
			{
				//User needs to log in, so show the Login View Controlller
				var loginViewController = GetViewController(MainStoryboard, "LoginWithEmailViewController") as LoginWithEmailViewController;
				loginViewController.OnLoginSuccess += LoginViewController_OnLoginSuccess;
				SetRootViewController(loginViewController, false);
			}
			//			//return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
			//			return true;


			Profile.EnableUpdatesOnAccessTokenChange(true);
			Settings.AppID = appId;
			Settings.DisplayName = appName;

			// This method verifies if you have been logged into the app before, and keep you logged in after you reopen or kill your app.
			return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);


		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
			return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		}

		void LoginViewController_OnLoginSuccess(object sender, EventArgs e)
		{
			//Window = new UIWindow(UIScreen.MainScreen.Bounds);

			//// If you have defined a root view controller, set it here:
			//Window.RootViewController = new RootViewController();

			//// make the window visible
			//Window.MakeKeyAndVisible();

			var tabBarController = GetViewController(MainStoryboard, "RootViewController");
			SetRootViewController(tabBarController, true);
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			AppEvents.ActivateApp();
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}

