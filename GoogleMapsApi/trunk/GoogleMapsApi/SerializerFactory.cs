using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace GoogleMapsApi
{
	internal static class SerializerFactory
	{
		internal static XmlObjectSerializer GetSerializer<TRespnseType>(ResponseOutputType outputType) where TRespnseType : class
		{
			XmlObjectSerializer serializer;
			switch (outputType)
			{
				case ResponseOutputType.XML:
					serializer = new DataContractSerializer(typeof(TRespnseType));
					break;
				case ResponseOutputType.JSON:
					serializer = new DataContractJsonSerializer(typeof(TRespnseType));

					break;
				default:
					throw new ArgumentException(outputType+ " is unknown output type.");
			}

			return serializer;
		}
	}
}
