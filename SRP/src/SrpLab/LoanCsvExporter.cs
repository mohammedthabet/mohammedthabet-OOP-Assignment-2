namespace SrpLab;

public sealed class LoanCsvExporter
{
    public string Export(
        LoanDesk loan,
        LoanRiskEvaluator riskEvaluator,
        string applicationId)
    {
        return $"{applicationId}," +
               $"{loan.CreditScore}," +
               $"{loan.EmploymentMonths}," +
               $"{(loan.HasCollateral ? 1 : 0)}," +
               $"{riskEvaluator.RiskScore(loan):0.00}," +
               $"{(riskEvaluator.IsEligible(loan) ? "Y" : "N")}";
    }
}