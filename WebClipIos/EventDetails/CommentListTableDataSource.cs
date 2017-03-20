using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using WeClip.Core.Model;

namespace WebClipIos
{
	public class CommentListTableDataSource : UITableViewSource
	{
		
		List<EventFeedModel> Item = new List<EventFeedModel>();
	
		public CommentListTableDataSource(List<EventFeedModel> item)
		{
			Item = item;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			
				var cell = tableView.DequeueReusableCell("EventDetaisCommentTableViewCell") as EventDetaisCommentTableViewCell;
				
				cell.UpdateCell(
				comment: Item[indexPath.Row].Message,
				commentDate: Item[indexPath.Row].FeedDate.ToString(),
				commentUserName: Item[indexPath.Row].UserName,
				hostName:Item[indexPath.Row].EventCreaterName,
				imageUrl:Item[indexPath.Row].UserProfilePicUrl
					
				);
				tableView.RowHeight = 100;
				return cell;

		}



		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return Item.Count;
			//throw new NotImplementedException();
		}
	}
}
