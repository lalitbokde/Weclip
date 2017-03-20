using System;
using System.IO;
using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using SDWebImage;
using UIKit;
using WebClip.IOS.PCL;

namespace WebClipIos
{
	public class HelperMethod
	{
		
			public static bool IsUserLoggedIn()
			{
			if (string.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey(LoginUserDataModel.UserName)))
				{
					return false;
				}
				return true;
			}

			public static string GetUserKey()
			{
			return NSUserDefaults.StandardUserDefaults.StringForKey(LoginUserDataModel.AccessToken);
			}



			public static void ClearUserPreferences()
			{
				SetUserPreferences("", "", ""
					);
				
			}

			public static string GetVideoThumbUrl(string videoUrl)
			{
				string videoThumbUrl = null;
				//			string videoThumbUrl = Path.GetDirectoryName (videoUrl) + "/" + Path.GetFileNameWithoutExtension (videoUrl) + "-thumb.jpg";
				var ext = Path.GetExtension(videoUrl);
				if (ext.Equals(".3gp"))
				{
					videoThumbUrl = videoUrl.Replace(".3gp", "-thumb.jpg");
				}
				else if (ext.Equals(".mp4"))
				{
					videoThumbUrl = videoUrl.Replace(".mp4", "-thumb.jpg");
				}
				return videoThumbUrl;
			}

			//		public static UIImage drawImage(UIImage inputImage, CGR frame) {
			//			UIGraphicsBeginImageContextWithOptions(self.size, NO, 0.0);
			//			[self drawInRect:CGRectMake(0.0, 0.0, self.size.width, self.size.height)];
			//			[inputImage drawInRect:frame];
			//			UIImage *newImage = UIGraphicsGetImageFromCurrentImageContext();
			//			UIGraphicsEndImageContext();
			//			return newImage;
			//		}

			public static string GetImageThumbUrl(string imageUrl)
			{
				if (!imageUrl.Contains("WeClip"))
					return imageUrl;
				var thumbImage = Path.GetFileNameWithoutExtension(imageUrl);
				return imageUrl.Replace(thumbImage, thumbImage + "-bigger");
			}

			public static void SetUserPreferences(string userrId, string userKey, string userName)
			{
				if (userrId != null)
				{
				NSUserDefaults.StandardUserDefaults.SetString(userrId, LoginUserDataModel.UserId.ToString());
				}
				if (userKey != null)
				{
				NSUserDefaults.StandardUserDefaults.SetString(userKey, LoginUserDataModel.AccessToken);
				}
				if (userName != null)
				{
				NSUserDefaults.StandardUserDefaults.SetString(userName, LoginUserDataModel.UserName);
				}
				
				NSUserDefaults.StandardUserDefaults.Synchronize();
			}

			// resize the image to be contained within a maximum width and height, keeping aspect ratio
			public static UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
			{
				var sourceSize = sourceImage.Size;
				var maxResizeFactor = (float)Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
				if (maxResizeFactor > 1)
					return sourceImage;
				var width = maxResizeFactor * (float)sourceSize.Width;
				var height = maxResizeFactor * (float)sourceSize.Height;
				UIGraphics.BeginImageContext(new CGSize(width, height));
				sourceImage.Draw(new CGRect(0, 0, width, height));
				var resultImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
				return resultImage;
			}

			// resize the image (without trying to maintain aspect ratio)
			public static UIImage ResizeImage(UIImage sourceImage, float width, float height)
			{
				UIGraphics.BeginImageContext(new CGSize(width, height));
				sourceImage.Draw(new CGRect(0, 0, width, height));
				var resultImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
				return resultImage;
			}

			// crop the image, without resizing
			public static UIImage CropImage(UIImage sourceImage, int crop_x, int crop_y, int width, int height)
			{
				var imgSize = sourceImage.Size;
				UIGraphics.BeginImageContext(new CGSize(width, height));
				var context = UIGraphics.GetCurrentContext();
				var clippedRect = new CGRect(0, 0, width, height);
				context.ClipToRect(clippedRect);
				var drawRect = new CGRect(-crop_x, -crop_y, (float)imgSize.Width, (float)imgSize.Height);
				sourceImage.Draw(drawRect);
				var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
				return modifiedImage;
			}

