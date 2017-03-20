using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using CoreAnimation;
using CoreGraphics;

namespace WebClipIos
{
    public partial class ImageViewCell : UICollectionViewCell
    {
		bool tapped;
		  private string _imageURL;
		public UIImageView ImageButton;
		public string imageURL
		{
			set
			{
				if (value != _imageURL)
				{
					_imageURL = value;
					UpdateContent();
				}
			}
			get { return _imageURL; }
		}
        public ImageViewCell (IntPtr handle) : base (handle)
        {
        }


		protected void UpdateContent()
		{
			ImageService.Instance.LoadUrl(imageURL)
						.ErrorPlaceholder("gradient_background.jpg", ImageSource.ApplicationBundle)
						.LoadingPlaceholder("gradient_background.jpg", ImageSource.CompiledResource)
			            .Into(imageView);
			


			CALayer hostedByImageCircle = imageView.Layer;
			hostedByImageCircle.CornerRadius = 10;
			hostedByImageCircle.MasksToBounds = true;
		}


		void TapThat(UITapGestureRecognizer tap)
		{
			
		}
    }
}