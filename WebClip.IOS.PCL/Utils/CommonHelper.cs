using System;
using System.Net.Mime;

namespace WebClip.IOS.PCL
{
	public class CommonHelper
	{
		public static string Get_Response_Status_Message(int statusCode)
		{
			switch (statusCode)
			{
				case 0:
					return "Data not found";
				case 101:
					return "SwitchingProtocols (" + statusCode + ") : The protocol version or protocol is being changed";
				// return "the protocol version or protocol is being changed";
				case 200:
					return "Ok";
				case 203:
					return "NonAuthoritativeInformation (" + statusCode +
						   ") : The returned metainformation is from a cached copy instead of the origin server and therefore may be incorrect";
				// return "The returned metainformation is from a cached copy instead of the origin server and therefore may be incorrect";
				case 204:
					return "NoContent (" + statusCode +
						   ") : The request has been successfully processed and that the response is intentionally blank";
				// return "The request has been successfully processed and that the response is intentionally blank";
				case 206:
					return "PartialContent (" + statusCode +
						   ") : The response is a partial response as requested by a GET request that includes a byte range";
				// return "The response is a partial response as requested by a GET request that includes a byte range";
				case 300:
					return "Ambiguous (" + statusCode + ") : The requested information has multiple representations";
				//return "
				case 301:
					return "Moved (" + statusCode +
						   ") : The requested information has been moved to the URI specified in the Location header";
				//return "The requested information has been moved to the URI specified in the Location header";
				case 302:
					return "Redirect (" + statusCode +
						   ") : The requested information is located at the URI specified in the Location header";
				// return "The requested information is located at the URI specified in the Location header";
				case 304:
					return "NotModified (" + statusCode +
						   ") : The client's cached copy is up to date. The contents of the resource are not transferred";
				// return "The client's cached copy is up to date. The contents of the resource are not transferred";
				case 305:
					return "UseProxy (" + statusCode +
						   ") : The request should use the proxy server at the URI specified in the Location header";
				// return "the request should use the proxy server at the URI specified in the Location header";
				case 400:
					return "BadRequest (" + statusCode + ") : The request could not be understood by the server";
				// return "The request could not be understood by the server";
				case 401:
					return "Unauthorized (" + statusCode + ") : The requested resource requires authentication";
				// return "The requested resource requires authentication";
				case 403:
					return "Forbidden (" + statusCode + ") : The server refuses to fulfill the request";
				//return "The server refuses to fulfill the request";
				case 404:
					return "NotFound (" + statusCode + ") : The requested resource does not exist on the server";
				//return "The requested resource does not exist on the server";
				case 405:
					return "MethodNotAllowed (" + statusCode +
						   ") : The request method (POST or GET) is not allowed on the requested resource";
				// return "The request method (POST or GET) is not allowed on the requested resource";
				case 406:
					return "NotAcceptable (" + statusCode +
						   ") : The client has indicated with Accept headers that it will not accept any of the available representations of the resource";
				// return "The client has indicated with Accept headers that it will not accept any of the available representations of the resource";
				case 407:
					return "ProxyAuthenticationRequired (" + statusCode +
						   ") : The requested proxy requires authentication. The Proxy-authenticate header contains the details of how to perform the authentication";
				// return "The requested proxy requires authentication. The Proxy-authenticate header contains the details of how to perform the authentication";
				case 408:
					return "RequestTimeout (" + statusCode +
						   ") : The client did not send a request within the time the server was expecting the request";
				// return "the client did not send a request within the time the server was expecting the request";
				case 409:
					return "Conflict (" + statusCode +
						   ") : The request could not be carried out because of a conflict on the server";
				//return "The request could not be carried out because of a conflict on the server";
				case 410:
					return "Gone (" + statusCode + ") : The requested resource is no longer available";
				//return "The requested resource is no longer available";
				case 411:
					return "LenghRequired (" + statusCode + ") : The required Content-length header is missing";
				//return "The required Content-length header is missing";
				case 413:
					return "RequestEntityTooLarge (" + statusCode +
						   ") : RequestEntityTooLarge indicates that the request is too large for the server to process";
				// return "RequestEntityTooLarge indicates that the request is too large for the server to process";
				case 414:
					return "RequestUriTooLong (" + statusCode +
						   ") : RequestUriTooLong indicates that the URI is too long";
				// return " RequestUriTooLong indicates that the URI is too long";
				case 415:
					return "UnsupportedMediaType (" + statusCode + ") : The request is an unsupported type";
				// return "the request is an unsupported type";
				case 416:
					return "RequestedRangeNotSatisfiable (" + statusCode +
						   ") : The range of data requested from the resource cannot be returned";
				// return "The range of data requested from the resource cannot be returned";
				case 426:
					return "UpgradeRequired (" + statusCode +
						   ") : The client should switch to a different protocol such as TLS/1.0";
				// return "the client should switch to a different protocol such as TLS/1.0";
				case 500:
					return "Internal server error (" + statusCode + ") : A generic error has occurred on the server";
				//return "A generic error has occurred on the server";
				case 501:
					return "NotImplemented (" + statusCode + ") : The server does not support the requested function";
				// return "The server does not support the requested function";
				case 502:
					return "BadGateWay (" + statusCode +
						   ") : Gateway received a bad response from another proxy or the origin server";
				//return "Gateway received a bad response from another proxy or the origin server.";
				case 503:
					return "ServiceUnavailable (" + statusCode +
						   ") : The server is temporarily unavailable, usually due to high load or maintenance";
				// return "the server is temporarily unavailable, usually due to high load or maintenance";
				case 504:
					return "Timeout (" + statusCode + ") : Request is time out";
				case 505:
					return "HttpVersionNotSupported (" + statusCode +
						   ") : The requested HTTP version is not supported by the server";
				//return "The requested HTTP version is not supported by the server";


				default:
					return "failure";
			}
		}


	}
}