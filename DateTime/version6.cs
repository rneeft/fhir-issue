#: package Hl7.Fhir.R4@6.2.1

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

var bundleXml = File.ReadAllText("MessageWithIncorrectDateTime.xml");

var converter = new FhirXmlConverter();
var bundle = converter.Deserialize<Bundle>(bundleXml);

var provenances = bundle.GetResources()
                     .OfType<Provenance>()
                     .ToList();

foreach (var provenance in provenances)
{
    ValidateRecorded(provenance);
}

Console.WriteLine("OK");


static void ValidateRecorded(Provenance provenance)
{
    if (!provenance.Recorded.HasValue)
    {
        Console.WriteLine("TILT");
    }
}

public class FhirXmlConverter
{
    private readonly FhirXmlDeserializer _deserializer;
    private readonly FhirXmlSerializer _serializer;

    public FhirXmlConverter()
    {
        _deserializer = new FhirXmlDeserializer(new DeserializerSettings().UsingMode(DeserializationMode.Recoverable));
        _serializer = new FhirXmlSerializer();
    }

    public T Deserialize<T>(string fhirXml) where T : Resource
    {
        return _deserializer.Deserialize<T>(fhirXml);
    }

    public string Serialize<T>(T resource) where T : Resource
    {
        return _serializer.SerializeToString(resource);
    }
}
