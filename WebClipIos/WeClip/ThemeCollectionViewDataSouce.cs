using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using WeClip.Core.Model;

namespace WebClipIos
{
	public class ThemeCollectionViewDataSouce : UICollectionViewSource
	{
		//public NSIndexPath[] SelectedItems { get { return _selectedItems.ToArray(); } }
		//readonly List<NSIndexPath> _selectedItems = new List<NSIndexPath>();
		public NSIndexPath selectedThemeIndex = new NSIndexPath();


		List<Theme> _imageList = new List<Theme>();
	
		public ThemeCollectionViewDataSouce(List<Theme> Items)
		{
			this._imageList = Items;

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
			var imageCell = (ThemeCollectionViewCell)collectionView.DequeueReusableCell("ThemeCollectionViewCell", indexPath) as ThemeCollectionViewCell;
			//imageCell.themeCheckImage.Hidden = true;

			if (_imageList != null)
				imageCell.UpdateContent(_imageList[indexPath.Row].Thumb,_imageList[indexPath.Row].Name);
			imageCell.AddGestureRecognizer(new UITapGestureRecognizer((v) =>
			{
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


			this.ItemHighlighted(collectionView, indexPath);

				//_selectedItems.RemoveAll((NSIndexPath obj) => true);
				//_selectedItems.Add(indexPath);
			selectedThemeIndex = indexPath;

			//else
			//{
			//	this.ItemUnhighlighted(collectionView, indexPath);
			//	_selectedItems.Remove(indexPath);
			//}
		}


		public override void PerformSelector(ObjCRuntime.Selector selector, Foundation.NSObject withObject, double afterDelay, Foundation.NSString[] nsRunLoopModes)
		{
			base.PerformSelector(selector, withObject, afterDelay, nsRunLoopModes);
		}

		public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
		{
			
			var selectedItemsNew = collectionView.IndexPathsForVisibleItems;
			foreach (var item in selectedItemsNew)
			{
				collectionView.DeselectItem(item, animated: true);

							  this.ItemUnhighlighted(collectionView, item);

			}
		
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

		public Theme GetSelectedTheme(NSIndexPath indexPath)
		{
			
				Theme _class = new Theme();
			_class = _imageList[indexPath.Row];
			return _class;
		}
	}
}
