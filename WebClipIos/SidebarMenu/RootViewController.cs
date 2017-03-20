using Foundation;
using System;
using UIKit;
using SidebarNavigation;

namespace WebClipIos
{
    public partial class RootViewController : UIViewController
    {
        private UIStoryboard _storyboard;
		// the sidebar controller for the app
	
		public SidebarNavigation.SidebarController SidebarController { get; private set; }

		// the navigation controller
		public NavController NavController { get; private set; }

		// the storyboard
		public override UIStoryboard Storyboard
		{
			get
			{
				if (_storyboard == null)
					_storyboard = UIStoryboard.FromName("Main", null);
				return _storyboard;
			}
		}

		public RootViewController(IntPtr handle) : base(handle)
		{

		}

		public RootViewController() : base(null, null)
		{
			//this.RestorationIdentifier = "RootViewController";

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var eventListViewController = (EventListViewController)Storyboard.InstantiateViewController("EventListViewController");
			var menuController = (MenuController)Storyboard.InstantiateViewController("MenuController");
		
			// create a slideout navigation controller with the top navigation controller and the menu view controller
			NavController = new NavController();
			NavController.PushViewController(eventListViewController, false);
			SidebarController = new SidebarNavigation.SidebarController(this, NavController, menuController);
			SidebarController.MenuWidth = 220;
			SidebarController.ReopenOnRotate = false;
			SidebarController.MenuLocation = MenuLocations.Left;
		}
    }
}