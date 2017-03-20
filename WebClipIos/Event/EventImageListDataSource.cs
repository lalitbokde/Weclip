using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FFImageLoading;
using UIKit;
using WeClip.Core.Model;

namespace WebClipIos
{
	public class EventImageListDataSource : UICollectionViewSource
	{
		List<string> _imageList = new List<string>();
		public long EventId { get; set; }
		public EventImageListDataSource(List<string> Items, long eventId)
		{
			this._imageList =Items;
			this.EventId = eventId;
		}
		public event EventHandler ImageItemSelectedEvent;
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
			
			var imageCell = (ImageViewCell)collectionView.DequeueReusableCell("ImageViewCell", indexPath) as ImageViewCell;
			if(_imageList!=null)
			imageCell.imageURL = _imageList[indexPath.Row];
			//imageCell.ImageButton.RestorationIdentifier = EventId.ToString();
		 				imageCell.AddGestureRecognizer(new UITapGestureRecognizer((v) =>
			{
				this.ItemSelected(collectionView, indexPath);
			}));



			return imageCell;
		}
	
		public override void ItemSelected(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			int ind = indexPath.Row;
			if (ImageItemSelectedEvent != null)
			{
				ImageItemSelectedEvent(this, EventArgs.Empty);
				WebClipIos.SocialConstants.CheckEvent = true;
			}
		}


		public override void PerformSelector(ObjCRuntime.Selector selector, Foundation.NSObject withObject, double afterDelay, Foundation.NSString[] nsRunLoopModes)
		{
			base.PerformSelector(selector, withObject, afterDelay, nsRunLoopModes);
		}
	}
}
