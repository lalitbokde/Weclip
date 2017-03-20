using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using WebClip.IOS.PCL;
using WeClip.Core.Model;

namespace WebClipIos
{
	public class CreateWeClipImageCollectionViewDataSource : UICollectionViewSource
	{
		
		public NSIndexPath[] SelectedItems { get { return _selectedItems.ToArray(); } }
		readonly List<NSIndexPath> _selectedItems = new List<NSIndexPath>();

		List<EventFiles> _imageList = new List<EventFiles>();
		List<WeClipVideo> _videoList = new List<WeClipVideo>();

		public CreateWeClipImageCollectionViewDataSource(List<EventFiles> Items, List<WeClipVideo> videoItems,string Type)
		{
			this._imageList = Items;
			this._videoList = videoItems;
		}

		public event EventHandler RowSelectedEvent;
		public override nint NumberOfSections(UICollectionView collectionView)
		{ return 1; }

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			if (_imageList != null)
			{
				return _imageList.Count;
			}
			else
			{
				return 0;
			}
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			//uncomment
			var imageCell = (CreateWeClipImagesCollectionViewCell)collectionView.DequeueReusableCell("CreateWeClipImagesCollectionViewCell", indexPath) as CreateWeClipImagesCollectionViewCell;
			imageCell.checkImage.Hidden = true;
			if (_imageList != null)
				imageCell.imageURL = _imageList[indexPath.Row].FileUrl;//"http://static.dev.weclipapp.com/EventFiles/240/EventFiles/8dab3d27a54c4404ae065882d69bfb55.jpg";

			imageCell.AddGestureRecognizer(new UITapGestureRecognizer((v) =>
			{
				if (collectionView.CellForItem(indexPath).ContentView.BackgroundColor != UIColor.Yellow)
				{
					imageCell.checkImage.Hidden = false;
				}
				else
				{
					imageCell.checkImage.Hidden = true;
				}
				this.ItemSelected(collectionView, indexPath);
			}));
			return imageCell;
		}

		public override void ItemSelected(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			int ind = indexPath.Row;
			if (RowSelectedEvent != null)
			{
				RowSelectedEvent(this, EventArgs.Empty);
				WebClipIos.SocialConstants.CheckEvent = true;
			}
			if (collectionView.CellForItem(indexPath).ContentView.BackgroundColor != UIColor.Yellow)
			{
				this.ItemHighlighted(collectionView, indexPath);
				_selectedItems.Add(indexPath);
			}
			else
			{
				this.ItemUnhighlighted(collectionView, indexPath);
				_selectedItems.Remove(indexPath);
			}
		}


		public override void PerformSelector(ObjCRuntime.Selector selector, Foundation.NSObject withObject, double afterDelay, Foundation.NSString[] nsRunLoopModes)
		{
			base.PerformSelector(selector, withObject, afterDelay, nsRunLoopModes);
		}

		public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem(indexPath);
			cell.ContentView.BackgroundColor = UIColor.Yellow;
		}

		public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem(indexPath);
			cell.ContentView.BackgroundColor = UIColor.White;
		}

		public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
		{
			return true;
		}

		public List<ImageInfo> GetSelectedItemList(NSIndexPath[] indexPath)
		{
			List<ImageInfo> _list = new List<ImageInfo>();
			foreach (var item in indexPath)
			{
				ImageInfo _class = new ImageInfo();
				_class.Filename = _imageList[item.Row].FileName;
				_class.IsVideo = false;
				_list.Add(_class);
			}
			return _list;
		}
	}
}
