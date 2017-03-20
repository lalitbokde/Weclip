using System;
using BigTed;
using UIKit;
using WebClip.IOS.PCL;
using WeClip.Core.Model;


namespace WebClipIos
{
	public partial class LoginWithEmailViewController : UIViewController
	{
		public event EventHandler OnLoginSuccess;

		public LoginWithEmailViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			//avplayer_videocontroler.ViewDidLoad(base);
			this.txt_Username.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				txt_Password.BecomeFirstResponder();
				return true;
			};
			this.txt_Password.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				btn_SignIn.SendActionForControlEvents(UIControlEvent.TouchUpInside);
				return true;
			};

			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);
			View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Homescreen.jpg"));
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void Btn_SignIn_TouchUpInside(UIButton sender)
		{
			if (CheckBlankOrNull() != false)
			{
				var _objLoginModel = new LoginModel();

				_objLoginModel.UserName = txt_Username.Text;
				_objLoginModel.Password = txt_Password.Text;

				SignInWithEmailID(_objLoginModel);
			}
		}

		public bool CheckBlankOrNull()
		{
			if (string.IsNullOrEmpty(txt_Username.Text))
			{
				new UIAlertView("", "Please enter valid User Name.", null, "Ok", null).Show();
				return false;
			}

			else if (string.IsNullOrEmpty(txt_Password.Text) || (txt_Password.Text.Length < 6))
			{
				new UIAlertView("", "Please enter valid Password.", null, "Ok", null).Show();
				return false;
			}

			else
			{
				return true;
			}
		}
		private async void SignInWithEmailID(LoginModel _objLoginModel)
		{
			try
			{
				bool d = BTProgressHUD.IsVisible;
				BTProgressHUD.Show("Logging in..", maskType: ProgressHUD.MaskType.Black);
			
			
				Token _ObjTokenResult = null;
				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await UserAccountService.signInWithEmailID(_objLoginModel);

				//redirect to main screen
				if (_ObjTokenResult != null && _ObjTokenResult.access_token != null && _ObjTokenResult.Success == true)
				{

					LoginUserDataModel.EmailID = _ObjTokenResult.EmailID;
					LoginUserDataModel.UserName = _ObjTokenResult.UserName;
					LoginUserDataModel.UserId = _ObjTokenResult.UserID;
					LoginUserDataModel.AccessToken = _ObjTokenResult.access_token;
					LoginUserDataModel.Bio= _ObjTokenResult.Bio;
					LoginUserDataModel.EmailID = _ObjTokenResult.DOB;
					LoginUserDataModel.IsNotificationEnable = _ObjTokenResult.IsNotificationEnable;
					LoginUserDataModel.IsPublic = _ObjTokenResult.IsPublic;
					LoginUserDataModel.LoginUserName = _ObjTokenResult.LoginUserName;
					LoginUserDataModel.MaxImageForWeClip = _ObjTokenResult.MaxImageForWeClip;
					LoginUserDataModel.MaxVideoForWeclip = _ObjTokenResult.MaxVideoForWeclip;
					LoginUserDataModel.MaxVideoDurationInMinute = _ObjTokenResult.MaxVideoDurationInMinute;
					LoginUserDataModel.PhoneNumber = _ObjTokenResult.PhoneNumber;
					LoginUserDataModel.EmailID =  _ObjTokenResult.ProfilePic;
					LoginUserDataModel.EmailID = _ObjTokenResult.status;
					LoginUserDataModel.EmailID = _ObjTokenResult.token_type;
				

					continuetoMainScreen();
					BTProgressHUD.Dismiss();
				}
				else
				{
					BTProgressHUD.Dismiss();
					new UIAlertView("Error", _ObjTokenResult.Message, null,
					"OK", null).Show();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
				new UIAlertView("", "Mobile Data is turned off", null,
				"OK", null).Show();
			}
		}


		void continuetoMainScreen()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var mainStoryboard = appDelegate.MainStoryboard;
			var rootViewMainController = appDelegate.GetViewController(mainStoryboard, "RootViewController");
			appDelegate.SetRootViewController(rootViewMainController, true);

		}
	}
}