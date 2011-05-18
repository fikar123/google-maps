using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using GoogleMapsApi.Common;

namespace GoogleMapsApi.Serialization
{
	internal class XmlObjectSerializerFactory : IObjectSerializerFactory
	{
		public XmlObjectSerializer GetSerializer<TResponseType>(ResponseOutputType outputType) where TResponseType : class
		{
			XmlObjectSerializer serializer;
			switch (outputType)
			{
				case ResponseOutputType.XML:
					serializer = new DataContractSerializer(typeof(TResponseType));
					break;
				case ResponseOutputType.JSON:
					serializer = new DataContractJsonSerializer(typeof(TResponseType));

					break;
				default:
					throw new ArgumentException(outputType + " is unknown output type.");
			}

			return serializer;
		}
	}
}
