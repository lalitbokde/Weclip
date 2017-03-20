using Foundation;
using System;
using UIKit;
using WeClip.Core.Model;
using WebClip.IOS.PCL;

namespace WebClipIos
{
    public partial class CreateWeclipFinalPageViewController : UIViewController
    {


		WeClipInfo _data = new WeClipInfo();
        public CreateWeclipFinalPageViewController (IntPtr handle) : base (handle)
        {
		}

		internal void SendWeClipThemeData(WeClipInfo _WeClipInfo)
		{
			_data = _WeClipInfo;
		}

		partial void Bttn_CreateWeClip_TouchUpInside(UIButton sender)
		{
			_data.Tag = txt_EventTag.Text;
			_data.Title = txt_EventName.Text;
			CreateWeClip(_data);
		}

		private async void CreateWeClip(WeClipInfo WCI)
		{
			try
			{
				Token _ObjTokenResult = null;


				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await WeClipService.CreateWeclipVideo(WCI);

				//redirect to main screen
				if (_ObjTokenResult.Success == true)
				{
					new UIAlertView("Message", _ObjTokenResult.Message.ToString(), null,
					"OK", null).Show();

					continuetoMainScreen();;
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

			void continuetoMainScreen()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var mainStoryboard = appDelegate.MainStoryboard;
			var rootViewMainController = appDelegate.GetViewController(mainStoryboard, "RootViewController");
			appDelegate.SetRootViewController(rootViewMainController, true);

		}
	}
}