using LibrarySystem;

Console.WriteLine("=== LIBRARY SYSTEM ===");
Console.WriteLine();

// Polymorphism through Person
List<Person> people =
[
    new Student(1, "Ali"),
    new Premium(2, "Sara"),
    new Librarian(3, "Nora"),
    new Shelver(4, "Omar"),
    new HeadLibrarian(5, "Mona")
];

Console.WriteLine("People:");

foreach (var person in people)
{
    Console.WriteLine(
        $"{person.Name} -> {person.GetRole()}");
}

Console.WriteLine();

// Polymorphism through LibraryItem
List<LibraryItem> items =
[
    new Book(1, "Clean Code"),
    new DVD(2, "C# Course"),
    new Magazine(3, "Tech Monthly")
];

Console.WriteLine("Library Items:");

foreach (var item in items)
{
    Console.WriteLine(
        $"{item.Title} -> loan period: {item.GetLoanPeriodDays()} days");
}

Console.WriteLine();

// Loan relationship
var student = new Student(10, "Mohamed");
var book = new Book(20, "C# in Depth");

var loan = new Loan(
    student,
    book,
    new DateOnly(2026, 9, 29));

Console.WriteLine("Loan:");
Console.WriteLine(
    $"{loan.Borrower.Name} borrowed {loan.Item.Title}");

Console.WriteLine($"Status: {loan.Status}");

loan.Return();

Console.WriteLine($"After return: {loan.Status}");