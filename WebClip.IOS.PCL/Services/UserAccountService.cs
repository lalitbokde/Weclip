using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WeClip.Core.Model;

namespace WebClip.IOS.PCL
{
	public class UserAccountService
	{
		public static int StatusCode;

		#region FbpostServices

		public static async Task<Token> fbUserLogin(faceBookProfileInfo model)
		{
			StatusCode = 0;

			try
			{	
				var url = UrlHelper.AccountUrls.LoginfbServiceUrl;
				var client = new RestClient(url);
				var request = new RestRequest(Method.POST);

				if (model != null)
				{
					
					var json = JsonConvert.SerializeObject(model);
					request.AddParameter("application/json", json, ParameterType.RequestBody);
				}

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


	#endregion


		#region  googlepostservice
		public static async Task<Token> googleUserLogin(GoogleInfo model)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.LogingoogleServiceUrl;
				var client = new RestClient(url);
				var request = new RestRequest(Method.POST);

				if (model != null)
				{
					var json = JsonConvert.SerializeObject(model);
					request.AddParameter("application/json", json, ParameterType.RequestBody);
				}

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

		#endregion

		#region  SignUpWithEmailID

		public static async Task<Token> signUpWithEmailID(RegistrationModel model)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.UserRegistration;
				var client = new RestClient(url);
				var request = new RestRequest(Method.POST);

				if (model != null)
				{
					var json = JsonConvert.SerializeObject(model);
					request.AddParameter("application/json", json, ParameterType.RequestBody);
				}

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


		#endregion

		#region  SignInWithEmailID

		public static async Task<Token> signInWithEmailID(LoginModel model)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.LoginNew;
				var client = new RestClient(url);
				var request = new RestRequest(Method.POST);

				if (model != null)
				{
					var json = JsonConvert.SerializeObject(model);
					request.AddParameter("application/json", json, ParameterType.RequestBody);
				}

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


		#endregion

		#region EventCreate

		public static async Task<Token> createEvent(EventModel model)
		{
			StatusCode = 0;

			try
			{
				//model.creatorname = DataTransferModel.UserName;
				//model.EventType = "M";
				//model.CreatedOn = DateTime.UtcNow;
				model.CreatorID = 0;
				model.EventID = 0;
				model.FriendID = 0;
				model.EventCategory = model.EventCategory;
				model.EventPic = null;
				model.UserID = LoginUserDataModel.UserId;

				var url = UrlHelper.AccountUrls.EventCreateUrl;
				var client = new RestClient(url);
				var request = new RestRequest(Method.POST);
				if (LoginUserDataModel.AccessToken != null)
				{
					client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", LoginUserDataModel.AccessToken));
				}
				if (model != null)
				{

					var json = JsonConvert.SerializeObject(model);
					request.AddParameter("application/json", json, ParameterType.RequestBody);
				}

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


		#endregion


		//get

		#region GetPublicEvent

		public static async Task<List<EventModel>> GetPublicEvent()
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.GetEventUrl+LoginUserDataModel.UserId;
				var client = new RestClient(url);
				var request = new RestRequest(Method.GET);
				if(LoginUserDataModel.AccessToken!=null)
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
							var result = JsonConvert.DeserializeObject<List<EventModel>>(response.Content);
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