			// crop the image, without resizing
			public static UIImage CropSquareImage(UIImage sourceImage, float dimension)
			{
				var resizedImage = MaxResizeImage(sourceImage, dimension, dimension);
				var imgSize = resizedImage.Size;
				var largerDimension = imgSize.Width > imgSize.Height ? imgSize.Width : imgSize.Height;
				var crop_y = largerDimension == imgSize.Width ? 0 : (largerDimension - imgSize.Width) / 2;
				var crop_x = largerDimension == imgSize.Height ? 0 : (largerDimension - imgSize.Height) / 2;

				UIGraphics.BeginImageContext(new CGSize(dimension, dimension));
				var context = UIGraphics.GetCurrentContext();
				var clippedRect = new CGRect(0, 0, dimension, dimension);
				context.ClipToRect(clippedRect);
				var drawRect = new CGRect(-crop_x, -crop_y, (float)imgSize.Width, (float)imgSize.Height);
				resizedImage.Draw(drawRect);
				var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
				return modifiedImage;
			}

			public static UIImage GetThumbnailFromVideoURL(NSUrl videoURL)
			{
				UIImage theImage = null;
				var asset = AVAsset.FromUrl(videoURL);
				var generator = AVAssetImageGenerator.FromAsset(asset);
				generator.AppliesPreferredTrackTransform = true;
				NSError error = null;
				var time = new CMTime(1, 60);
				CMTime actTime;
				var img = generator.CopyCGImageAtTime(time, out actTime, out error);

				if (error != null)
				{
					Console.WriteLine(error.ToString());
					return UIImage.FromBundle("videoimage.png");
				}
				Console.WriteLine(img.ToString());
				theImage = new UIImage(img);
				var path = videoURL.RelativePath;
				var imgData = SuperimposeImage(theImage, UIImage.FromBundle("btn_play.png")).AsJPEG(0.5f);
				NSError err = null;
				var jpgFilename = path.Replace(Path.GetExtension(path), "-thumb.jpg");
				if (imgData.Save(jpgFilename, false, out err))
				{
					if (err == null)
					{
						Console.WriteLine("Thumbnail saved");
					}
					else
					{
						Console.WriteLine("error in saving " + err);
					}
				}
				else
				{
					Console.WriteLine("Thumbnail not saved");
				}

				return theImage;
			}

			public static UIImage SuperimposeImage(UIImage image1, UIImage image2)
			{
				UIImage combinedImage;
				UIGraphics.BeginImageContext(image1.Size);
				image1.Draw(new CGRect(
					0, 0, image1.Size.Width, image1.Size.Height));

				image2.Draw(new CGRect(
					image1.Size.Width / 4,
					image1.Size.Height / 4,
					image1.Size.Width / 2,
					image1.Size.Height / 2));

				combinedImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();

				return combinedImage;
			}

			public static void SaveVideoThumbnail(UIImage image, string thumbname)
			{
				var img = UIImage.FromBundle("btn_play.png");
				var imgData = SuperimposeImage(image, img).AsJPEG(0.5f);
				NSError err = null;
				//			thumbname = "bitmob-" + Guid.NewGuid () + ".jpg";
				//			string jpgFilename = System.IO.Path.Combine (CommonHelper.GetDirectoryForVideo (), thumbname);
				if (imgData.Save(thumbname, false, out err))
				{
					if (err == null)
					{
						Console.WriteLine("Thumbnail saved");
					}
					else
					{
						Console.WriteLine("error in saving " + err);
					}
				}
				else
				{
					Console.WriteLine("Thumbnail not saved");
				}
			}

			public static void SaveImage(UIImage image, string thumbname)
			{
				var imgData = image.AsJPEG(0.5f);
				NSError err = null;
				if (imgData.Save(thumbname, false, out err))
				{
					if (err == null)
					{
						Console.WriteLine("Thumbnail saved");
					}
					else
					{
						Console.WriteLine("error in saving " + err);
					}
				}
				else
				{
					Console.WriteLine("Thumbnail not saved");
				}
			}

			public static void SetVideoThumbnailOnUIImageView(UIImageView imageView, string url, int type = 0,
				bool addBtn = true)
			{
				var filePath = Path.Combine(GetDirectoryForVideo(), GetVideoThumbUrl(Path.GetFileName(url)));
				//Check image exists
				if (!File.Exists(filePath))
				{
					var vdoThumb = GetVideoThumbUrl(url);
					imageView.SetImage(
						new NSUrl(vdoThumb),
						UIImage.FromBundle("DefaultContactImage.png"),
						SDWebImageOptions.ProgressiveDownload,
						(image, error, cacheType, finished) =>
						{
							if (image != null)
							{
								SaveVideoThumbnail(image, filePath);
							}
						}
						);
				}
				else
				{
					imageView.SetImage(NSUrl.FromFilename(filePath));
				}
				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

				if (addBtn)
				{
					//				UIImageView myImageView = new UIImageView (imageView.Frame);
					//				myImageView.ContentMode = UIViewContentMode.Center;
					//				myImageView.Image = UIImage.FromFile ("btn_play.png");
					//				imageView.Add (myImageView);
				}
			}

