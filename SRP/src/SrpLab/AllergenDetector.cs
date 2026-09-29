namespace SrpLab;

public sealed class AllergenDetector
{
    public IReadOnlyList<string> Detect(IEnumerable<string> ingredients)
    {
        var allergens = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        foreach (var ingredient in ingredients)
        {
            if (ingredient.Equals("wheat", StringComparison.OrdinalIgnoreCase))
                allergens.Add("gluten");

            if (ingredient.Equals("milk", StringComparison.OrdinalIgnoreCase))
                allergens.Add("dairy");

            if (ingredient.Equals("peanut", StringComparison.OrdinalIgnoreCase))
                allergens.Add("peanut");
        }

        return allergens.OrderBy(x => x).ToList();
    }
}