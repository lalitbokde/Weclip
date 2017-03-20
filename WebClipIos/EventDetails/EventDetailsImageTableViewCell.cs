using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;

namespace WebClipIos
{
    public partial class EventDetailsImageTableViewCell : UITableViewCell
    {
        public EventDetailsImageTableViewCell (IntPtr handle) : base (handle)
        {
        }

		public void UpdateCell(string LikesCount, string imageUrl)
		{
			
				EventDetailsLikeCountLabel.Text = LikesCount;
				

				ImageService.Instance.LoadUrl(imageUrl)
							.ErrorPlaceholder("gradient_background.png", ImageSource.ApplicationBundle)
							.LoadingPlaceholder("gradient_background.png", ImageSource.CompiledResource)
							.Into(EventDetailsImageIV);

		}
    }
}