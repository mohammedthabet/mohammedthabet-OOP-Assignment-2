namespace SrpLab;

public sealed class TuitionInvoiceFormatter
{
    public string Format(
        CourseEnrollmentDesk course,
        string studentEmail)
    {
        if (!course.IsSeated(studentEmail))
            return $"{course.CourseCode},WAITLIST,0.00";

        var vat = Math.Round(course.Tuition * 0.14m, 2);

        return $"{course.CourseCode},TUITION,{course.Tuition:0.00}," +
               $"VAT,{vat:0.00},TOTAL,{(course.Tuition + vat):0.00}";
    }
}