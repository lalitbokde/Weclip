using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using WeClip.Core.Model;
using WebClip.IOS.PCL;

namespace WebClipIos
{
	public class UserProfileDescriptionTableSource : UITableViewSource
	{
		private List<EventModel> Items = new List<EventModel>();
		UserProfile _ObjUserProfileResult = null;
		public event EventHandler RowSelectedEvent;
		public int CurrentClickIndex { get; set; }
		public int countRefresh { get; set; }

		public UserProfileDescriptionTableSource(List<EventModel> items)
		{
			countRefresh = 0;
			this.Items = items;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			// Hard coded 1 section
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Items.Count + 2;
		}


		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{

			if (indexPath.Row == 0)
			{
				var cell = tableView.DequeueReusableCell("UserProfileDescriptionCell") as UserProfileDescriptionCell;
				getUserProfileDetails();
				cell.updateCell(_ObjUserProfileResult.Bio, _ObjUserProfileResult.EmailId, _ObjUserProfileResult.TotalEvents.ToString(), _ObjUserProfileResult.UserName, _ObjUserProfileResult.Follwers, _ObjUserProfileResult.Following, _ObjUserProfileResult.ProfilePic);

				tableView.RowHeight = 167;
				return cell;
			}
			else if (indexPath.Row == 1)
			{
				var cell = tableView.DequeueReusableCell("UserProfileTabSelectionCell") as UserProfileTabSelectionCell;
				cell.UpdateCell();
				cell.segmentTab.ValueChanged += (sender, e) =>
		{



			if (RowSelectedEvent != null)
			{
				CurrentClickIndex = (int)cell.segmentTab.SelectedSegment;
				//RowSelectedEvent(CurrentClickIndex, EventArgs.Empty);
				tableView.ReloadData();
				countRefresh = 1;
				tableView.BeginUpdates();

						NSIndexPath[] IndexPathsToReload = new NSIndexPath[tableView.IndexPathsForVisibleRows.Length];

				int j = 0;
						foreach (NSIndexPath ind in tableView.IndexPathsForVisibleRows)
				{
					//if (j > 2)
					//{
					//	ind[j] = tableView.IndexPathsForVisibleRows[j];
							
					//		}
				}
				tableView.ReloadRows(tableView.IndexPathsForVisibleRows, UITableViewRowAnimation.Automatic);
				tableView.EndUpdates();

			}

		};

				tableView.RowHeight = 31;

				return cell;
			}
			else
			{
				if (CurrentClickIndex == 1)
				{
					var cell = tableView.DequeueReusableCell("EventCell") as EventDetailCell;
					//if (indexPath.Row != Items.Count - 1)
					//{
					cell.UpdateCell(Items[indexPath.Row - 2].EventID, Items[indexPath.Row - 2].EventName
								, Items[indexPath.Row - 2].EventDate.ToString(),
								Items[indexPath.Row - 2].creatorname
									, Items[indexPath.Row - 2].creatorpic, Items[indexPath.Row - 2].listFiles, false
							   );
					tableView.RowHeight = 203;
					return cell;
				}
				else
				{
					var cell = tableView.DequeueReusableCell("EventCell") as EventDetailCell;
					tableView.RowHeight = 0;
					return cell;
				}
			}
		}

		public async void getUserProfileDetails()
		{
			_ObjUserProfileResult = await UserService.GetUserProfileDetails();

		}


	}
}
