namespace SrpLab;

public sealed class ExpoLaneRouter
{
    public string Route(int etaMinutes, bool hasAllergens)
    {
        if (hasAllergens)
            return "ALLERGY";

        return etaMinutes <= 10
            ? "FAST"
            : "STANDARD";
    }
}