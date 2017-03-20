using System;
using System.Collections.Generic;

namespace WebClipIos.Social
{

	public class Email
	{
		public string value { get; set; }
		public string type { get; set; }
	}

	public class TwUrl
	{
		public string value { get; set; }
		public string type { get; set; }
		public string label { get; set; }
	}

	public class EventName
	{
		public string familyName { get; set; }
		public string givenName { get; set; }
	}

	public class Image
	{
		public string url { get; set; }
		public bool isDefault { get; set; }
	}

	public class RootObjectGooglePlus
	{
		public string kind { get; set; }
		public string etag { get; set; }
		public string gender { get; set; }
		public List<Email> emails { get; set; }
		public List<TwUrl> urls { get; set; }
		public string objectType { get; set; }
		public string id { get; set; }
		public string displayName { get; set; }
		public EventName name { get; set; }
		public string url { get; set; }
		public Image image { get; set; }
		public bool isPlusUser { get; set; }
		public int circledByCount { get; set; }
		public bool verified { get; set; }
	}
}