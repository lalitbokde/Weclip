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
	
	public class EventListForSearchTableDataSouce : UITableViewDataSource
	{
		#region Private Variables
		//private SendInviteTableViewController Controller;
		#endregion

		#region Computed Properties
		private List<EventModel> Items = new List<EventModel>();
		private List<EventModel> SearchItems = new List<EventModel>();
		

		public string CellID
		{
			get { return "EventCell"; }
		}
		#endregion

		#region Constructors
		public EventListForSearchTableDataSouce()
		{
			// Initialize
			Initialize();
		}

		public EventListForSearchTableDataSouce(List<EventModel> items)
		{
			//// Initialize
			/// 
			//EventModel _model = new EventModel();
			//items.Add(_model);
			Items = items;
			SearchItems = items;
			
			//Initialize();
		}




		private void Initialize()
		{
			
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
			return SearchItems.Count;
		}


		UIImage CreatorImage { get; set; }

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("EventShortDescCell") as EventListShortDescriptionCell;
			//if (indexPath.Row != Items.Count - 1)
			//{
				cell.UpdateCell(SearchItems[indexPath.Row].EventName
							,
							SearchItems[indexPath.Row].creatorname
				                , SearchItems[indexPath.Row].creatorpic,false
						   );
			//}
			//else
			//{
			//	//cell.UpdateCell(""
			//	//			, "",
			//	//			"",""
			//	//                , null,true
			//	//		   );
			//}
			
			return cell;
		}



	



		public void PerformSearch(string searchText)
		{
			searchText = searchText.ToLower();
			this.SearchItems = Items.Where(x => x.EventName.ToLower().Contains(searchText)).ToList();
			//this.Items = Items.Where(x => x.ContactName.ToLower().Contains(searchText)).ToList();
			
		}

		#endregion

	}
	
}
