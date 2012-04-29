using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
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

			//PlacesUri = new Uri(BaseUri, "place/");
		}

		protected MapsAPIGenericEngine()
		{
			objectSerializerFactory = new XmlObjectSerializerFactory();
		}

		protected IAsyncResult BeginQueryGoogleAPI<TRequest, TResponse>(TRequest request, AsyncCallback asyncCallback, object state)
			where TRequest : MapsBaseRequest
			where TResponse : class
		{
			TaskCompletionSource<TResponse> tcs = new TaskCompletionSource<TResponse>(state);
			
			//Set the webclient and initiate the async WebRequest in a new task.
			Task.Factory.StartNew(() =>
															{
																WebClient wc = new WebClient();

																ConfigureUnderlyingWebClient(wc, request);

																Uri uri = GetUri(request);

																OpenReadCompletedEventHandler completedEventHandler = null;

																completedEventHandler = (sender, args) =>
																													{
																														wc.OpenReadCompleted -= completedEventHandler;

																														Stream stream = args.Result;

																														try
																														{
																															XmlObjectSerializer serializer = GetSerializer<TResponse>(request);

																															TResponse result =
																																(TResponse)serializer.ReadObject(stream);

																															tcs.SetResult(result);
																														}
																														catch (Exception ex)
																														{
																															tcs.SetException(ex);
																														}

																													};

																wc.OpenReadCompleted += completedEventHandler;

																wc.OpenReadAsync(uri);
															}).ContinueWith(faultedTask => tcs.SetException(faultedTask.Exception),
																							TaskContinuationOptions.OnlyOnFaulted)
																.ContinueWith(task =>
																								{
																									if (asyncCallback != null)
																									{
																										asyncCallback(tcs.Task);
																									}
																								});

			return tcs.Task;
		}

		protected TResponse EndQueryGoogleAPI<TResponse>(IAsyncResult asyncResult)
			where TResponse : class 
		{
			Task<TResponse> task = (Task<TResponse>)asyncResult;

			return task.Result;
		}

		protected TResponse QueryGoogleAPI<TRequest, TResponse>(TRequest request)
			where TRequest : MapsBaseRequest
			where TResponse : class 
		{
			IAsyncResult asyncResult = BeginQueryGoogleAPI<TRequest, TResponse>(request, null, null);

			return EndQueryGoogleAPI<TResponse>(asyncResult);
		}


		abstract protected Uri GetUri(MapsBaseRequest request);

		protected abstract void ConfigureUnderlyingWebClient(WebClient wc, MapsBaseRequest request);
		
		protected virtual XmlObjectSerializer GetSerializer<TResponse>(MapsBaseRequest request)
			where TResponse : class 
		{
			return objectSerializerFactory.GetSerializer<TResponse>(request.Output);
		}
	}
}