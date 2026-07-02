# Breaking change between version 5.x.x and 6.x.x

```bash
dotnet run version5.cs
```

returns `OK`

```bash
dotnet run version6.cs
```
returns

```
Unhandled exception. Hl7.Fhir.Validation.CodedValidationException: string '2022-10-04T11:03:48.472' is not a correct literal for a instant.
   at Hl7.Fhir.Model.Instant.get_Value()
   at Hl7.Fhir.Model.Provenance.get_Recorded()
   at Program.<<Main>$>g__ValidateRecorded|0_0(Provenance provenance) in D:\fhir-issue\DateTime-fhir6.cs:line 25
   at Program.<Main>$(String[] args) in D:\fhir-issue\DateTime-fhir6.cs:line 17
```