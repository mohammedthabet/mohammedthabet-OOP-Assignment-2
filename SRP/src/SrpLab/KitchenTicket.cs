namespace SrpLab;

public sealed class KitchenTicket
{
    private sealed record Item(
        string Name,
        IReadOnlyList<string> Ingredients,
        int PrepMinutes);

    private readonly List<Item> _items = new();

    private readonly AllergenDetector _allergenDetector = new();
    private readonly KitchenEtaCalculator _etaCalculator = new();
    private readonly ThermalTicketRenderer _renderer = new();
    private readonly ExpoLaneRouter _router = new();

    public void AddItem(
        string name,
        IEnumerable<string> ingredients,
        int prepMinutes)
    {
        _items.Add(
            new Item(
                name,
                ingredients.ToList(),
                prepMinutes));
    }

    public int EstimatedMinutes()
    {
        return _etaCalculator.Calculate(
            _items.Select(x => x.PrepMinutes));
    }

    public IReadOnlyList<string> Allergens()
    {
        return _allergenDetector.Detect(
            _items.SelectMany(x => x.Ingredients));
    }

    public string RenderThermalTicket(int orderNumber)
    {
        return _renderer.Render(
            orderNumber,
            _items.Select(x => (x.Name, x.PrepMinutes)),
            EstimatedMinutes(),
            Allergens());
    }

    public string ExpoLane()
    {
        return _router.Route(
            EstimatedMinutes(),
            Allergens().Count > 0);
    }
}