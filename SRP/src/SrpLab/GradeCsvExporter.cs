namespace SrpLab;

public sealed class GradeCsvExporter
{
    public string Export(
        GradeBook gradeBook,
        GradePolicy gradePolicy)
    {
        var rows = new List<string>
        {
            "studentId,average,letter,honor"
        };

        foreach (var id in gradeBook.StudentIds())
        {
            var average = gradeBook.Average(id);
            var letter = gradePolicy.Letter(average);
            var honor = gradePolicy.MeetsHonorRoll(average);

            rows.Add(
                $"{id},{average},{letter},{(honor ? 1 : 0)}");
        }

        return string.Join('\n', rows);
    }
}