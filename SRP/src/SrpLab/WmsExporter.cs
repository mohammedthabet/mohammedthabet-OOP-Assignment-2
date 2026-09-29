namespace SrpLab;

public sealed class WmsExporter
{
    public string Export(
        IEnumerable<PickNeed> needs,
        StockAllocator allocator)
    {
        var rows = new List<string>
        {
            "sku,aisle,bin,requested,allocated"
        };

        foreach (var need in needs)
        {
            rows.Add(
                $"{need.Sku},{need.Aisle},{need.Bin}," +
                $"{need.Requested},{allocator.Allocated(need)}");
        }

        return string.Join('\n', rows);
    }
}