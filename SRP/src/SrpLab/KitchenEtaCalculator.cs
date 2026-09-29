namespace SrpLab;

public sealed class KitchenEtaCalculator
{
    public int Calculate(IEnumerable<int> prepMinutes)
    {
        return prepMinutes.Any()
            ? prepMinutes.Max()
            : 0;
    }
}