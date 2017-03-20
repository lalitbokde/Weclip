using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using WebClip.IOS.PCL;
using WeClip.Core.Model;


namespace WebClipIos
{
	public class EventDetailsSource : UITableViewSource 
	{
		EventDetails Item = new EventDetails();
		List<EventFiles> listImages = new List<EventFiles>();
		List<WeClipVideo> listVideos = new List<WeClipVideo>();
		//List<Comment> listVideos = new List<WeClipVideo>();
		public EventDetailsSource(EventDetails item)
		{
			Item = item;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row == 0)
			{
				var cell = tableView.DequeueReusableCell("EventDetailsImageTableViewCell") as EventDetailsImageTableViewCell;
				cell.UpdateCell(
					LikesCount: Item.TotalLikes,
					imageUrl: Item.EventPicUrl
				);
				tableView.RowHeight = 230;
				return cell;
			}
			else if (indexPath.Row == 1)
			{
				var cell = tableView.DequeueReusableCell("EventDetailsDescriptionTableViewCell") as EventDetailsDescriptionTableViewCell;
				cell.UpdateCell(
					EventTitle: Item.EventName,
					HostedBy: Item.HostName,
					EventLocation:Item.EventLocation,
					EventDate: Item.EventDate, 
					EventTime: Item.EventStartTime, 
					EventDescription: Item.EventDescription,
					GoingCount: Item.Going,
					MaybeCount: Item.Maybe,
					InvitedCount:Item.Invited
				);
				tableView.RowHeight = 298;
				return cell;
			}
			else
			{	var cell = tableView.DequeueReusableCell("EventDetailsImageCollectionsTableViewCell") as EventDetailsImageCollectionsTableViewCell;
				loadEventImage();
				cell.UpdateCell(
							listImages, null, "Image", Item.Id
					);
				cell.segmentSelectEvent += (sender, e) =>
		{
			if (cell.selectedSegId == 0)
			{
				loadEventImage();
				cell.UpdateCell(
							listImages,null,"Image",Item.Id
					);
			
			}
			else if (cell.selectedSegId == 1)
			{
				loadWeClipdata();
				cell.UpdateCell(null,
listVideos,"Video",Item.Id
					);
						
			}
			else
			{
						cell.UpdateCell(null,
listVideos, "Image", Item.Id
					);
					
						
			}
		};

				tableView.RowHeight = 640;
				return cell;
			}
		}

		private async void loadWeClipdata()
		{
			listVideos = await EventService.getWeClipVideo(Item.Id);

		}

		private async void loadEventImage()
		{
			listImages = await EventService.getEventImages(Item.Id);

		}


		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return 3;
			//throw new NotImplementedException();
		}
	}
}
