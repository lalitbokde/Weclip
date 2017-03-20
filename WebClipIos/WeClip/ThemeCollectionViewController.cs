using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using WeClip.Core.Model;
using WebClip.IOS.PCL;

namespace WebClipIos
{
    public partial class ThemeCollectionViewController : UICollectionViewController
    {

		ThemeCollectionViewDataSouce _dataSource { get; set; }
		public long eventid { get; set; }
		List<Theme> imageList = new List<Theme>();
		WeClipInfo _WeClipInfo = new WeClipInfo();
        public ThemeCollectionViewController (IntPtr handle) : base (handle)
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

			this.CollectionView.DataSource = _dataSource;
			this.CollectionView.ReloadData();

		}

		private async void loadThemeList()
		{
			imageList = await WeClipService.getWeClipThemes();
		}
    }
}