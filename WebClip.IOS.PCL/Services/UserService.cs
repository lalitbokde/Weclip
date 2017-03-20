using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using SystemConfiguration;
using WeClip.Core.Model;

namespace WebClip.IOS.PCL
{
	public class UserService
	{
		public static int StatusCode;
		public static async Task<Token> updateNotificationSetting(NotificationSettingPostModel model)
		{
			 StatusCode = 0;

			try
			{
				string Setting = "IsPublic=" + model.isPublic + "&IsEnable=" + model.isEnable;
				var url = UrlHelper.AccountUrls.UserProfileNotificationSettingUpdateUrl+Setting;
				var client = new RestClient(url);
				var request = new RestRequest(Method.GET);
				if (LoginUserDataModel.AccessToken != null)
				{
					client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", LoginUserDataModel.AccessToken));
				}
				//if (model != null)
				//{

				//	var json = JsonConvert.SerializeObject(model);
				//	request.AddParameter("application/json", json, ParameterType.RequestBody);
				//}

				var response = client.Execute(request);


				if (response != null)
				{
					StatusCode = Convert.ToInt32(response.StatusCode);
					if (response.StatusCode == HttpStatusCode.OK)
					{

						try
						{
							var result = JsonConvert.DeserializeObject<Token>(response.Content);
							return result;
						}
						catch (JsonException ex)
						{
							throw new Exception("Json data could not be parsed. ", ex);
						}
						catch (Exception ex)
						{
							throw new Exception("Could not connect to the server. ", ex);
						}
					}
					if (StatusCode == 400)
						throw new Exception("The token does not exist in the database" +
											CommonHelper.Get_Response_Status_Message(StatusCode));
					throw new Exception("Failuer response from server. " +
										CommonHelper.Get_Response_Status_Message(StatusCode));
				}
				throw new Exception("Null responce from server. " + CommonHelper.Get_Response_Status_Message(StatusCode));
			}
			catch (JsonSerializationException e)
			{
				throw new Exception(
					"Json Data could not be parsed." + CommonHelper.Get_Response_Status_Message(StatusCode), e);
			}
			catch (Exception ex)
			{
				throw ex;
				//new Exception("Could not connect to the server." + CommonHelper.Get_Response_Status_Message(StatusCode), ex);
			}
		}


		#region userProfile

		public static async Task<UserProfile> GetUserProfileDetails()
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.UserProfileDetailsUrl + LoginUserDataModel.UserId;
				var client = new RestClient(url);
				var request = new RestRequest(Method.GET);
				if (LoginUserDataModel.AccessToken != null)
				{
					client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", LoginUserDataModel.AccessToken));
				}
				var response = client.Execute(request);


				if (response != null)
				{
					StatusCode = Convert.ToInt32(response.StatusCode);
					if (response.StatusCode == HttpStatusCode.OK)
					{

						try
						{
							var result = JsonConvert.DeserializeObject<UserProfile>(response.Content);
							return result;
						}
						catch (JsonException ex)
						{
							throw new Exception("Json data could not be parsed. ", ex);
						}
						catch (Exception ex)
						{
							throw new Exception("Could not connect to the server. ", ex);
						}
					}
					if (StatusCode == 400)
						throw new Exception("The token does not exist in the database" +
											CommonHelper.Get_Response_Status_Message(StatusCode));
					throw new Exception("Failuer response from server. " +
										CommonHelper.Get_Response_Status_Message(StatusCode));
				}
				throw new Exception("Null responce from server. " + CommonHelper.Get_Response_Status_Message(StatusCode));
			}
			catch (JsonSerializationException e)
			{
				throw new Exception(
					"Json Data could not be parsed." + CommonHelper.Get_Response_Status_Message(StatusCode), e);
			}
			catch (Exception ex)
			{
				throw ex;
				//new Exception("Could not connect to the server." + CommonHelper.Get_Response_Status_Message(StatusCode), ex);
			}
		}

		#endregion



		#region userProfile

		public static async Task<UserProfile> GetGoingUserDetails()
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.UserProfileDetailsUrl + LoginUserDataModel.UserId;
				var client = new RestClient(url);
				var request = new RestRequest(Method.GET);
				if (LoginUserDataModel.AccessToken != null)
				{
					client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", LoginUserDataModel.AccessToken));
				}
				var response = client.Execute(request);


				if (response != null)
				{
					StatusCode = Convert.ToInt32(response.StatusCode);
					if (response.StatusCode == HttpStatusCode.OK)
					{

						try
						{
							var result = JsonConvert.DeserializeObject<UserProfile>(response.Content);
							return result;
						}
						catch (JsonException ex)
						{
							throw new Exception("Json data could not be parsed. ", ex);
						}
						catch (Exception ex)
						{
							throw new Exception("Could not connect to the server. ", ex);
						}
					}
					if (StatusCode == 400)
						throw new Exception("The token does not exist in the database" +
											CommonHelper.Get_Response_Status_Message(StatusCode));
					throw new Exception("Failuer response from server. " +
										CommonHelper.Get_Response_Status_Message(StatusCode));
				}
				throw new Exception("Null responce from server. " + CommonHelper.Get_Response_Status_Message(StatusCode));
			}
			catch (JsonSerializationException e)
			{
				throw new Exception(
					"Json Data could not be parsed." + CommonHelper.Get_Response_Status_Message(StatusCode), e);
			}
			catch (Exception ex)
			{
				throw ex;
				//new Exception("Could not connect to the server." + CommonHelper.Get_Response_Status_Message(StatusCode), ex);
			}
		}

		#endregion




	
	}
}
