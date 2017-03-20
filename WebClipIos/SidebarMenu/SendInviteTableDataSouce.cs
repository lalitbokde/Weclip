using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Xamarin.Contacts;

namespace WebClipIos
{
	public class SendInviteItem
	{
		#region Computed Properties
		public string ImageName { get; set; } = "";
		public string ContactName { get; set; } = "";
		public string ContactNumber { get; set; } = "";
		#endregion

		#region Constructors
		public SendInviteItem()
		{
		}

		public SendInviteItem(string imageName, string contactName,string contactNumber)
		{
			// Initialize
			this.ImageName = imageName;
			this.ContactName = contactName;
			this.ContactNumber = contactNumber;
		}
		#endregion

	}
	public class SendInviteTableDataSource : UITableViewSource
	{
		#region Private Variables
		//private SendInviteTableViewController Controller;
		#endregion

		#region Computed Properties
		private List<Contact> Items = new List<Contact>();
		private List<Contact> SearchItems = new List<Contact>();

		public string CellID
		{
			get { return "InviteCell"; }
		}
		#endregion

		#region Constructors
		public SendInviteTableDataSource()
		{
			// Initialize
			Initialize();
		}

		public SendInviteTableDataSource(List<Contact> items)
		{
			// Initialize
			SearchItems = items;
			Items = items;
			Initialize();
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

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("InviteCell") as SendInviteTableCell;
			cell.UpdateCell(SearchItems[indexPath.Row].FirstName
				, UIImage.FromFile("DefaultContactImage.png"));
			return cell;
		}

		public void PerformSearch(string searchText)
		{
			searchText = searchText.ToLower();
			this.SearchItems = Items.Where(x => x.FirstName.ToLower().Contains(searchText)).ToList();
		}

		#endregion

	}
	
}
