using System;
using FFImageLoading;
using FFImageLoading.Work;
using Foundation;
using UIKit;

namespace WebClipIos
{
	public partial class EventDetaisCommentTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("EventDetaisCommentTableViewCell");
		public static readonly UINib Nib;

		static EventDetaisCommentTableViewCell()
		{
			Nib = UINib.FromName("EventDetaisCommentTableViewCell", NSBundle.MainBundle);
		}

		protected EventDetaisCommentTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void UpdateCell(string comment,string commentDate,string commentUserName,string hostName,  string imageUrl)
		{

			this.Comment.Text = comment;
			this.CommentDate.Text = commentDate;
			this.CommentUserName.Text = commentUserName;
			this.HostName.Text = hostName;
		
			ImageService.Instance.LoadUrl(imageUrl)
						.ErrorPlaceholder("DefaultContactImage.png", ImageSource.ApplicationBundle)
						.LoadingPlaceholder("DefaultContactImage.png", ImageSource.CompiledResource)
			            .Into(CommentUserImage);

		}
	}
}
