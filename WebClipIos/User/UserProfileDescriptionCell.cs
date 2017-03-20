using Foundation;
using System;
using UIKit;
using FFImageLoading.Work;
using FFImageLoading;

namespace WebClipIos
{
    public partial class UserProfileDescriptionCell : UITableViewCell
    {
        public UserProfileDescriptionCell (IntPtr handle) : base (handle)
        {
        }


		public void updateCell(string bio, string emailid, string totalEvents, string userName,string followers,string following, string picUrl)
		{

			lbl_Bio.Text = bio;
			lbl_Email.Text = emailid;
			bttn_Events.SetTitle(totalEvents.ToString(),UIControlState.Normal);
			lbl_FullName.Text = userName;
			bttn_Followers.SetTitle(followers.ToString(),UIControlState.Normal);
			bttn_Following.SetTitle(following.ToString(), UIControlState.Normal);

			ImageService.Instance.LoadUrl(picUrl)
					.ErrorPlaceholder("DefaultContactImage.png", ImageSource.ApplicationBundle)
					.LoadingPlaceholder("DefaultContactImage.png", ImageSource.CompiledResource)
						.Into(ProfileImage);
		}
    }
}