using Foundation;
using System;
using UIKit;

namespace WebClipIos
{
    public partial class SendInviteTableCell : UITableViewCell
    {
		partial void Bttn_invite_TouchUpInside(UIButton sender)
		{
		 string ident=	sender.RestorationIdentifier;

			//new UIAlertView("",ident +" clicked",
			//			null, "Ok", null).Show();
		}

		public void UpdateCell(string name, UIImage image)
		{
			img_profileImage.Image = image;
			lbl_ContactName.Text = name;
			//bttn_invite.SetBackgroundImage(UIImage.FromFile("gradient_background.jpg"), UIControlState.Normal);
			//bttn_invite.SendActionForControlEvents(UIControlEvent.TouchUpInside);
			bttn_invite.RestorationIdentifier = name;
		}

		public SendInviteTableCell(IntPtr handle) : base(handle)
		{
		}

    }
}