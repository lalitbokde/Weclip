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
	public class EventService
	{
		public static int StatusCode;

		long totalBytes = 0;
		long uploadedBytes = 0;
		int percentComplete = 0;
		private long MaxVideoSize;
		WebClient wClient;

		public EventService()
		{
		}

		#region Event details
		//get Event Details


		public static async Task<EventDetails> getEventDetails(long EventId)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.GetEventDetailsUrl + EventId;
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
							var result = JsonConvert.DeserializeObject<EventDetails>(response.Content);
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


		#region weclip data

		public static async Task<List<WeClipVideo>> getWeClipVideo(long EventId)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.GetWeClipVideoUrl + EventId;
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
							var result = JsonConvert.DeserializeObject<List<WeClipVideo>>(response.Content);

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

		public static async Task<List<EventFiles>> getEventImages(long EventId)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.GetImagesUrl + EventId;
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
							var result = JsonConvert.DeserializeObject<List<EventFiles>>(response.Content);

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


		#region Event Comment Data

		public static async Task<List<EventFeedModel>> getEventComments(long EventId)
		{
			StatusCode = 0;

			try
			{
				var url = UrlHelper.AccountUrls.GetEventFeedUrl + EventId;
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
							var result = JsonConvert.DeserializeObject<List<EventFeedModel>>(response.Content);

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



		#region ImagePost
		//camera Post
		public static async Task<List<EventModel>> PostCameraImage(MediaFile file)
		{


			try
			{
				Uri uri;

				if (file.MediaType == MediaType.Photo)
				{
					uri = new Uri(UrlHelper.AccountUrls.PostPhoto + file.EventID);
				}
				else
				{
					uri = new Uri(UrlHelper.AccountUrls.PostVideo + file.EventID);
				}


				var client = new RestClient(uri);
				var request = new RestRequest(Method.POST);
				if (LoginUserDataModel.AccessToken != null)
				{
					client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", LoginUserDataModel.AccessToken));
				}

				if (file != null)
				{

					var json = JsonConvert.SerializeObject(file);
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

		public async Task UploadFiles(List<MediaFile> mediaFiles)
		{
			foreach (var file in mediaFiles)
			{
				await UploadSingleFile(file);
			}
		}

		public async Task UploadSingleFile(MediaFile file)
		{
			Uri uri;

			if (file.MediaType == MediaType.Photo)
			{
				uri = new Uri(UrlHelper.AccountUrls.PostPhoto + file.EventID);
			}
			else
			{
				uri = new Uri(UrlHelper.AccountUrls.PostVideo + file.EventID);
			}

			string uploadFilePath = file.FilePath;
			FileInfo finfo = new FileInfo(uploadFilePath);
			totalBytes = finfo.Length;

			if (totalBytes == 0) return;
			double totalMB = Convert.ToDouble(totalBytes) / (1024 * 1024); // Size in MB

			//if (totalMB > MaxVideoSize)
			//{


			//	return;
			//}
			//else
			//{

				//  && (totalBytes / 1024) > 500
				if (file.MediaType == MediaType.Photo)
				{

					var documentsDirectory = Environment.GetFolderPath
												  (Environment.SpecialFolder.Personal);
					string jpgFilename = System.IO.Path.Combine(documentsDirectory, "Photo.jpg");
					//NSData imgData = PhotoCapture.AsJPEG();
					//NSError err = null;



					//uploadFilePath = CompressFile.compressImage(uploadFilePath);
					finfo = new FileInfo(uploadFilePath);
					totalBytes = finfo.Length;
				}

				uploadedBytes = 0;
				percentComplete = 0;
				var totalfileSize = Math.Round(totalMB, 2);
				wClient = new WebClient();
				wClient.Headers.Add("authorization", "bearer " + LoginUserDataModel.AccessToken);
				wClient.UploadFileAsync(uri, uploadFilePath);
				wClient.UploadFileCompleted +=WClient_UploadFileCompleted;
				//wClient.UploadProgressChanged += new UploadProgressChangedEventHandler(ProgressChanged);
				//wClient.UploadFileCompleted += WClient_UploadFileCompleted;
			//}
		}

		void WClient_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
		{
			//throw new NotImplementedException();
		}

		#endregion


		#region Comment post

		public static async Task<Token> PostComment(EventFeedModel model)
		{
			StatusCode = 0;

			try
			{
				
			
				model.UserID = LoginUserDataModel.UserId;

				var url = UrlHelper.AccountUrls.EventCommentPostUrl;
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




}
}
