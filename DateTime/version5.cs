#: package Hl7.Fhir.R4@5.12.2

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
    private readonly FhirXmlParser parser;
    private readonly FhirXmlSerializer serializer;

    public FhirXmlConverter()
    {
        parser = new FhirXmlParser(new ParserSettings());
        serializer = new FhirXmlSerializer(new SerializerSettings());
    }

    public T Deserialize<T>(string fhirXml) where T : Resource
    {
        return parser.Parse<T>(fhirXml);
    }

    public string Serialize<T>(T resource) where T : Resource
    {
        return serializer.SerializeToString(resource);
    }
}
