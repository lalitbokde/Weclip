using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace WebClip.IOS.Common
{
	public class UserProfileRepository
	{
			public static void SaveUserProfile(UserProfile userProfile)
			{
				try
				{
					var db = new SQLiteConnection(CommonConstants.DBPath);
					db.Insert(userProfile);
					db.Commit();
					db.Close();
				}
				catch (SQLiteException e)
				{
					Console.WriteLine("Error while saving user data : " + e.Message);
				}
			}

			public static List<UserProfile> GetUserProfile(int bitmobId, int page = 0)
			{
				List<UserProfile> lstUserProfile = null;
				var db = new SQLiteConnection(CommonConstants.DBPath);
				try
				{
					lstUserProfile = db.Query<UserProfile>("select * from 'UserProfile' where userId=" + bitmobId);
				}
				catch (SQLiteException e)
				{
					lstUserProfile = new List<UserProfile>();
					Console.WriteLine("Error while fetching user data : " + e.Message);
				}
				db.Close();
				return lstUserProfile;
			}

			public static UserProfile checkUser(long id)
			{
				UserProfile result = null;
				var db = new SQLiteConnection(CommonConstants.DBPath);
				try
				{
					var data = db.Query<UserProfile>("select * from 'UserProfile' where userId=" + id).ToList();
					//				var res = db.Query<UserProfile>("select * from 'UserProfile'");
					result = data.FirstOrDefault();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message, ex);
				}
				db.Close();
				return result;
			}

			public static void UpdateUserProfile(UserProfile userProfile)
			{
				var db = new SQLiteConnection(CommonConstants.DBPath);
				try
				{
					db.Update(userProfile);
					db.Commit();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message, ex);
				}
				db.Close();
			}

			public static void DeleteUserProfiles()
			{
				var db = new SQLiteConnection(CommonConstants.DBPath);
				try
				{
					var result = db.Execute("delete from 'UserProfile'");
					if (result > 0)
						Console.WriteLine("UserProfiles deleted.");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message, ex);
				}
				db.Close();
			}
		}
	}