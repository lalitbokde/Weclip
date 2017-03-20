using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using CoreAnimation;

namespace WebClipIos
{
    public partial class EventI_mageCollectionViewCell : UICollectionViewCell
    {





		 
		bool tapped;



		//private string _imageURL;
		public UIImageView PlayButton=new UIImageView();

		//public string imageURL
		//{
		//	set
		//	{
		//		if (value != _imageURL)
		//		{
		//			_imageURL = value;
		//			UpdateContent();

		//		}
		//	}
		//	get { return _imageURL; }
		//}
        public EventI_mageCollectionViewCell (IntPtr handle) : base (handle)
        {
        }

		public void UpdateContent(string ImageUrl,string Type)
		{
			ImageService.Instance.LoadUrl(ImageUrl)
						.ErrorPlaceholder("gradient_background.jpg", ImageSource.ApplicationBundle)
						.LoadingPlaceholder("gradient_background.jpg", ImageSource.CompiledResource)
						.Into(imageForEventImageCollectionCell);
			if (Type == "Image")
			{
				PlaySymbolImage.Hidden = true;
			}
			else
			{
				PlaySymbolImage.Hidden = false;
			}


			CALayer ImageCircle = imageForEventImageCollectionCell.Layer;
			ImageCircle.CornerRadius = 10;
			ImageCircle.MasksToBounds = true;
		}
    }
}