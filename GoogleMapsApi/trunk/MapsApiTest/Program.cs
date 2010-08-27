using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Directions.Request;
using GoogleMapsApi;
using GoogleMapsApi.Geocoding.Request;
using GoogleMapsApi.Elevation.Request;
using System.Reflection;

namespace MapsApiTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//Directions
			var directionsRequest = new DirectionsRequest()
			{
				Origin = "NYC, 5th and 39",
				Destination = "Philladephia, Chesnut and Wallnut",
			};

			var directions = MapsAPI.GetDirections(directionsRequest);


			//Geocode
			var geocodeRequest = new GeocodingRequest()
			{
				Address = "new york city",
			};


			var geocode = MapsAPI.GetGeocode(geocodeRequest);



			//Elevation
			var elevationRequest = new ElevationRequest()
			{
				Locations = new Location[] { new Location(54, 78) },
			};


			var elevation = MapsAPI.GetElevation(elevationRequest);

		
		}
	}
}
