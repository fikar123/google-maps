﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Engine;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Elevation.Request;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using NUnit.Framework;

/////////////////////////////////////////////////////////////////////////
//
//  Warning: The tests below run against the real Google API web
//           servers and count towards your query limit. Also, the tests
//           require a working internet connection in order to pass.
//           Their run time may vary depending on your connection,
//           network congestion and the current load on Google's servers.
//
/////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////
//
//  Note: Tests are written in the Arrange-Act-Assert pattern, where each
//        section is separated by a blank line.
//
/////////////////////////////////////////////////////////////////////////


namespace GoogleMapsApi.Test
{
	[TestFixture]
	public class IntegrationTests
	{
		[Test]
		public void Geocoding_ReturnsCorrectLocation()
		{
			var engine = new GeocodingEngine();
			var request = new GeocodingRequest { Address = "285 Bedford Ave, Brooklyn, NY 11211, USA" };

			var result = engine.GetGeocode(request);

			if (result.Status == Status.OVER_QUERY_LIMIT)
				Assert.Inconclusive("Cannot run test since you have exceeded your Google API query limit.");
			Assert.AreEqual(Status.OK, result.Status);
			Assert.AreEqual("40.7141289,-73.9614074", result.Results.First().Geometry.Location.LocationString);
		}

		[Test]
		public void ReverseGeocoding_ReturnsCorrectAddress()
		{
			var engine = new GeocodingEngine();
			var request = new GeocodingRequest { Location = new Location(40.7141289, -73.9614074) };

			var result = engine.GetGeocode(request);

			if (result.Status == Status.OVER_QUERY_LIMIT)
				Assert.Inconclusive("Cannot run test since you have exceeded your Google API query limit.");
			Assert.AreEqual(Status.OK, result.Status);
			Assert.AreEqual("285 Bedford Ave, Brooklyn, NY 11211, USA", result.Results.First().FormattedAddress);
		}

		[Test]
		public void Directions_SumOfStepDistancesCorrect()
		{
			var engine = new DirectionsEngine();
			var request = new DirectionsRequest { Origin = "285 Bedford Ave, Brooklyn, NY, USA", Destination = "185 Broadway Ave, Manhattan, NY, USA" };

			var result = engine.GetDirections(request);

			if (result.Status == DirectionsStatusCodes.OVER_QUERY_LIMIT)
				Assert.Inconclusive("Cannot run test since you have exceeded your Google API query limit.");
			Assert.AreEqual(DirectionsStatusCodes.OK, result.Status);
			Assert.AreEqual(8236, result.Routes.First().Legs.First().Steps.Sum(s => s.Distance.Value));
		}

		[Test]
		public void Elevation_ReturnsCorrectElevation()
		{
			var engine = new ElevationEngine();
			var request = new ElevationRequest { Locations = new[] { new Location(40.7141289, -73.9614074) } };

			var result = engine.GetElevation(request);

			if (result.Status == Entities.Elevation.Response.Status.OVER_QUERY_LIMIT)
				Assert.Inconclusive("Cannot run test since you have exceeded your Google API query limit.");
			Assert.AreEqual(Entities.Elevation.Response.Status.OK, result.Status);
			Assert.AreEqual(14.782454490661619, result.Results.First().Elevation);
		}
	}
}
