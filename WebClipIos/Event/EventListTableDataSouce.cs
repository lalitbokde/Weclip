using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AssetsLibrary;
using Foundation;
using Photos;
using UIKit;
using WeClip.Core.Model;
using Xamarin.Contacts;
using SDWebImage;

namespace WebClipIos
{
	
	public class EventListTableDataSouce : UITableViewSource
	{
		#region Private Variables
		//private SendInviteTableViewController Controller;
		#endregion

		#region Computed Properties
		private List<EventModel> Items = new List<EventModel>();
		public event EventHandler RowSelectedEvent;
		public event EventHandler CollectionImageSelectedEvent;
		public EventModel selectedItem = new EventModel();
		public long eventId { get; set; }
		public int CurrentClickIndex { get; set; }
		public int SelectedIndexPath { get; set; }

		public string CellID
		{
			get { return "EventCell"; }
		}
		#endregion

		#region Constructors
		public EventListTableDataSouce()
		{
			// Initialize
			Initialize();
		}

		public EventListTableDataSouce(List<EventModel> items)
		{
			//// Initialize
			/// 
			//EventModel _model = new EventModel();
			//items.Add(_model);
			Items = items;
			Initialize();
		}


		private void Initialize()
		{

			// Populate database
			//Items.Add(new SendInviteItem("gradient_background.jpg", "Apurva"));
			//Items.Add(new SendInviteItem("gradient_background.jpg", "Lalit"));
		}
		#endregion

		#region Override Methods
		public override nint NumberOfSections(UITableView tableView)
		{
			// Hard coded 1 section
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Items.Count;
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





		UIImage CreatorImage { get; set; }

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("EventCell") as EventDetailCell;
			//if (indexPath.Row != Items.Count - 1)
			//{
			cell.UpdateCell(Items[indexPath.Row].EventID,
			                Items[indexPath.Row].EventName
							, Items[indexPath.Row].EventDate.ToString(),
							Items[indexPath.Row].creatorname
				                , Items[indexPath.Row].creatorpic, Items[indexPath.Row].listFiles,false
						   );



			cell.EventDetailCellImageSelectedEvent += (sender, e) =>
			{
				if (WebClipIos.SocialConstants.CheckEvent == true)
				{
					

				if (CollectionImageSelectedEvent != null)
					{
						this.eventId = cell.eventid;
						WebClipIos.SocialConstants.CheckEvent = false;

						CollectionImageSelectedEvent(this, EventArgs.Empty);
					}
					WebClipIos.SocialConstants.CheckEvent = false;
				}
			};
			cell.bttn_click.TouchUpInside += (sender, e) =>
			{
				CurrentClickIndex = indexPath.Row;
				if (RowSelectedEvent != null)
				{
					this.selectedItem = Items[indexPath.Row];
					RowSelectedEvent(this, EventArgs.Empty);

				}
			};

			
			return cell;
		}


		public EventModel GetItem(int index)
		{
			return Items[index];
		}



		#endregion

	}
	
}
