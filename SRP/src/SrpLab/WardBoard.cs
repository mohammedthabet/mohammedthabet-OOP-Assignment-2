namespace SrpLab;

public sealed class WardBoard
{
    private readonly Dictionary<int, (string PatientId, int Acuity)> _beds =
        new();

    private readonly AcuityScorer _acuityScorer = new();
    private readonly WardPager _pager = new();
    private readonly HandoffFormatter _handoffFormatter = new();
    private readonly CensusExporter _censusExporter = new();

    public void AssignBed(
        int bedNumber,
        string patientId,
        int heartRate,
        int spo2)
    {
        var acuity =
            _acuityScorer.Calculate(heartRate, spo2);

        _beds[bedNumber] = (patientId, acuity);

        if (acuity >= 7)
            _pager.Escalate(bedNumber);
    }

    public string BuildHandoffNote(int bedNumber)
    {
        var patient = _beds[bedNumber];

        return _handoffFormatter.Format(
            bedNumber,
            patient.PatientId,
            patient.Acuity);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pager.Drain();
    }

    public string ExportCensusCsv()
    {
        return _censusExporter.Export(_beds);
    }
}