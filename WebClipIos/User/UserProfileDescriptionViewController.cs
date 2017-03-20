using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using WeClip.Core.Model;
using WebClip.IOS.PCL;
using System.Linq;
using FFImageLoading;
using FFImageLoading.Work;
using CoreAnimation;

namespace WebClipIos
{
    public partial class UserProfileDescriptionViewController : UIViewController
    {   
		
		UserProfileDescriptionTableSource source { get; set; }
		UserProfile _ObjUserProfileResult = null;
        public UserProfileDescriptionViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			CALayer profileImageCircle = UserProfileTableView.Layer;
			profileImageCircle.CornerRadius = 40;
			profileImageCircle.MasksToBounds = true;

			//lbl_FullName.Text = LoginUserDataModel.UserName;
			//lbl_Email.Text = LoginUserDataModel.EmailID;
			//lbl_Bio.Text = LoginUserDataModel.Bio;

			//getUserProfileDetails();

			getPrivateEventData();
			UserProfileTableView.Source = source;
			UserProfileTableView.ReloadData();

			 UserProfileTableView.RowHeight = UITableView.AutomaticDimension;


		}



		private async void getPrivateEventData()
		{
			try
			{
				List<EventModel> _ObjEventResult = null;
				//api/Account/LoginWithFacebook api calling
				_ObjEventResult = await UserAccountService.GetPublicEvent();

				//redirect to main screen
				if (_ObjEventResult != null)
				{

				

					_ObjEventResult = _ObjEventResult.Where(a => a.EventType == "M").ToList();
					if (_ObjEventResult == null)
					{
						UserProfileTableView.Hidden = true;
					}
					else
					{
						UserProfileTableView.Hidden = false;

					}


					source = new UserProfileDescriptionTableSource(_ObjEventResult);
					int selectedSeg=0;
						source.RowSelectedEvent += (senderData, e) =>
				{
						
					selectedSeg = (int)senderData;
					if (selectedSeg == 1)
					{
						UserProfileTableView.Source = source;
						
					}
					else
					{
							UserProfileTableView.Source = null;
					}
						source.CurrentClickIndex = selectedSeg;
					UserProfileTableView.ReloadData();
				};


						UserProfileTableView.ReloadData();
					//tblInviteContact.Source = tblInviteContac

				}
				else
				{
					new UIAlertView("Error", "No event data found", null,
					"OK", null).Show();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}
}
}