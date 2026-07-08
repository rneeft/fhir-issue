// NOTE: Run both of these versions to reproduce the bug. The bug is present in 6.1.1 but not in 5.12.2
// Run with (requires .NET 10 SDK): 
// dotnet run new.cs

#:package Hl7.Fhir.R4@6.1.1

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

var inputPath = Path.Combine(Environment.CurrentDirectory, "input.json");

if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Input file not found: {inputPath}");
    Console.Error.WriteLine("Place input.json next to the executable.");
    return;
}

//Note: This step (deserializing JSON) is just to build up a valid Bundle object in memory using available JSON examples.
var json = await File.ReadAllTextAsync(inputPath);
var deserializer = new FhirJsonDeserializer();
Bundle bundle = deserializer.Deserialize<Bundle>(json);

//Then, in order to reproduce the bug, we serialize the Bundle object to XML:
var serializer = new FhirXmlSerializer();
var xml = serializer.SerializeToString(bundle);
var version = GetFhirVersion();
var outputPath = Path.Combine(Environment.CurrentDirectory, $"FhirXmlSerializer-output-{version}.xml");
await File.WriteAllTextAsync(outputPath, xml);
Console.WriteLine($"Bundle serialized to XML using FhirXmlSerializer: {outputPath}");

static string GetFhirVersion()
    => typeof(Bundle).Assembly.GetName().Version?.ToString() ?? "unknown";

