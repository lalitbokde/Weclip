using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using System.Collections.Generic;
using CoreAnimation;

namespace WebClipIos
{
    public partial class EventListShortDescriptionCell : UITableViewCell
    {
        public EventListShortDescriptionCell (IntPtr handle) : base (handle)
        {
        }

		public void UpdateCell(string eventName,  string hostedBy, string hostedByImage, bool hide)
		{
			if (hide == false)
			{
				lblEventTitle.Text=eventName;
				lbl_hostedBy.Text = hostedBy;
			
				ImageService.Instance.LoadUrl(hostedByImage)
							.ErrorPlaceholder("DefaultContactImage.png", ImageSource.ApplicationBundle)
							.LoadingPlaceholder("DefaultContactImage.png", ImageSource.CompiledResource)
							.Into(img_HostedBy);

				CALayer hostedByImageCircle = img_HostedBy.Layer;
				hostedByImageCircle.CornerRadius = 26;
				hostedByImageCircle.MasksToBounds = true;


			}



		}
    }
}