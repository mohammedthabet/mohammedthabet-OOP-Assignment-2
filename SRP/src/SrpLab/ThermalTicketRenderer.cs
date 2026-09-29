namespace SrpLab;

public sealed class ThermalTicketRenderer
{
    public string Render(
        int orderNumber,
        IEnumerable<(string Name, int PrepMinutes)> items,
        int eta,
        IReadOnlyList<string> allergens)
    {
        var lines = new List<string>
        {
            "================================",
            $"ORDER #{orderNumber}",
            $"ETA {eta} MIN"
        };

        foreach (var item in items)
            lines.Add($"* {item.Name.ToUpperInvariant()} ({item.PrepMinutes}m)");

        if (allergens.Count > 0)
            lines.Add($"ALLERGENS: {string.Join(",", allergens)}");

        lines.Add("================================");

        return string.Join('\n', lines) + "\n";
    }
}