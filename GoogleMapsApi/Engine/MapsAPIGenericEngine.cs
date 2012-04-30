using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GoogleMapsApi.Entities.Common;
using System.Linq;
using GoogleMapsApi.Serialization;

namespace GoogleMapsApi.Engine
{
	public abstract class MapsAPIGenericEngine
	{
		protected static string BaseUrl;

		protected IObjectSerializerFactory objectSerializerFactory;

		static MapsAPIGenericEngine()
		{
			BaseUrl = @"maps.google.com/maps/api/";
		}

		protected MapsAPIGenericEngine()
		{
			objectSerializerFactory = new XmlObjectSerializerFactory();
		}

		protected IAsyncResult BeginQueryGoogleAPI<TRequest, TResponse>(TRequest request, AsyncCallback asyncCallback, object state)
			where TRequest : MapsBaseRequest
			where TResponse : class
		{
			// Must use TaskCompletionSource because in .NET 4.0 there's no overload of ContinueWith that accepts a state object (used in IAsyncResult).
			// Such overloads have been added in .NET 4.5, so this can be removed if/when the project is promoted to that version.
			// An example of such an added overload can be found at: http://msdn.microsoft.com/en-us/library/hh160386.aspx

			var completionSource = new TaskCompletionSource<TResponse>(state);
			QueryGoogleAPIAsync<TRequest, TResponse>(request).ContinueWith(t =>
			{
				if (t.IsFaulted)
					completionSource.SetException(t.Exception);
				else if (t.IsCanceled)
					completionSource.SetCanceled();
				else
					completionSource.SetResult(t.Result);

				asyncCallback(completionSource.Task);
			});

			return completionSource.Task;
		}

		protected TResponse EndQueryGoogleAPI<TResponse>(IAsyncResult asyncResult)
			where TResponse : class 
		{
			return ((Task<TResponse>)asyncResult).Result;
		}

		protected TResponse QueryGoogleAPI<TRequest, TResponse>(TRequest request)
			where TRequest : MapsBaseRequest
			where TResponse : class 
		{
			return QueryGoogleAPIAsync<TRequest, TResponse>(request).Result;
		}

		protected Task<TResponse> QueryGoogleAPIAsync<TRequest, TResponse>(TRequest request)
			where TRequest : MapsBaseRequest
			where TResponse : class
		{
			var client = new WebClient();
			ConfigureUnderlyingWebClient(client, request);
			return client.DownloadStringTaskAsync(GetUri(request))
				.ContinueWith(t => Deserialize<TRequest, TResponse>(request, t.Result), TaskContinuationOptions.ExecuteSynchronously);
		}

		protected abstract Uri GetUri(MapsBaseRequest request);
		protected abstract void ConfigureUnderlyingWebClient(WebClient wc, MapsBaseRequest request);

		private TResponse Deserialize<TRequest, TResponse>(TRequest request, string serializedObject)
			where TRequest : MapsBaseRequest
			where TResponse : class
		{
			var serializer = objectSerializerFactory.GetSerializer<TResponse>(request.Output);
			return (TResponse)serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(serializedObject)));
		}
	}
}