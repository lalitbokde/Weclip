using Foundation;
using System;
using UIKit;
using CoreGraphics;
using CoreAnimation;

namespace WebClipIos
{
    public partial class NavController : UINavigationController
    {
        public NavController() : base((string)null, null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("gradient_background.jpg"));
			this.NavigationBar.TintColor = UIColor.White;
			this.NavigationBar.BarTintColor = UIColor.FromPatternImage(UIImage.FromFile("gradient_background.jpg"));
			View.TintColor = UIColor.White;
		
			var gradient = new CAGradientLayer();
			gradient.Frame = this.View.Bounds;
			gradient.NeedsDisplayOnBoundsChange = true;
			gradient.MasksToBounds = true; 

			gradient.Colors = new CGColor[]
			{
					UIColor.FromRGB(0, 214, 168).CGColor,
					UIColor.FromRGB(119, 218, 242).CGColor,

			};

			gradient.Transform.Rotate(180,0,0,0);

			UIGraphics.BeginImageContext(gradient.Bounds.Size);
			gradient.RenderInContext(UIGraphics.GetCurrentContext());
			UIImage backImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			this.NavigationBar.SetBackgroundImage(backImage, UIBarMetrics.Default);

			// Perform any additional setup after loading the view, typically from a nib.
		}
    }
}