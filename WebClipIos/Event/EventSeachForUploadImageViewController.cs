using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using WeClip.Core.Model;
using WebClip.IOS.PCL;
using System.Linq;

namespace WebClipIos
{
    public partial class EventSeachForUploadImageViewController : UIViewController
    {
		EventListForSearchTableDataSouce tableSource { get; set; }

		EventListForSearchSource tableview { get; set; }
		List<EventModel> _listEvent = new List<EventModel>();

        public EventSeachForUploadImageViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			getPrivateEventData();

			SearchEventBar.TextChanged += (sender, e) =>
	  		{
			 searchTable();
	  		};
			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);

		}

		private async void getPrivateEventData()
		{
			try
			{
				List<EventModel> _ObjEventResult = null;
				//api/Account/LoginWithFacebook api calling
				_ObjEventResult = await UserAccountService.GetPublicEvent();

				//redirect to main screen
				if (_ObjEventResult != null)
				{

					_listEvent.Clear();

					_ObjEventResult = _ObjEventResult.ToList();
					if (_ObjEventResult == null)
					{
						tblEventList.Hidden = true;
					}
					else
					{
						tblEventList.Hidden = false;

					}
					_listEvent = _ObjEventResult;
					tblEventList.RowHeight = 78;

					tableview = new EventListForSearchSource(_ObjEventResult);

					tableview.RowSelectedEvent += (sender, e) =>
					{
						EventModel Data = ((EventListForSearchSource)sender).selectedItem;
						var Controller = (ImageUploadOnEventViewController)this.Storyboard.InstantiateViewController("ImageUploadOnEventViewController");
						Controller.data = Data;
						 NavigationController.PushViewController(Controller,true);

					};
					//tblInviteContact.Source = tblInviteContac
					tblEventList.Source =tableview ;
					tblEventList.ReloadData(); 
				}
				else
				{
					new UIAlertView("Error", "No event data found", null,
					"OK", null).Show();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}

	

		private void searchTable()
		{
			
			//perform the search, and refresh the table with the results  
			tableview.PerformSearch(SearchEventBar.Text);
			tblEventList.ReloadData();
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{



		}
    }
}