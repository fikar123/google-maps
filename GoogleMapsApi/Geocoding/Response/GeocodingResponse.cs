﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GoogleMapsApi.Geocoding.Response
{
	[DataContract]
	public class GeocodingResponse
	{
		[DataMember(Name = "status")]
		public string StatusStr
		{
			get
			{
				return Status.ToString();
			}
			set
			{
				Status = (Status)Enum.Parse(typeof(Status), value);
			}
		}

		public Status Status { get; set; }

		[DataMember(Name = "results")]
		public IEnumerable<Result> Results { get; set; }

		public override string ToString()
		{
			return string.Format("GeocodingResponse - Status: {0}, Results count: {1}", Status, Results != null ? Results.Count() : 0);
		}
	}
}
