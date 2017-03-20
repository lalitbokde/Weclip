using Foundation;
using System;
using UIKit;
using WebClipIos;
using SharpMobileCode.ModalPicker;

using CoreGraphics;
using WeClip.Core.Model;
using WebClip.IOS.PCL;
using System.Drawing;

namespace WebClipIos
{
	public partial class UICreateEventViewController : UIViewController
    {
			private float scroll_amount = 0.0f;    // amount to scroll 
		private float bottom = 0.0f;           // bottom poin
		private float offset = 10.0f;          // extra offse
		private bool moveViewUp = false;
		partial void Btn_NextCreateEvent_TouchUpInside(UIButton sender)
		{
			if (CheckBlankOrNull() != false)
			{

						var _objEvent = new EventModel();

						_objEvent.EventName= txt_EventTitle.Text;
						_objEvent.EventLocation = txt_EventLocation.Text;
						_objEvent.EventDate = Convert.ToDateTime(txt_EventDate.Text);
						_objEvent.EventEndTime = Convert.ToDateTime(Convert.ToDateTime(txt_EventTime.Text).ToString("HH:MM"));
						_objEvent.EventStartTime = Convert.ToDateTime(Convert.ToDateTime(txt_EventTime.Text).ToString("HH:MM"));
						_objEvent.EventDescription = txt_Description.Text;
						_objEvent.EventTag = "";
				if (PrivacySegment.SelectedSegment == 0)
				{
					_objEvent.EventCategory = "Public";
				}
				else
				{
					_objEvent.EventCategory = "Private";
				}
						_objEvent.UserID = LoginUserDataModel.UserId;

					
						var Controller = (ImageUploadOnEventViewController)this.Storyboard.InstantiateViewController("ImageUploadOnEventViewController");
						Controller.data = _objEvent;

						 NavigationController.PushViewController(Controller, true);
						//CreateEvent(_objEvent);
					}

			//txt_EventTitle.BecomeFirstResponder();
		}

		//partial void Btn_NextCreateEvent_TouchUpInside(UIButton sender)
		//{
		//	if (CheckBlankOrNull() != false)
		//	{

		//		var _objEvent = new EventModel();

		//		_objEvent.EventName= txt_EventTitle.Text;
		//		_objEvent.EventLocation = txt_EventLocation.Text;
		//		_objEvent.EventDate = Convert.ToDateTime(txt_EventDate.Text);
		//		_objEvent.EventEndTime = Convert.ToDateTime(Convert.ToDateTime(txt_EventTime.Text).ToString("HH:MM"));
		//		_objEvent.EventStartTime = Convert.ToDateTime(Convert.ToDateTime(txt_EventTime.Text).ToString("HH:MM"));
		//		_objEvent.EventDescription = txt_Description.Text;
		//		_objEvent.UserID = DataTransferModel.UserId;
		//		CreateEvent(_objEvent);
		//	}
		//}

		//private async void CreateEvent(EventModel UR)
		//{
		//	try
		//	{
		//		Token _ObjTokenResult = null;

		//		if (PrivacySegment.SelectedSegment == 0)
		//		{
		//			UR.EventCategory = "Public";
		//		}
		//		else
		//		{
		//			UR.EventCategory = "Private";
		//		}
		//		//api/Account/LoginWithFacebook api calling
		//		_ObjTokenResult = await UserAccountService.createEvent(UR);

		//		//redirect to main screen
		//		if (_ObjTokenResult.Success == true)
		//		{
		//			new UIAlertView("Message", _ObjTokenResult.Message.ToString(), null,
		//			"OK", null).Show();
		//			txt_EventDate.Text = "";
		//			txt_EventTime.Text = "";
		//			txt_EventTitle.Text = "";
		//			txt_EventLocation.Text = "";

		//			//continuetoMainScreen();
		//		}
		//		else
		//		{
		//			new UIAlertView("Error", _ObjTokenResult.Message.ToString(), null,
		//			"OK", null).Show();
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e.StackTrace);
		//	}
		//}

		//UIButton showSimpleButton;
		//UILabel dateLabel;

		partial void txt_EventDate_DidBeginS(UITextField sender)
		{
			
		}

		public UICreateEventViewController (IntPtr handle) : base (handle)
		{
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();



			txt_EventDate.ShouldBeginEditing += OnTextFieldShouldBeginEditing;
			txt_EventTime.ShouldBeginEditing += OnTimeFieldShouldBeginEditing;

			this.txt_EventTitle.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				txt_EventDate.BecomeFirstResponder();
				return true;
			};

