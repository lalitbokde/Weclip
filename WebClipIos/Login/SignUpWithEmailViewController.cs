using Foundation;
using System;
using UIKit;
using WebClip.IOS.PCL;
using System.Net;
using WeClip.Core.Model;

namespace WebClipIos
{
	
    public partial class SignUpWithEmailViewController : UIViewController
    {

	partial void Btn_SignUp_TouchUpInside(UIButton sender)
		{
 			if (CheckBlankOrNull() != false)
			{

				var _objUserRegistration = new RegistrationModel();

				_objUserRegistration.Email = txt_EmailID.Text;
				_objUserRegistration.Password = txt_Password.Text;
				_objUserRegistration.ConfirmPassword = txt_ConfirmPassword.Text;
				_objUserRegistration.DeviceID = "";
				_objUserRegistration.PhoneNumber = "";
				_objUserRegistration.UserName = "";


				SignUpWithEmailID(_objUserRegistration);
			}
		}


		private async void SignUpWithEmailID(RegistrationModel UR)
		{
			try
			{
				Token _ObjTokenResult = null;
				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await UserAccountService.signUpWithEmailID(UR);

				//redirect to main screen
				if (_ObjTokenResult != null && _ObjTokenResult.access_token != null && _ObjTokenResult.Success == true)
				{
					new UIAlertView("Message", _ObjTokenResult.Message.ToString(), null,
					"OK", null).Show();
					//continuetoMainScreen();
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

		public bool CheckBlankOrNull()
		{
			if (string.IsNullOrEmpty(txt_EmailID.Text))
			{
				new UIAlertView("", "Please enter valid Email ID.", null, "Ok", null).Show();
				return false;
			}

			else if (string.IsNullOrEmpty(txt_Password.Text) || (txt_Password.Text.Length < 6))
			{
				new UIAlertView("", "Please enter valid Password.", null, "Ok", null).Show();
				return false;
			}

			else if (string.IsNullOrEmpty(txt_ConfirmPassword.Text))
			{
				new UIAlertView("", "Please enter valid Confirm Password.", null, "Ok", null).Show();
				return false;
			}

			else if ((txt_Password.Text) != (txt_ConfirmPassword.Text))
			{
				new UIAlertView("", "Password Not Match.", null, "Ok", null).Show();
				return false;
			}

			else
			{
				return true;
			}
		}


		public override void ViewDidLoad()
		{
			
			this.txt_EmailID.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				txt_Password.BecomeFirstResponder();
				return true;
			};

			this.txt_Password.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				txt_ConfirmPassword.BecomeFirstResponder();
				return true;
			};
			this.txt_ConfirmPassword.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();


				Btn_SignUp.SendActionForControlEvents(UIControlEvent.TouchUpInside);
				return true;
			};

			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public SignUpWithEmailViewController (IntPtr handle) : base (handle)
        {
        }
    }
}