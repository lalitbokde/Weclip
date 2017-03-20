using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;
using WebClip.IOS.PCL;
using WeClip.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace WebClipIos
{
    public partial class ImageUploadOnEventViewController : UIViewController
    {
		UIImage PhotoCapture;
		EventService _eventService = new EventService();
		public EventModel data { get; set; }
		public List<MediaFile> _listMediaFiles;
		private float scroll_amount = 0.0f;    // amount to scroll 
		private float bottom = 0.0f;           // bottom point
		private float offset = 10.0f;          // extra offset
		private bool moveViewUp = false;

		public string Type { get; set; }//identifies controller parent
		public int EventId { get; set; }

		partial void Bttn_UploadImagge_TouchUpInside(UIButton sender)
		{

			if (data != null)
			{
				data.EventTag = txt_Tag.Text;
				CreateEvent(data);
			}
			
		}


		private async void CreateEvent(EventModel UR)
		{
			try
			{
				Token _ObjTokenResult = null;


				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await UserAccountService.createEvent(UR);

				//redirect to main screen
				if (_ObjTokenResult.Success == true)
				{
					new UIAlertView("Message", _ObjTokenResult.Message.ToString(), null,
					"OK", null).Show();
					
					//continuetoMainScreen();;
				}
				else
				{
					new UIAlertView("Error", _ObjTokenResult.Message.ToString(), null,
					"OK", null).Show();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

	
		partial void Bttn_CaptureImage_TouchUpInside(UIButton sender)
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

		public  void SelectImageFromCamera()
		{
				#region FromCamera		
				TweetStation.Camera.TakePicture(this, (obj) =>
			{


				PhotoCapture = new UIImage();
				 PhotoCapture = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
				imgEventImage.Image = PhotoCapture;

				// This bit of code saves to the application's Documents directory, doesn't save metadata

				var documentsDirectory = Environment.GetFolderPath
											  (Environment.SpecialFolder.Personal);
				string jpgFilename = System.IO.Path.Combine(documentsDirectory, "Photo.jpg");
				NSData imgData = PhotoCapture.AsJPEG();
				NSError err = null;

				MediaFile _mf = new MediaFile();
				_mf.EventID = data.EventID;
				_mf.FilePath = jpgFilename;
				_mf.MediaType = MediaType.Photo;
				 _eventService.UploadSingleFile(_mf);

				if (imgData.Save(jpgFilename, false, out err))
				{
					Console.WriteLine("saved as " + jpgFilename);
				}
				else {
					Console.WriteLine("NOT saved as" + jpgFilename + " because" + err.LocalizedDescription);
				}






			});
			#endregion
		}


		public void cancelUploadImage()
		{
			
			//for selecting image from library
			//NavigationController.PresentModalViewController(imagePicker, true);

		}


		UIImagePickerController imagePicker;
        public ImageUploadOnEventViewController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//imgEventImage.Hidden = true;
			imagePicker = new UIImagePickerController();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

//			NSNotificationCenter.DefaultCenter.AddObserver
//(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

//			// Keyboard Down
//			NSNotificationCenter.DefaultCenter.AddObserver
//			(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

				var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);
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
					imgEventImage.Image = originalImage; // display
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
		//UITextView activeview=new UITextView();
		//private void KeyBoardUpNotification(NSNotification notification)
		//{
		//	// get the keyboard size
		//	var r = UIKeyboard.FrameBeginFromNotification(notification);

		//	 // Find what opened the keyboard
		//	foreach (UITextView view in this.View)
		//	{
		//		if (view.IsFirstResponder)
		//			activeview = view;
		//	}
		//	bottom = ((float)(activeview.Frame.Y + activeview.Frame.Height + offset));

		//	// Calculate how far we need to scroll
		//	scroll_amount = ((float)(r.Height - (View.Frame.Size.Height - bottom)));

		//	// Perform the scrolling
		//	if (scroll_amount > 0)
		//	{
		//		moveViewUp = true;
		//		ScrollTheView(moveViewUp);
		//	}
		//	else {
		//		moveViewUp = false;
		//	}

		//}

		//private void KeyBoardDownNotification(NSNotification notification)
		//{
		//	if (moveViewUp) { ScrollTheView(false); }
		//}

		//private void ScrollTheView(bool move)
		//{

		//	// scroll the view up or down
		//	UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
		//	UIView.SetAnimationDuration(0.3);

		//	RectangleF frame = (System.Drawing.RectangleF)View.Frame;

		//	if (move)
		//	{
		//		frame.Y -= scroll_amount;
		//	}
		//	else {
		//		frame.Y += scroll_amount;
		//		scroll_amount = 0;
		//	}

		//	View.Frame = frame;
		//	UIView.CommitAnimations();
		//}

	}
}