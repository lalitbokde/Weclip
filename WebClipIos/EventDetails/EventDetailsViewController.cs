using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;
using WebClip.IOS.PCL;
using WeClip.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace WebClipIos
{
    public partial class EventDetailsViewController : BaseController
    {
		public long eventId { get; set; }

		EventDetailsSource _source { get; set; }

        public EventDetailsViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			getEventDetails();


			
		}

		private async void getEventDetails()
		{
			try
			{
				EventDetails eventDetails=await EventService.getEventDetails(eventId);
				_source = new EventDetailsSource(eventDetails);
				EventDetailsTableView.Source = _source;;
			}
			catch (Exception ex)
			{
				new UIAlertView("Error", "No event data found", null,
					"OK", null).Show();

				
			}
		}



		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "CreateWeClipSegue")
			{
				base.PrepareForSegue(segue, sender);
				var createWeClipViewController = segue.DestinationViewController as CreateWeClipViewController;

				if (createWeClipViewController != null)
				{

						createWeClipViewController.SetTask(this);

					// to be defined on the TaskDetailViewController
				}
				//commentViewController.EventData = this.EventData;
				//NavigationController.PushViewController(commentViewController, true);



			}
		}


    }
}