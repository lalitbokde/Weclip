using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using WeClip.Core.Model;

namespace WebClipIos 
{
	public class EventListForSearchSource : UITableViewSource
	{
		private List<EventModel> Items = new List<EventModel>();
		private List<EventModel> SearchItems = new List<EventModel>();
		public event EventHandler RowSelectedEvent;
		public long eventId { get; set; }
		 public EventModel selectedItem = new EventModel();
		public EventListForSearchSource(List<EventModel> items)
		{
			this.Items = items;
			this.SearchItems = items;
		}

		public override nint NumberOfSections(UITableView tableView)
		 {
            return 1;
       	 }

        public override nint RowsInSection(UITableView tableview, nint section)
		{
			return SearchItems.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (RowSelectedEvent != null)
			{
				this.selectedItem = Items[indexPath.Row];
				RowSelectedEvent(this, EventArgs.Empty);
			}
			tableView.DeselectRow(indexPath, true);
		}


		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell =tableView.DequeueReusableCell("EventShortDescCell") as EventListShortDescriptionCell;
			cell.UpdateCell(SearchItems[indexPath.Row].EventName
							,
							SearchItems[indexPath.Row].creatorname
								, SearchItems[indexPath.Row].creatorpic, false
						   );
			return cell;
		}

		public void PerformSearch(string searchText)
		{
			searchText = searchText.ToLower();
			this.SearchItems = Items.Where(x => x.EventName.ToLower().Contains(searchText)).ToList();
		}
	}
}
