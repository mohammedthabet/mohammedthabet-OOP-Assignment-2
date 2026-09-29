namespace SrpLab;

public sealed class TranscriptFormatter
{
    public string Format(
        GradeBook gradeBook,
        GradePolicy gradePolicy,
        string studentId,
        string fullName)
    {
        var average = gradeBook.Average(studentId);
        var letter = gradePolicy.Letter(average);
        var honor = gradePolicy.MeetsHonorRoll(average);

        return $"TRANSCRIPT\n" +
               $"Student: {fullName} ({studentId})\n" +
               $"Average: {average}\n" +
               $"Letter: {letter}\n" +
               $"Honor: {honor}\n";
    }
}