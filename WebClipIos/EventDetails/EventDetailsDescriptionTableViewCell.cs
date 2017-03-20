using Foundation;
using System;
using UIKit;
using FFImageLoading;
using FFImageLoading.Work;
using System.Globalization;

namespace WebClipIos
{
    public partial class EventDetailsDescriptionTableViewCell : UITableViewCell
    {
        public EventDetailsDescriptionTableViewCell (IntPtr handle) : base (handle)
        {
        }

		public void UpdateCell(string EventTitle, string HostedBy,string EventLocation,DateTime? EventDate, DateTime? EventTime, string EventDescription, string GoingCount, string MaybeCount,string InvitedCount)
		{
			EventDetailsEventTitle.Text = EventTitle;
			EventDetailsEventLocationLalel.Text = EventLocation;
			if (EventTime != null)
			{
				EventDetailsEventTimeLalel.Text = Convert.ToDateTime(EventTime).ToString();
			}
			EventDetailsEventDescriptionLalel.Text = EventDescription;
			EventDetailsUserNameLabel.Text = HostedBy;
			if (EventDate != null)
			{
				EventDetailsDatelabel.Text = Convert.ToDateTime(EventDate).Day.ToString();
				EventDetailsMonthLabel.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToDateTime(EventDate).Month).Substring(0,3);
			}


			EventDetailsGoingCountButton.SetTitle(GoingCount.ToString(), UIControlState.Normal);
			EventDetailsMayBeCountButton.SetTitle(MaybeCount.ToString(), UIControlState.Normal);
			EventDetailsInvitedCountButton.SetTitle(InvitedCount.ToString(), UIControlState.Normal);


		}


    }
}