using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi;
using GoogleMapsApi.Engine;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Elevation.Request;
using GoogleMapsApi.Entities.Elevation.Response;
using System.Reflection;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;

namespace MapsApiTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//Static class use (Directions)
			DirectionsRequest directionsRequest = new DirectionsRequest()
			{
				Origin = "NYC, 5th and 39",
				Destination = "Philladephia, Chesnut and Wallnut",
			};

			DirectionsResponse directions = MapsAPI.GetDirections(directionsRequest);

			Console.WriteLine(directions);


			//Instance class use (Geocode)
			GeocodingRequest geocodeRequest = new GeocodingRequest()
			{
				Address = "new york city",
			};

			GeocodingEngine geocodingEngine = new GeocodingEngine();

			GeocodingResponse geocode = geocodingEngine.GetGeocode(geocodeRequest);

			Console.WriteLine(geocode);


			//Instance class - Async! (Elevation)
			ElevationRequest elevationRequest = new ElevationRequest()
			{
				Locations = new Location[] { new Location(54, 78) },
			};

			ElevationEngine elevationEngine = new ElevationEngine();

			elevationEngine.BeginGetElevation(elevationRequest,
																				ar =>
																				{
																					ElevationResponse elevation = elevationEngine.EndGetElevation(ar);
																					Console.WriteLine(elevation);
																				},
																				null);

			Console.WriteLine("Finised! (But wait .. async elevation request should get response soon)");

			Console.ReadKey();
		}
	}
}
