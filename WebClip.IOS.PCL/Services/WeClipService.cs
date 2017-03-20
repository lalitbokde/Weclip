using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Newtonsoft.Json;
using RestSharp;
using WeClip.Core.Model;
namespace WebClip.IOS.PCL
{
	public class WeClipService
	{
		public static int StatusCode;
		public WeClipService()
		{
		}

		#region WeClip Video      

		public static async Task<Token> CreateWeclipVideo(WeClipInfo model)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.CreateWeclipVideo + model.EventID;
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


		#region Event Image data

		public static async Task<List<Theme>> getWeClipThemes()
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.GetWeClipThemes;
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
							var result = JsonConvert.DeserializeObject<List<Theme>>(response.Content);

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
