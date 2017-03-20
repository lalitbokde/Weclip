using AVKit;
using Foundation;
using System;
using MediaPlayer;
using System.Drawing;
using AVFoundation;
using UIKit;

namespace WebClipIos
{
	public partial class avplayer_videocontroler : AVPlayerViewController
	{
		AVPlayer _player;
		AVPlayerLayer _playerLayer;
		AVAsset _asset;
		AVPlayerItem _playerItem;
		public avplayer_videocontroler(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			
			base.ViewDidLoad();



			_asset = AVAsset.FromUrl(NSUrl.FromFilename("/Users/lalitbokde/Downloads/Tu Hi Hai - Video MP4.mp4"));
			_playerItem = new AVPlayerItem(_asset);
			//_playerLayer.BackgroundColor = UIColor.Blue.CGColor;

			//_playerLayer.Frame = new RectangleF(0, 0, 320, 320);

			//View.Layer.AddSublayer(_playerLayer);

			_player = new AVPlayer(_playerItem);

			_playerLayer = AVPlayerLayer.FromPlayer(_player);
			_playerLayer.Frame = View.Frame;
			View.Layer.AddSublayer(_playerLayer);

			_player.Play();

			//string url ="http://www.ebookfrenzy.com/ios_book/movie/movie.mov";

			//this.Player = AVPlayer.NSUrl.FromFilename("https://dl.pagal.link/upload_file/367/382/7491/PagalWorld%20-%20Bollywood%20Mp4%20Video%20Songs%202016/Dear%20Zindagi%20%282016%29%20Mp4%20Video%20Songs/Tu%20Hi%20Hai%20-%20Video%20MP4.mp4"));
			//this.Player.View.Frame = new RectangleF(0f, 20f, (float)this.View.Frame.Width, 320f); this.View.AddSubview(this.Player.View);

		}


    }
}