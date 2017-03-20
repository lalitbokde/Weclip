using Foundation;
using System;
using UIKit;
using CoreAnimation;
using WebClip.IOS.PCL;
using FFImageLoading;
using FFImageLoading.Work;

namespace WebClipIos
{
	public partial class ProfileViewController : BaseController
    {
		UIImagePickerController imagePicker;
		partial void UIButton677_TouchUpInside(UIButton sender)
		{
			UIAlertController _ImageSelection = UIAlertController.Create("", "", UIAlertControllerStyle.ActionSheet);
			_ImageSelection.AddAction(UIAlertAction.Create("Capture Image", UIAlertActionStyle.Default, (action) => SelectImageFromCamera()));
			_ImageSelection.AddAction(UIAlertAction.Create("Select from Library", UIAlertActionStyle.Default, (action) => SelectImageFromLibrary()));
			_ImageSelection.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (action) => cancelUploadImage()));

			UIPopoverPresentationController presentationPopover = _ImageSelection.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}
			// Display the pop up for options for selecting image
			this.PresentViewController(_ImageSelection, true, null);
		}

			public void SelectImageFromLibrary()
		{
			#region FromLibrary
			//for selecting image from library
			NavigationController.PresentModalViewController(imagePicker, true);
			#endregion
		}

		public void SelectImageFromCamera()
		{
				#region FromCamera		
				TweetStation.Camera.TakePicture(this, (obj) =>
			{
				
				var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
				profileimageview.Image = photo;

				// This bit of code saves to the application's Documents directory, doesn't save metadata
				var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

				string jpgFilename = System.IO.Path.Combine(documentsDirectory, "Photo.jpg");

				var someImage = UIImage.FromFile(jpgFilename);
				someImage.SaveToPhotosAlbum((image, error) =>
				{
					var o = image as UIImage;
					Console.WriteLine("error:" + error);
				});

			});
			#endregion
		}


		public void cancelUploadImage()
		{
		}

		public ProfileViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
				CALayer profileImageCircle = profileimageview.Layer;
			profileImageCircle.CornerRadius = 40;
			profileImageCircle.MasksToBounds = true;



			txt_UserProfileName.Text = LoginUserDataModel.UserName;
			lbl_EmailID.Text = LoginUserDataModel.EmailID;

			ImageService.Instance.LoadUrl(LoginUserDataModel.ProfilePic)
					.ErrorPlaceholder("DefaultContactImage.png", ImageSource.ApplicationBundle)
					.LoadingPlaceholder("DefaultContactImage.png", ImageSource.CompiledResource)
			            .Into(profileimageview);

				imagePicker = new UIImagePickerController();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

		}

		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch (e.Info[UIImagePickerController.MediaType].ToString())
			{
				case "public.image":
					Console.WriteLine("Image selected");
					isImage = true;
					break;
				case "public.video":
					Console.WriteLine("Video selected");
					break;
			}

			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null)
				Console.WriteLine("Url:" + referenceURL.ToString());

			// if it was an image, get the other image info
			if (isImage)
			{
				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null)
				{
					// do something with the image
					Console.WriteLine("got the original image");
					profileimageview.Image = originalImage; // display
				}
			}
			else { // if it's a video
				   // get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if (mediaURL != null)
				{
					Console.WriteLine(mediaURL.ToString());
				}
			}
			// dismiss the picker
			imagePicker.DismissModalViewController(true);
		}

		void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewController(true);
		}
    }
}