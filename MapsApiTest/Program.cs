using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Directions.Request;
using GoogleMapsApi;
using GoogleMapsApi.Directions.Response;
using GoogleMapsApi.Elevation.Response;
using GoogleMapsApi.Geocoding.Request;
using GoogleMapsApi.Elevation.Request;
using System.Reflection;
using GoogleMapsApi.Common;
using GoogleMapsApi.Geocoding.Response;

namespace MapsApiTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//Directions
			DirectionsRequest directionsRequest = new DirectionsRequest()
			{
				Origin = "NYC, 5th and 39",
				Destination = "Philladephia, Chesnut and Wallnut",
			};

			DirectionsResponse directions = MapsAPI.GetDirections(directionsRequest);


			//Geocode
			GeocodingRequest geocodeRequest = new GeocodingRequest()
			{
				Address = "new york city",
			};


			GeocodingResponse geocode = MapsAPI.GetGeocode(geocodeRequest);

			Console.WriteLine(geocode);

			//Elevation
			ElevationRequest elevationRequest = new ElevationRequest()
			{
				Locations = new Location[] { new Location(54, 78) },
			};


			ElevationResponse elevation = MapsAPI.GetElevation(elevationRequest);

			Console.WriteLine(elevation);
		}
	}
}
