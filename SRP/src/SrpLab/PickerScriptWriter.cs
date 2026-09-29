namespace SrpLab;

public sealed class PickerScriptWriter
{
    public string Write(
        IEnumerable<PickNeed> route,
        StockAllocator allocator)
    {
        var needs = route.ToList();
        var lines = new List<string>();

        var step = 1;

        foreach (var need in needs)
        {
            lines.Add(
                $"{step}. Go aisle {need.Aisle} bin {need.Bin}: " +
                $"pick {allocator.Allocated(need)} × {need.Sku}");

            step++;
        }

        var shortages = needs
            .Where(allocator.HasShortage)
            .Select(x => x.Sku)
            .ToList();

        if (shortages.Count > 0)
            lines.Add($"SHORTAGES: {string.Join(",", shortages)}");

        return string.Join('\n', lines);
    }
}