			public static void SetVideoThumbnailOnUIImageView(UIImageView imageView, string url, int type = 0)
			{
				var filePath = Path.Combine(GetDirectoryForVideo(), GetVideoThumbUrl(Path.GetFileName(url)));
				if (!File.Exists(filePath))
				{
					var vdoThumb = GetVideoThumbUrl(url);
					imageView.SetImage(
						new NSUrl(vdoThumb),
						UIImage.FromBundle("DefaultContactImage.png"),
						SDWebImageOptions.ProgressiveDownload,
						(image, error, cacheType, finished) =>
						{
							if (image != null)
							{
								SaveVideoThumbnail(image, filePath);
							}
						}
						);
				}
				else
				{
					imageView.SetImage(NSUrl.FromFilename(filePath));
				}
				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
				//			if(type == 0)
				//				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
				//			else if(type == 1)
				//				imageView.ContentMode = UIViewContentMode.Left;
				//			else if(type == 2)
				//				imageView.ContentMode = UIViewContentMode.Right;
			}

			// type = 0 -> Center
			// type = 1 -> Left
			// type = 2 -> Right
			public static void SetImageOnUIImageView(UIImageView imageView, string url, int type = 0)
			{
				if (string.IsNullOrWhiteSpace(url))
				{
					imageView.Image = UIImage.FromBundle("DefaultContactImage.png");
				}

				var filePath = Path.Combine(GetDirectoryForPictures(), Path.GetFileName(url));

				if (!File.Exists(filePath))
				{
					imageView.SetImage(
						new NSUrl(url),
						UIImage.FromBundle("DefaultContactImage.png"),
						SDWebImageOptions.RetryFailed,
						(image, error, cacheType, finished) =>
						{
							if (image != null)
							{
								SaveImage(image, filePath);
							}
						}
						);
				}
				else
				{
					imageView.SetImage(NSUrl.FromFilename(filePath));
				}
				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
				//			if(type == 0)
				//				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
				//			else if(type == 1)
				//				imageView.ContentMode = UIViewContentMode.Left;
				//			else if(type == 2)
				//				imageView.ContentMode = UIViewContentMode.Right;
			}

			//		+ (UIImage *)thumbnailFromVideoAtURL:(NSURL *)contentURL {
			//			UIImage *theImage = nil;
			//			AVURLAsset *asset = [[AVURLAsset alloc] initWithURL:contentURL options:nil];
			//			AVAssetImageGenerator *generator = [[AVAssetImageGenerator alloc] initWithAsset:asset];
			//			generator.appliesPreferredTrackTransform = YES;
			//			NSError *err = NULL;
			//			CMTime time = CMTimeMake(1, 60);
			//			CGImageRef imgRef = [generator copyCGImageAtTime:time actualTime:NULL error:&err];
			//
			//			theImage = [[[UIImage alloc] initWithCGImage:imgRef] autorelease];
			//
			//			CGImageRelease(imgRef);
			//			[asset release];
			//			[generator release];
			//
			//			return theImage;
			//		}

	

			public static string GetDirectoryForPictures()
			{
				//System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.jpg")
				var _dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "WebClipPictures");
				if (!Directory.Exists(_dirPath))
				{
					Directory.CreateDirectory(_dirPath);
				}
				return _dirPath;
			}

			public static string GetDirectoryForAudio()
			{
				//System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.jpg")
				var _dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "WebClipAudio");
				if (!Directory.Exists(_dirPath))
				{
					Directory.CreateDirectory(_dirPath);
				}
				return _dirPath;
			}

			public static string GetDirectoryForVideo()
			{
				//System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.jpg")
				var _dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "WebClipVideo");
				if (!Directory.Exists(_dirPath))
				{
					Directory.CreateDirectory(_dirPath);
				}
				return _dirPath;
			}



			public static string GetMediaTypeByExtension(string extension)
			{
				switch (extension)
				{
					case ".jpg":
						return "image/jpg";
					case ".png":
						return "image/png";
					case ".gif":
						return "image/gif";
					case ".bmp":
						return "image/bmp";
					default:
						return "";
				}
			}


			
			public static void CreateAppDirectory()
			{
				var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var directoryname = Path.Combine(documents, "BitmobDirectory");
				var isExisting = Directory.Exists(directoryname);
				if (!isExisting)
				{
					Directory.CreateDirectory(directoryname);
				}
				else
				{
					Console.WriteLine("Directory already exists");
				}
			}

		}

}
