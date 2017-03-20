using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using CoreAnimation;

namespace WebClipIos
{
    public partial class EventImageCollectionViewCell : UICollectionViewCell
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
		public EventImageCollectionViewCell(IntPtr handle) : base (handle)
        {
		}


		protected void UpdateContent()
		{
			ImageService.Instance.LoadUrl(imageURL)
						.ErrorPlaceholder("gradient_background.jpg", ImageSource.ApplicationBundle)
						.LoadingPlaceholder("gradient_background.jpg", ImageSource.CompiledResource)
			            .Into(imageForEventImageCollectionCell);



			CALayer ImageCircle = imageForEventImageCollectionCell.Layer;
			ImageCircle.CornerRadius = 10;
			ImageCircle.MasksToBounds = true;
		}
    }
}