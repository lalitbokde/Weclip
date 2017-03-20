using System;
namespace WebClip.IOS.PCL
{
	public class UrlHelper
	{
		public static string BaseUrl = "http://api.weclipapp.com/";

		public struct AccountUrls
		{
			//Post For Logins
			public static string LoginfbServiceUrl = BaseUrl + "api/Account/LoginWithFacebook";
			public static string LogingoogleServiceUrl = BaseUrl + "api/Account/LoginWithGoogle";
			public static string CheckLogin = BaseUrl + "api/Account/UserInfo";
			public static string UserRegistration = BaseUrl + "api/Account/RegisterNew";
			public static string LoginNew = BaseUrl + "api/Account/LoginNew";

			//user
			public static string UserProfileDetailsUrl = BaseUrl + "api/User/GetUserProfileWithWeclipStory?id=";
			public static string UserProfileNotificationSettingUpdateUrl = BaseUrl + "api/api/User/NotificationSetting?";

			//Post for Event
			public static string EventCreateUrl = BaseUrl + "api/Event/Create";
			public static string PostPhoto = BaseUrl + "Event/UploadEventFiles?eventid=";
			public static string PostVideo = BaseUrl + "Event/UploadVideo?eventid=";
			public static string EventCommentPostUrl = BaseUrl + "api/Event/SaveEventFeed";

			//get for Event
			public static string GetEventUrl = BaseUrl + "api/EventRequest/GetMyEventList?id=";
			public static string GetEventDetailsUrl = BaseUrl + "api/Event/GetEventDetails?eventid=";
			public static string GetWeClipVideoUrl = BaseUrl + "api/Event/GetWeClipVideo?eventid=";
			public static string GetImagesUrl = BaseUrl + "api/Event/GetEventFiles?eventid=";
			public static string GetEventFeedUrl = BaseUrl + "api/Event/GetAllEventFeedDetails?eventid=";

			//post weclip
			public static string CreateWeclipVideo = BaseUrl + "/api/Event/CreateWeClipVideo?eventid=";

			//get weclip
			public static string GetWeClipThemes = BaseUrl + "/api/Event/GetWeClipThemes";
		}
	}
}