			this.txt_EventDate.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				txt_EventTime.BecomeFirstResponder();
				return true;
			};
			this.txt_EventTime.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				txt_EventLocation.BecomeFirstResponder();
				//Btn_SignUp.SendActionForControlEvents(UIControlEvent.TouchUpInside);
				return true;
			};

			this.txt_EventLocation.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				txt_Description.BecomeFirstResponder();

				//Btn_SignUp.SendActionForControlEvents(UIControlEvent.TouchUpInside);
				return true;
			};

			this.txt_Description.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				PrivacySegment.BecomeFirstResponder();

				return true;
			};

		//		this.txt_Description.ShouldReturn += (textField) =>
		//{
		//	textField.ResignFirstResponder();
		//	txt_EventLocation.BecomeFirstResponder();

		//		btn_NextCreateEvent.Btn_NextCreateEvent_TouchUpInside(UIControlEvent.TouchUpInside);)
		//		return true;
		//};
//				NSNotificationCenter.DefaultCenter.AddObserver
//(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

//			// Keyboard Dow
//			NSNotificationCenter.DefaultCenter.AddObserver
//			(UIKeyboard.WillHideNotification, KeyBoardDownNotification);



			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);
		
		}


		async void DatePickerButtonTapped(object sender, EventArgs e)
		{
			var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", this)
			{
				HeaderBackgroundColor = UIColor.Red,
				HeaderTextColor = UIColor.White,
				TransitioningDelegate = new ModalPickerTransitionDelegate(),
				ModalPresentationStyle = UIModalPresentationStyle.Custom
			};

			modalPicker.DatePicker.Mode = UIDatePickerMode.Date;

			modalPicker.OnModalPickerDismissed += (s, ea) =>
			{
				var dateFormatter = new NSDateFormatter()
				{
					DateFormat = "MMMM dd, yyyy"
				};

				txt_EventDate.Text = dateFormatter.ToString(modalPicker.DatePicker.Date);
			};

			await PresentViewControllerAsync(modalPicker, true);
		}

		 bool OnTextFieldShouldBeginEditing(UITextField textField)
		{
			var modalPicker = new ModalPickerViewController(ModalPickerType.Date, "Select A Date", this)
			{
				HeaderBackgroundColor = UIColor.Red,
				HeaderTextColor = UIColor.White,
				TransitioningDelegate = new ModalPickerTransitionDelegate(),
				ModalPresentationStyle = UIModalPresentationStyle.Custom
			};

			modalPicker.DatePicker.Mode = UIDatePickerMode.Date;

			modalPicker.OnModalPickerDismissed += (s, ea) =>
			{
				var dateFormatter = new NSDateFormatter()
				{
					DateFormat = "MMMM dd, yyyy"
				};

				txt_EventDate.Text = dateFormatter.ToString(modalPicker.DatePicker.Date);
			};

			PresentViewController(modalPicker, true, null);

			return false;
		}

		bool OnTimeFieldShouldBeginEditing(UITextField textField)
		{
			var modalPicker = new ModalPickerViewController(ModalPickerType.Time, "Select Time", this)
			{
				HeaderBackgroundColor = UIColor.Blue,
				HeaderTextColor = UIColor.White,
				TransitioningDelegate = new ModalPickerTransitionDelegate(),
				ModalPresentationStyle = UIModalPresentationStyle.Custom
			};

			//modalPicker.DatePicker.Mode = UIDatePickerMode.Time;

			modalPicker.OnModalPickerDismissed += (s, ea) =>
			{
				var dateFormatter = new NSDateFormatter()
				{
					DateFormat = "HH:MM tt"
				};

				txt_EventTime.Text = dateFormatter.ToString(modalPicker.TimePicker.Date);
			};

			PresentViewController(modalPicker, true, null);

			return false;
		}

		public bool CheckBlankOrNull()
		{
			if (string.IsNullOrEmpty(txt_EventDate.Text))
			{
				new UIAlertView("", "Please Enter Date", null, "Ok", null).Show();
				return false;
			}

			else if (string.IsNullOrEmpty(txt_EventTime.Text))
			{
				new UIAlertView("", "Please Enter Time", null, "Ok", null).Show();
				return false;
			}

			else if (string.IsNullOrEmpty(txt_EventTitle.Text))
			{
				new UIAlertView("", "Please Enter Event Title", null, "Ok", null).Show();
				return false;
			}

			else if (string.IsNullOrEmpty(txt_Description.Text))
			{
				new UIAlertView("", "Please Enter Description", null, "Ok", null).Show();
				return false;
			}
			else if (string.IsNullOrEmpty(txt_EventLocation.Text))
			{
				new UIAlertView("", "Please Enter Event Location", null, "Ok", null).Show();
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			//base.PrepareForSegue(segue, sender);
			//var imageUploadViewController = segue.DestinationViewController as ImageUploadOnEventViewController;
			//imageUploadViewController.Type = "Create";
			//imageUploadViewController.EventId = 0;


		}


		//private void KeyBoardUpNotification(NSNotification notification)
		//{
		//	// get the keyboard size
		//	var r = UIKeyboard.FrameBeginFromNotification(notification);

		//	bottom = ((float)(this.View.Frame.Height));

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

		//	// scroll the view up or dow
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