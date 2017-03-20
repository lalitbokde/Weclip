using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using CoreAnimation;

namespace WebClipIos
{
    public partial class ThemeCollectionViewCell : UICollectionViewCell
    {

			bool tapped;
		private string _imageURL;
		//public UIImageView themeCheckImage=new UIImageView();
		public string imageURL
		{
			set
			{
				if (value != _imageURL)
				{
					_imageURL = value;
					//UpdateContent();

				}
			}
			get { return _imageURL; }
		}
        public ThemeCollectionViewCell (IntPtr handle) : base (handle)
        {
			//themeCheckImage = PlaySymbolImage;
        }

		public void UpdateContent(string imageUrl,string themeName)
		{
			ImageService.Instance.LoadUrl(imageUrl)
						.ErrorPlaceholder("gradient_background.jpg", ImageSource.ApplicationBundle)
						.LoadingPlaceholder("gradient_background.jpg", ImageSource.CompiledResource)
			            .Into(ThemeImage);

			lbl_themeName.Text = themeName;

			//CALayer ImageCircle = ThemeImage.Layer;
			//ImageCircle.CornerRadius = 10;
			//ImageCircle.MasksToBounds = true;

			this.SizeToFit();
		}


	}
}