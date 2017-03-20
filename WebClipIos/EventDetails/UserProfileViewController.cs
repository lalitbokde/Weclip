using Foundation;
using System;
using UIKit;
using WeClip.Core.Model;
using System.Collections.Generic;
using WebClip.IOS.PCL;
using System.Linq;
using CoreAnimation;
using FFImageLoading;
using FFImageLoading.Work;

namespace WebClipIos
{
    public partial class UserProfileViewController : UIViewController
    {
		partial void Btn_editProfile_TouchUpInside(UIButton sender)
		{
			this.Dispose(true);
			var Controller = (ProfileViewController)this.Storyboard.InstantiateViewController("ProfileViewController");
			NavigationController.PushViewController(Controller, true);
		}


		EventListTableDataSouce tableSource { get; set; }
		List<EventModel> _listEvent = new List<EventModel>();
		UserProfile _ObjUserProfileResult = null;
		partial void EventTypeSelectValueChanges(UISegmentedControl sender)
		{
			if (EventSelectorSegment.SelectedSegment == 0)
			{
				EventTable.Hidden = true;
			}
			else
			{
				EventTable.Hidden = false;
				getPrivateEventData();
			}
		}

		public UserProfileViewController (IntPtr handle) : base (handle)
		{
			
        }

		public override void ViewDidLoad()
		{
			EventSelectorSegment.SelectedSegment = 0;
			CALayer profileImageCircle = ProfileImage.Layer;
			profileImageCircle.CornerRadius = 40;
			profileImageCircle.MasksToBounds = true;

			lbl_FullName.Text = LoginUserDataModel.UserName;
			lbl_Email.Text = LoginUserDataModel.EmailID;
			lbl_Bio.Text = LoginUserDataModel.Bio;

			getUserProfileDetails();

				
		}

		public async void getUserProfileDetails()
		{
			_ObjUserProfileResult = await UserService.GetUserProfileDetails();
			lbl_Bio.Text = _ObjUserProfileResult.Bio;
			lbl_Email.Text = _ObjUserProfileResult.EmailId;
			lbl_Events.Text = _ObjUserProfileResult.TotalEvents.ToString();
			lbl_FullName.Text = _ObjUserProfileResult.UserName;
			lbl_Followers.Text = _ObjUserProfileResult.Follwers.ToString();
			lbl_Following.Text = _ObjUserProfileResult.Following.ToString();

			ImageService.Instance.LoadUrl(_ObjUserProfileResult.ProfilePic)
					.ErrorPlaceholder("DefaultContactImage.png", ImageSource.ApplicationBundle)
					.LoadingPlaceholder("DefaultContactImage.png", ImageSource.CompiledResource)
			            .Into(ProfileImage);
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

					_listEvent.Clear();

					_ObjEventResult = _ObjEventResult.Where(a => a.EventType == "M").ToList();
					if (_ObjEventResult == null)
					{
						EventTable.Hidden = true;
					}
					else
					{
						EventTable.Hidden = false;

					}

					EventTable.RowHeight = 203;

					tableSource = new EventListTableDataSouce(_ObjEventResult);
					//tblInviteContact.Source = tblInviteContac
					EventTable.Source = tableSource;
					EventTable.ReloadData(); ;
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