using System;
using SQLite;

namespace WebClip.IOS.Common
{
	public class UserProfile
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		public long userId { get; set; }
		public string userName { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string gender { get; set; }
		public long? telephone { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string profilePicUrl { get; set; }
		public DateTime lastAccessDate { get; set; }
		public double lastAccessLatitude { get; set; }
		public double lastAccessLongitude { get; set; }
		public DateTime? dateofBirth { get; set; }
		public string language { get; set; }
		public long userGroupId { get; set; }
		public long broadcastDistance { get; set; }
		public long broadcastDistanceUnit { get; set; }
	}
}
