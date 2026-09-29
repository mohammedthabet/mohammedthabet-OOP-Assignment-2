namespace SrpLab;

public sealed class WelcomePacketWriter
{
    public string Write(
        CourseEnrollmentDesk course,
        string studentEmail,
        string studentName)
    {
        var position = course.WaitlistPosition(studentEmail);

        var status = position == -1
            ? "confirmed seat"
            : $"waitlist #{position}";

        return $"# Welcome to {course.CourseCode}\n" +
               $"Hi {studentName},\n" +
               $"Your status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: " +
               $"https://example.invalid/{course.CourseCode.ToLowerInvariant()}\n";
    }
}