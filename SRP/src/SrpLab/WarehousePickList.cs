namespace SrpLab;

public sealed class WarehousePickList
{
    private readonly List<PickNeed> _needs = new();

    private readonly StockAllocator _allocator = new();
    private readonly WalkingRoutePlanner _routePlanner = new();
    private readonly PickerScriptWriter _scriptWriter = new();
    private readonly WmsExporter _wmsExporter = new();

    public void AddNeed(
        string sku,
        string aisle,
        int bin,
        int requested,
        int available)
    {
        _needs.Add(
            new PickNeed(
                sku,
                aisle,
                bin,
                requested,
                available));
    }

    public string PickerScript()
    {
        var route = _routePlanner.Plan(_needs);

        return _scriptWriter.Write(
            route,
            _allocator);
    }

    public string ExportForWms()
    {
        return _wmsExporter.Export(
            _needs,
            _allocator);
    }
}