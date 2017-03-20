using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using Contacts;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Contacts;

namespace WebClipIos
{
    public partial class InviteFriendsViewController : BaseController
    {
		List<SendInviteItem> sendInviteItem;
		SendInviteTableDataSource tableSource;

		//List<TableItem> tableItems;
		public InviteFriendsViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			getPhoneContact();


			tblInviteContact.RowHeight = 78;

			   SearchContactBar.TextChanged += (sender, e) =>
		 {
				//this is the method that is called when the user searches  
				searchTable();
		 };

			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);



		}

		private void getPhoneContact()
		{
			

			var book = new Xamarin.Contacts.AddressBook();


			book.RequestPermission().ContinueWith(t =>
			{
				if (!t.Result)
				{
					
					new UIAlertView("Access Denied", "Please allow  access to your contacts to use this feature.",
						null, "Ok", null).Show();
					Console.WriteLine("Permission denied by user or manifest");
					return;
				}
				var contacts =
					book.Where(c => c.Phones.Count() > 0 && c.FirstName != null && c.FirstName != "")
					    .OrderBy(c => c.FirstName).ToList();
				
				tableSource = new SendInviteTableDataSource(contacts);
					//tblInviteContact.Source = tblInviteContact;
					tblInviteContact.Source = tableSource;
					tblInviteContact.ReloadData();



			}, TaskScheduler.FromCurrentSynchronizationContext());

		}

		private void searchTable()
		{
			//perform the search, and refresh the table with the results  
			tableSource.PerformSearch(SearchContactBar.Text);
			tblInviteContact.ReloadData();
		}


	}
}