namespace SrpLab;

public sealed class LoanDecisionLetterWriter
{
    public string Write(
        LoanDesk loan,
        LoanRiskEvaluator riskEvaluator,
        LoanDocumentPolicy documentPolicy,
        string applicantName)
    {
        var risk = riskEvaluator.RiskScore(loan);

        if (riskEvaluator.IsEligible(loan))
        {
            var documents =
                documentPolicy.RequiredDocuments(loan, riskEvaluator);

            return $"Dear {applicantName},\n" +
                   $"Your request for {loan.RequestedAmount:C} is pre-approved (risk {risk:0}).\n" +
                   $"Please upload: {string.Join("; ", documents)}.\n";
        }

        return $"Dear {applicantName},\n" +
               $"We are unable to approve {loan.RequestedAmount:C} at this time.\n" +
               $"Reference risk={risk:0}. You may reapply after improving documentation.\n";
    }
}