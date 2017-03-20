using System;
using System.Collections.Generic;
using UIKit;
using WebClip.IOS.PCL;

namespace WebClipIos
{
	public class EventDetailsCollectionViewDataSource : UICollectionViewSource
	{
		string Type;


		List<EventFiles> _imageList = new List<EventFiles>();
		List<WeClipVideo> _videoList = new List<WeClipVideo>();

		public EventDetailsCollectionViewDataSource(List<EventFiles> Items, List<WeClipVideo> videoItems,string type)
		{
			this._imageList = Items;
			this._videoList = videoItems;
			Type = type;
		}
		public event EventHandler RowSelectedEvent;
		public override nint NumberOfSections(UICollectionView collectionView)
		{ return 1; }

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			if (Type == "Image")
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
			else
			{
				if (_videoList != null)
				{
					return _videoList.Count;
				}
				else
				{
					return 0;
				}
			}
		}


		public override UICollectionViewCell GetCell(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			//uncomment
			var imageCell = (EventI_mageCollectionViewCell)collectionView.DequeueReusableCell("EventI_mageCollectionViewCell", indexPath) as EventI_mageCollectionViewCell;
			if (Type == "Image")
			{
				if (_imageList != null)
				{
					imageCell.UpdateContent(_imageList[indexPath.Row].FileUrl,"Image");
				}//"http://static.dev.weclipapp.com/EventFiles/240/EventFiles/8dab3d27a54c4404ae065882d69bfb55.jpg";
			}
			else
			{
				if (_videoList != null)
				{
					
					imageCell.UpdateContent(_videoList[indexPath.Row].Thumb, "Video");

				}
			}

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
		}


		public override void PerformSelector(ObjCRuntime.Selector selector, Foundation.NSObject withObject, double afterDelay, Foundation.NSString[] nsRunLoopModes)
		{
			base.PerformSelector(selector, withObject, afterDelay, nsRunLoopModes);
		}

	}
}
