using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using WeClip.Core.Model;
using System.Collections.Generic;
using CoreAnimation;
using WebClip.IOS.PCL;

namespace WebClipIos
{
    public partial class EventDetailCell : UITableViewCell
    {
		public UIButton bttn_click = new UIButton();
		public int CurrentClickIndex { get; set; }
		public UICollectionView CollectionEventClick { get; set; }
		public event EventHandler EventDetailCellImageSelectedEvent;

		public long eventid { get;set; }
		public EventDetailCell()
		{
		}

		EventImageListDataSource _dataDource;
	
        public EventDetailCell (IntPtr handle) : base (handle)
        {
        }

		public void UpdateCell(long eventId, string eventName, string eventDate, string hostedBy,string hostedByImage,List<string> _model,bool hide)
		{
			if (hide == false)
			{
				lbl_HostedBy.Text = hostedBy;
				lbl_EventDate.Text = eventDate;
				lbl_EventTitle.Text = eventName;

				ImageService.Instance.LoadUrl(hostedByImage)
							.ErrorPlaceholder("DefaultContactImage.png", ImageSource.ApplicationBundle)
							.LoadingPlaceholder("DefaultContactImage.png", ImageSource.CompiledResource)
							.Into(img_HostedBy);

				CALayer hostedByImageCircle = img_HostedBy.Layer;
				hostedByImageCircle.CornerRadius = 26;
				hostedByImageCircle.MasksToBounds = true;
				_dataDource = new EventImageListDataSource(_model,eventId);
				bttn_Comment.Hidden = false;
				lbl_hostedByDisplay.Text = "Hosted By";
				EventImageCollection.DataSource = _dataDource;

				bttn_click = bttn_Comment;
				CollectionEventClick = EventImageCollection;
				_dataDource.ImageItemSelectedEvent += (sender, e) =>
					{
					if (EventDetailCellImageSelectedEvent != null)
						{
							this.eventid = ((EventImageListDataSource)sender).EventId;
							EventDetailCellImageSelectedEvent(this, EventArgs.Empty);
						}

					};
			}
			else
			{
				lbl_HostedBy.Text = "";
				lbl_EventDate.Text = "";
				lbl_EventTitle.Text = "";
				img_HostedBy.Image = null;
				lbl_hostedByDisplay.Text = "";
				EventImageCollection.DataSource = null;
				bttn_Comment.Hidden = true;
			}
		}
    }
}