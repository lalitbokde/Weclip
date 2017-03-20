using Foundation;
using System;
using UIKit;
using WeClip.Core.Model;
using WebClip.IOS.PCL;

namespace WebClipIos
{
	public partial class UserSettingsViewController : BaseController
    {
		partial void Bttn_SaveSetting_TouchUpInside(UIButton sender)
		{
			NotificationSettingPostModel _model = new NotificationSettingPostModel();
			if (PushNotificationSwitch.On)
			{
				_model.isEnable = true;
			}
			else
			{
				_model.isEnable = false;
			}

			if (PrivacySegment.SelectedSegment == 0)
			{
				_model.isPublic = true;
			}
			else
			{
				_model.isPublic = false;
			}

			PostUserNotificationSetting(_model);
		}

		private async void PostUserNotificationSetting(NotificationSettingPostModel UR)
		{
			try
			{
				Token _ObjTokenResult = null;


				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await UserService.updateNotificationSetting(UR);

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


		public UserSettingsViewController (IntPtr handle) : base (handle)
        {
        }
    }
}