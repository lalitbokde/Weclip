using System;

using Foundation;
using UIKit;

namespace WebClipIos
{
	public partial class EventDetailsDescriptionCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("EventDetailsDescriptionCell");
		public static readonly UINib Nib;

		static EventDetailsDescriptionCell()
		{
			Nib = UINib.FromName("EventDetailsDescriptionCell", NSBundle.MainBundle);
		}

		protected EventDetailsDescriptionCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	}
}
