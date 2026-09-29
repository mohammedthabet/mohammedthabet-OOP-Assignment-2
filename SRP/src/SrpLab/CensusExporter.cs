namespace SrpLab;

public sealed class CensusExporter
{
    public string Export(
        IReadOnlyDictionary<int, (string PatientId, int Acuity)> beds)
    {
        var rows = new List<string>
        {
            "bed,patient,acuity"
        };

        foreach (var pair in beds.OrderBy(x => x.Key))
        {
            rows.Add(
                $"{pair.Key}," +
                $"{pair.Value.PatientId}," +
                $"{pair.Value.Acuity}");
        }

        return string.Join('\n', rows);
    }
}