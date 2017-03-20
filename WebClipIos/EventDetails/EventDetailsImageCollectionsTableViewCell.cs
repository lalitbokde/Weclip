using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using System.Collections.Generic;
using WebClip.IOS.PCL;
using WeClip.Core.Model;

namespace WebClipIos
{
	public partial class EventDetailsImageCollectionsTableViewCell : UITableViewCell
	{
		public EventHandler segmentSelectEvent { get; set; }
		public int selectedSegId { get; set; }
		//public UITableView CommentTable = new UITableView();

		List<EventFeedModel> _listComment = new List<EventFeedModel>();
		CommentListTableDataSource _source { get; set; }

		partial void EventDetailsListTypeSelect(UISegmentedControl sender)
		{
			if (segmentSelectEvent != null)
			{
				if (EventDetailsListTypeSelectSeg.SelectedSegment == 0)
				{
					selectedSegId = 0;
					EventImageCollectionView.Hidden = false;
					CommentListTableView.Hidden = true;

				}
				else if (EventDetailsListTypeSelectSeg.SelectedSegment == 1)
				{
					selectedSegId = 1;
					EventImageCollectionView.Hidden = false;
					CommentListTableView.Hidden = true;
				}
				else 
				{
					selectedSegId = 2;
					EventImageCollectionView.Hidden = true;
					CommentListTableView.Hidden = false;
				}
				segmentSelectEvent(this, EventArgs.Empty);
			}
		}

		private async void loadCommentTable(long eventId)
		{
			_listComment = await EventService.getEventComments(eventId);

		}

		EventDetailsCollectionViewDataSource _dataSource { get; set; }
        public EventDetailsImageCollectionsTableViewCell (IntPtr handle) : base (handle)
        {
			
        }

		public void UpdateCell(List<EventFiles> imageList, List<WeClipVideo> videoList,string Type,long eventid)
		{
			
			_dataSource = new EventDetailsCollectionViewDataSource(imageList,videoList,Type);
		

			EventImageCollectionView.DataSource = _dataSource;
			loadCommentTable(eventid);

			_source = new CommentListTableDataSource(_listComment);
			  

			  CommentListTableView.RegisterNibForCellReuse(UINib.FromName("EventDetaisCommentTableViewCell", null), "EventDetaisCommentTableViewCell");
			this.CommentListTableView.Source = _source;
			CommentListTableView.ReloadData();
			//CommentListTableView.RowHeight = 50;
		}
    }
}