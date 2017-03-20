using System;
namespace WebClipIos
{
	

	public class SocialConstants
	{

		public static bool CheckEvent = false;
		public const string fbClientId = "391823637833075";
		public const string fbClientSecret = "729f20a5ac98baba04836dcc7b37c402";
		public const string appName = "WebClipPro";
		public const string ngClientId = "906689299088-cd38cmeu8ghf16jsm1jgu6uvg9imai69.apps.googleusercontent.com";
		public const string ngClientSecret = "BbNDpGzBdBTcfdZft2bU6AuS";
		public const string ngRedirectUri = "urn:ietf:wg:oauth:2.0:oob";
		public const string gClientId = "906689299088-fcmgu5b0g8jplc3nam2m9vlhko0b957g.apps.googleusercontent.com";
		public const string gClientSecret = "8ArBBJVKDiDqz0WJkM-8cJb-";
		public const string gRedirectUri = "https://www.bit-mob.com/oauth2callback";

		public const string igClientId = "a688ad4f6a2447caab70021dc850c45e";
		public const string igClientSecret = "f4221f2661fe4eb2bd669f4b26f97127";
		public const string igClientScope = "basic public_content follower_list comments relationships likes";
		//static NSArray* const fbPermissions = ["basic_info","read_friendlists","user_status","publish_stream","email","user_birthday","user_location","offline_access","publish_actions"];
		public const string lClientId = "758h8rhaxauwem";
		public const string lClientSecret = "NV8V62RSjQd80vO3";
		public const string lPermissions = "r_emailaddress"; // New permissions
															 //		public const string lPermissions = "r_contactinfo r_fullprofile r_emailaddress r_network rw_nus w_messages";

		public const string tClientId = "R1t0UV1rZtfgR7ArX7pRjg";
		public const string tClientSecret = "XZk3EZmRssgjBOzCL7IwehIbFUYhXfllNjuck7gWE";
		public const string fsClientId = "IDFNAW5O0PD5M5QSLC0DPUWC52RCG4UUSJNN4V054WTHONPD";
		public const string fsClientSecret = "2MROEDIG5BX5GRAUPX3LSFVJGRGYHJECCRWIYCPWMZV1AONA";
		public static readonly string[] fbPermissionsWrite = { "publish_actions" };

		public static readonly string[] fbPermissionsRead =
		{
			"email","public_profile"
		};
		public static readonly string[] fbPermissionsReadTest =
	  {

			 "email"
		};
		public static readonly string[] fbPermissionsfeed = { "user_posts" };
	}

	public class SocialLoginData
	{
		public string scAccessUrl;
		public string scAccount;
		public string scBirthDay;
		public string scFirstName;
		public string scLastName;
		public string scProfileImgUrl;
		public string scSocialId;
		public string scSocialOauthToken;
		public string scSource;
		public string scUserName;
	}

	public class iOSSocialLoginViewModel
	{
		public string SocialNetwork { get; set; }
		public string AccessToken { get; set; }
		public string AccessSecret { get; set; }
		public string SocialNetworkProfileId { get; set; }
		public string Account { get; set; }
	}
}
