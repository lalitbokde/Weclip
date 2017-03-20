using System;

using Foundation;
using UIKit;

namespace WebClipIos
{
	public partial class EventDetailsHeaderCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("EventDetailsHeaderCell");
		public static readonly UINib Nib;

		static EventDetailsHeaderCell()
		{
			Nib = UINib.FromName("EventDetailsHeaderCell", NSBundle.MainBundle);
		}

		protected EventDetailsHeaderCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	}
}
