using Foundation;
using System;
using UIKit;
using WeClip.Core.Model;
using WebClip.IOS.PCL;
using System.Collections.Generic;
using System.Linq;

namespace WebClipIos
{
	public partial class EventListViewController : BaseController
	{
		EventModel EventData=new EventModel();
		partial void EventTypeSelectSegmentValueChanged(UISegmentedControl sender)
		{
			if (EventSelectorSegment.SelectedSegment == 0)
			{
				getPrivateEventData();
			}
			else
			{
				getPublicEventData();
			}
		}


	

		partial void EventTypeSelectEvent(UISegmentedControl sender)
		{
		}

		EventListTableDataSouce tableview { get; set;}
		List<EventModel> _listEvent = new List<EventModel>();
        public EventListViewController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Home";
			getPrivateEventData(); 
		}


		private async void getPublicEventData()
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
					_ObjEventResult = _ObjEventResult.Where(a => a.EventType == "P").ToList();
					if (_ObjEventResult == null)
					{
						EventTable.Hidden = true;
					}
					else
					{
						EventTable.Hidden = false;
					}
					EventTable.RowHeight = 203;
					tableview = new EventListTableDataSouce(_ObjEventResult);
					//tblInviteContact.Source = tblInviteContac

					tableview.CollectionImageSelectedEvent += (sender, e) =>
					{
						long eventId = ((EventListTableDataSouce)sender).eventId;
						var Controller = (EventDetailsViewController)this.Storyboard.InstantiateViewController("EventDetailsViewController");
						Controller.eventId= eventId;
						NavigationController.PushViewController(Controller, true);

					};


					EventTable.Source = tableview;
					EventTable.ReloadData(); 
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

					_ObjEventResult=_ObjEventResult.Where(a=>a.EventType=="M").ToList();
					if (_ObjEventResult == null)
					{
						EventTable.Hidden = true;
					}
					else
					{
						EventTable.Hidden = false;

					}

					EventTable.RowHeight = 203;

					tableview = new EventListTableDataSouce(_ObjEventResult);


					tableview.RowSelectedEvent += (sender, e) =>
					{
						
					};


					tableview.CollectionImageSelectedEvent += (sender, e) =>
					{
						long eventId = ((EventListTableDataSouce)sender).eventId;
						var Controller = (EventDetailsViewController)this.Storyboard.InstantiateViewController("EventDetailsViewController");
						Controller.eventId = eventId;
						NavigationController.PushViewController(Controller, true);
					};
					//tblInviteContact.Source = tblInviteContac
					EventTable.Source = tableview;
					EventTable.ReloadData(); ;
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

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "CommentSegue")
			{
				base.PrepareForSegue(segue, sender);
				var commentViewController = segue.DestinationViewController as CommentViewController;

				if (commentViewController != null)
				{
					var source = EventTable.Source as EventListTableDataSouce;
					EventModel Data;
					int id = 0;
					source.RowSelectedEvent += (senderData, e) =>
					{
						id = ((EventListTableDataSouce)senderData).CurrentClickIndex;
						var item = source.GetItem(id);
						commentViewController.SetTask(this, item);
					};

					// to be defined on the TaskDetailViewController
				}
				//commentViewController.EventData = this.EventData;
				//NavigationController.PushViewController(commentViewController, true);



			}
		}


}
}