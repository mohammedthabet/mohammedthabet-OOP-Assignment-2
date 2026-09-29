namespace SrpLab;

public sealed class HandoffFormatter
{
    public string Format(
        int bedNumber,
        string patientId,
        int acuity)
    {
        var escalation =
            acuity >= 7 ? "ESCALATE" : "routine";

        return $"[HANDOFF {DateTimeOffset.Now:yyyy-MM-dd}] " +
               $"Bed {bedNumber} · {patientId.ToUpperInvariant()} · " +
               $"acuity={acuity} · {escalation}";
    }
}