namespace SrpLab;

public sealed class AcuityScorer
{
    public int Calculate(int heartRate, int spo2)
    {
        var score = 0;

        if (heartRate > 120)
            score += 4;
        else if (heartRate > 100)
            score += 2;

        if (spo2 < 90)
            score += 5;
        else if (spo2 < 94)
            score += 3;

        return score;
    }
}