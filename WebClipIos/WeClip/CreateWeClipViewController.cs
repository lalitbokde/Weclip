using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using WebClip.IOS.PCL;
using WeClip.Core.Model;

namespace WebClipIos
{
	public partial class CreateWeClipViewController : UIViewController
	{
		public  WeClipInfo _info = new WeClipInfo();



		CreateWeClipImageCollectionViewDataSource _dataSource { get; set; }
		public long eventid {get;set;}
		List<EventFiles> imageList = new List<EventFiles>();

        public CreateWeClipViewController (IntPtr handle) : base (handle)
        {
			//var items = _dataSource.SelectedItems;
        }

		public override void ViewDidLoad()
		{
			
			getEventImageCollection(eventid);
		
		}

		void getEventImageCollection(long eventid)
		{
			loadImageList(eventid);
			_dataSource = new CreateWeClipImageCollectionViewDataSource(imageList, null, "Image");

			EventImageCollection.DataSource = _dataSource;

		}

		internal void SetTask(EventDetailsViewController eventDetailsViewController)
		{
			eventid = eventDetailsViewController.eventId;
		}

		private async void loadImageList(long eventId)
		{
			imageList = await EventService.getEventImages(eventId);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "SelectThemeSegue")
			{
				if (_dataSource.SelectedItems.Length >= 3)
				{
					base.PrepareForSegue(segue, sender);
					var selectThemeViewController = segue.DestinationViewController as SelectThemeViewController;

					if (selectThemeViewController != null)
					{
						_info.EventID = eventid;

						_info.MediaFile = _dataSource.GetSelectedItemList(_dataSource.SelectedItems);
						selectThemeViewController.SendWeClipImageData(_info);


						// to be defined on the TaskDetailViewController
					}
				}
				else
				{
					new UIAlertView("Message", "Select atleast 3 images", null,
					"OK", null).Show();
				}
			}

				//commentViewController.EventData = this.EventData;
				//NavigationController.PushViewController(commentViewController, true);



			}

	}
}