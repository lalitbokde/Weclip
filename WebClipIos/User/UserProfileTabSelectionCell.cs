using Foundation;
using System;
using UIKit;

namespace WebClipIos
{
    public partial class UserProfileTabSelectionCell : UITableViewCell
    {
		public UISegmentedControl segmentTab = new UISegmentedControl();
	

	

		public UserProfileTabSelectionCell (IntPtr handle) : base (handle)
        {
        }

		public void UpdateCell()
		{
			segmentTab = UserProfileSegment;
		}

	}
}