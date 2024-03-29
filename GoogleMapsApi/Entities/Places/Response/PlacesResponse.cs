﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleMapsApi.Entities.Places.Response
{
    [DataContract]
    public class PlacesResponse
    {
        /// <summary>
        /// "status" contains metadata on the request.
        /// </summary>
        public Status Status { get; set; }

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

        /// <summary>
        /// "results" contains an array of places, with information about the place. See Place Search Results for information about these results. The Places API returns up to 20 establishment results. Additionally, political results may be returned which serve to identify the area of the request.
        /// </summary>
        [DataMember(Name="results")]
        public IEnumerable<Result> Results { get; set; }

        /// <summary>
        /// html_attributions contain a set of attributions about this listing which must be displayed to the user.
        /// </summary>
        //[DataMember(Name="html_attributions")]
        //public string HtmlAttributions { get; set; }
    }
}
