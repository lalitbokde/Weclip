using Foundation;
using System;
using UIKit;
using System.Drawing;
using WeClip.Core.Model;
using WebClip.IOS.PCL;
using System.Collections.Generic;

namespace WebClipIos
{
	public partial class CommentViewController : UIViewController
	{
		public EventModel EventData {get;set;}
		private UIView activeview;             // Controller that activated the keyboard
		private float scroll_amount = 0.0f;    // amount to scroll 
		private float bottom = 0.0f;           // bottom point
		private float offset = 10.0f;          // extra offset
		private bool moveViewUp = false;
		CommentListTableDataSource _source { get; set; }
		List<EventFeedModel> _listComment = new List<EventFeedModel>();


		partial void Bttn_send_TouchUpInside(UIButton sender)
		{
			EventFeedModel _model = new EventFeedModel();
			_model.EventID = EventData.EventID;
			_model.FeedDate = DateTime.UtcNow;
			_model.Message = txt_Comment.Text;
			PostComment(_model);
			LoadComments();
		}

		private async void PostComment(EventFeedModel commntmodel)
		{
			try
			{
				Token _ObjTokenResult = null;

				//api/Account/LoginWithFacebook api calling
				_ObjTokenResult = await EventService.PostComment(commntmodel);

				//redirect to main screen
				if (_ObjTokenResult.Success == true)
				{
					new UIAlertView("Message", _ObjTokenResult.Message.ToString(), null,
					"OK", null).Show();

					//continuetoMainScreen();;
				}
				else
				{
					new UIAlertView("Error", _ObjTokenResult.Message.ToString(), null,
					"OK", null).Show();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		public CommentViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.WillHideNotification, KeyBoardDownNotification);
			txt_Comment.BecomeFirstResponder();


			LoadComments();

				var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5

			View.AddGestureRecognizer(g);
		

		}

		private async void loadCommentTable(long eventId)
		{
			_listComment = await EventService.getEventComments(eventId);

		}

		void LoadComments()
		{
			loadCommentTable(EventData.EventID);
			_source = new CommentListTableDataSource(_listComment);


			CommentTable.RegisterNibForCellReuse(UINib.FromName("EventDetaisCommentTableViewCell", null), "EventDetaisCommentTableViewCell");
			this.CommentTable.Source = _source;
			CommentTable.ReloadData();
			CommentTable.RowHeight = 50;
		}

		public  void SetTask(EventListViewController eventListViewController, EventModel item)
		{
			EventData = item;
		}

		private void KeyBoardUpNotification(NSNotification notification)
		{
			// get the keyboard size
			    var r = UIKeyboard.FrameBeginFromNotification(notification);
		//	RectangleF r = UIKeyboard.BoundsFromNotification(notification);

			// Find what opened the keyboard
			//foreach (UIView view in this.View.Subviews)
			//{
			//	if (view.IsFirstResponder)
			//		activeview = view;
			//}

			// Bottom of the controller = initial position + height + offset      
			//bottom = (((float)((float)(this.View.Frame.Y) + this.View.Frame.Height)));


			bottom = ((float)(this.View.Frame.Height));

			// Calculate how far we need to scroll
			scroll_amount = ((float)(r.Height - (View.Frame.Size.Height - bottom)));

			// Perform the scrolling
			if (scroll_amount > 0)
			{
				moveViewUp = true;
				ScrollTheView(moveViewUp);
			}
			else {
				moveViewUp = false;
			}

		}

		private void KeyBoardDownNotification(NSNotification notification)
		{
			if (moveViewUp) { ScrollTheView(false); }
		}

		private void ScrollTheView(bool move)
		{
			
			// scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);

			RectangleF frame = (System.Drawing.RectangleF)View.Frame;

			if (move)
			{
				frame.Y -= scroll_amount;
			}
			else {
				frame.Y += scroll_amount;
				scroll_amount = 0;
			}

			View.Frame = frame;
			UIView.CommitAnimations();
		}

}
}