using Foundation;
using System;
using UIKit;
using CoreAnimation;
using WebClip.IOS.PCL;
using FFImageLoading;
using FFImageLoading.Work;

namespace WebClipIos
{
	public partial class MenuController : BaseController
	{
		//public event EventHandler OnLoginSuccess;
		partial void Bttn_LogOut_TouchUpInside(UIButton sender)
		{
			//Create an instance of our AppDelegate
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;

			//Get an instance of our MainStoryboard.storyboard
			var mainStoryboard = appDelegate.MainStoryboard;

			//Get an instance of our Login Page View Controller
			var loginPageViewController = appDelegate.GetViewController(mainStoryboard, "LoginWithEmailViewController") as LoginWithEmailViewController;

			//Wire our event handler to show the MainTabBarController after we successfully logged in.
			loginPageViewController.OnLoginSuccess += (s, e) =>
			{
				var tabBarController = appDelegate.GetViewController(mainStoryboard, "RootViewController");
				appDelegate.SetRootViewController(tabBarController, true);
			};

			//Set the Login Page as our RootViewController
			appDelegate.SetRootViewController(loginPageViewController, true);
		}


		InviteFriendsViewController assignInviteFriendsCaseController { get; set; }
		partial void Bttn_InviteFriends_TouchUpInside(UIButton sender)
		{
			if (NavController.TopViewController as InviteFriendsViewController == null)
			{
				if (assignInviteFriendsCaseController != null)
					assignInviteFriendsCaseController.Dispose();
				assignInviteFriendsCaseController = (InviteFriendsViewController)Storyboard.InstantiateViewController("InviteFriendsViewController");
				assignInviteFriendsCaseController.Title = "Invite Friends";
				NavController.PushViewController(assignInviteFriendsCaseController, false);
			}
			SidebarController.CloseMenu();
		}

		UserSettingsViewController assignUserSettingCaseController { get; set; }
		partial void Bttn_Settings_TouchUpInside(UIButton sender)
		{
			if (NavController.TopViewController as UserSettingsViewController == null)
			{
				if (assignUserSettingCaseController != null)
					assignUserSettingCaseController.Dispose();
				assignUserSettingCaseController = (UserSettingsViewController)Storyboard.InstantiateViewController("UserSettingsViewController");
				assignUserSettingCaseController.Title = "User Setting";
				NavController.PushViewController(assignUserSettingCaseController, false);
			}
			SidebarController.CloseMenu();
		}

		ProfileViewController assignCaseController { get; set; }
		partial void ContentButton_TouchUpInside(UIButton sender)
		{
			if (NavController.TopViewController as ProfileViewController == null)
			{
				if (assignCaseController != null)
					assignCaseController.Dispose();
				assignCaseController = (ProfileViewController)Storyboard.InstantiateViewController("ProfileViewController");

				//var menuController = (MenuController)Storyboard.InstantiateViewController("MenuController");
				assignCaseController.Title = "Edit your profile";

				NavController.PushViewController(assignCaseController, false);
			}
			SidebarController.CloseMenu();
		}

		EventListViewController assignEventListViewController { get; set; }
		partial void Bttn_Home_TouchUpInside(UIButton sender)
		{
			if (NavController.TopViewController as EventListViewController == null)
			{
				if (assignEventListViewController != null)
					assignEventListViewController.Dispose();
				assignEventListViewController = (EventListViewController)Storyboard.InstantiateViewController("EventListViewController");

				//var menuController = (MenuController)Storyboard.InstantiateViewController("MenuController")
				assignEventListViewController.Title = "Home";

				NavController.PushViewController(assignEventListViewController, false);
			}
			SidebarController.CloseMenu();
		}

		public MenuController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			// Make Image Profile Image Circular
			CALayer profileImageCircle = profileimageview.Layer;
			profileImageCircle.CornerRadius = 40;
			profileImageCircle.MasksToBounds = true;

			lbl_ProfileName.Text = LoginUserDataModel.UserName;
			ImageService.Instance.LoadUrl(LoginUserDataModel.ProfilePic)
					.ErrorPlaceholder("DefaultContactImage.png", ImageSource.ApplicationBundle)
					.LoadingPlaceholder("DefaultContactImage.png", ImageSource.CompiledResource)
						.Into(profileimageview);
		}
	}
}