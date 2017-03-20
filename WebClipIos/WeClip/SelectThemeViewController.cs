using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using WeClip.Core.Model;
using WebClip.IOS.PCL;

namespace WebClipIos
{
    public partial class SelectThemeViewController : UIViewController
    {
        ThemeCollectionViewDataSouce _dataSource { get; set; }
		public long eventid { get; set; }
		List<Theme> imageList = new List<Theme>();
		WeClipInfo _WeClipInfo = new WeClipInfo();
		public SelectThemeViewController(IntPtr handle) : base (handle)
        {
		}

		internal void SendWeClipImageData(WeClipInfo _info)
		{
			_WeClipInfo = _info;
		}


		public override void ViewDidLoad()
		{
			
			getWeClipThemeCollection();
		}

		void getWeClipThemeCollection()
		{
			loadThemeList();
			_dataSource = new ThemeCollectionViewDataSouce(imageList);

			ThemesCollectionView.DataSource = _dataSource;
			ThemesCollectionView.ReloadData();

		}

		private async void loadThemeList()
		{
			imageList = await WeClipService.getWeClipThemes();
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			
			if (segue.Identifier == "CreateWeClip")
			{
				base.PrepareForSegue(segue, sender);
				var createWeclipFinalPageViewController = segue.DestinationViewController as CreateWeclipFinalPageViewController;

				if (createWeclipFinalPageViewController != null)
				{
					_WeClipInfo.AudioId = 1;
					_WeClipInfo.IsDefault = true;
					_WeClipInfo.ThemeID = _dataSource.GetSelectedTheme(_dataSource.selectedThemeIndex).ID;
					createWeclipFinalPageViewController.SendWeClipThemeData(_WeClipInfo);

					// to be defined on the TaskDetailViewController
				}
				//commentViewController.EventData = this.EventData;
				//NavigationController.PushViewController(commentViewController, true);



			}
		}
    }
}