using LibrarySystem;

Console.WriteLine("=== LIBRARY SYSTEM ===");
Console.WriteLine();

// ======================================================
// 1. POLYMORPHISM - STAFF
// ======================================================

Console.WriteLine("=== STAFF POLYMORPHISM ===");

List<Staff> staff =
[
    new Librarian(1, "Nora"),
    new Shelver(2, "Omar"),
    new HeadLibrarian(3, "Mona")
];

foreach (var person in staff)
{
    Console.WriteLine(
        $"{person.Name} -> {person.GetRole()}");
}

Console.WriteLine();

// ======================================================
// 2. POLYMORPHISM - LIBRARY ITEMS
// ======================================================

Console.WriteLine("=== ITEM POLYMORPHISM ===");

List<LibraryItem> items =
[
    new Book(1, "Clean Code"),
    new DVD(2, "C# Course"),
    new Magazine(3, "Tech Monthly")
];

foreach (var item in items)
{
    Console.WriteLine(
        $"{item.Title}: " +
        $"{item.GetLoanPeriodDays()} days, " +
        $"late fee = {item.LateFeePerDay:C}/day");
}

Console.WriteLine();

// ======================================================
// 3. WITHDRAWN ITEM
// ======================================================

Console.WriteLine("=== WITHDRAWN ITEM TEST ===");

var student = new Student(10, "Ali");
var withdrawnBook = new Book(10, "Old Book");

withdrawnBook.Withdraw();

try
{
    new Loan(
        student,
        withdrawnBook,
        new DateOnly(2026, 9, 1));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Blocked: {ex.Message}");
}

Console.WriteLine();

// ======================================================
// 4. ITEM ALREADY ON LOAN
// ======================================================

Console.WriteLine("=== UNAVAILABLE ITEM TEST ===");

var sharedBook = new Book(11, "C# in Depth");

var firstLoan = new Loan(
    student,
    sharedBook,
    new DateOnly(2026, 9, 1));

try
{
    var anotherStudent = new Student(11, "Hassan");

    new Loan(
        anotherStudent,
        sharedBook,
        new DateOnly(2026, 9, 2));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Blocked: {ex.Message}");
}

Console.WriteLine();

// ======================================================
// 5. STUDENT BORROW LIMIT
// ======================================================

Console.WriteLine("=== STUDENT LIMIT TEST ===");

var limitedStudent = new Student(20, "Mohamed");

var loan1 = new Loan(
    limitedStudent,
    new Book(20, "Book 1"),
    new DateOnly(2026, 9, 1));

var loan2 = new Loan(
    limitedStudent,
    new Book(21, "Book 2"),
    new DateOnly(2026, 9, 1));

var loan3 = new Loan(
    limitedStudent,
    new Book(22, "Book 3"),
    new DateOnly(2026, 9, 1));

try
{
    var loan4 = new Loan(
        limitedStudent,
        new Book(23, "Book 4"),
        new DateOnly(2026, 9, 1));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Blocked: {ex.Message}");
}

Console.WriteLine();

// ======================================================
// 6. PREMIUM MEMBER + LATE DVD
// ======================================================

Console.WriteLine("=== PREMIUM LATE FEE TEST ===");

var premium = new Premium(30, "Sara");
var dvd = new DVD(30, "Design Patterns Course");

var premiumLoan = new Loan(
    premium,
    dvd,
    new DateOnly(2026, 9, 1));

var returnedOn =
    premiumLoan.DueOn.AddDays(5);

decimal lateFee =
    premiumLoan.CalculateLateFee(returnedOn);

Console.WriteLine($"Borrowed: {premiumLoan.BorrowedOn}");
Console.WriteLine($"Due: {premiumLoan.DueOn}");
Console.WriteLine($"Returned: {returnedOn}");
Console.WriteLine($"Late fee: {lateFee:C}");

premiumLoan.Return();

Console.WriteLine(
    $"Status after return: {premiumLoan.Status}");

Console.WriteLine();

// ======================================================
// 7. INVALID STATE TRANSITIONS
// ======================================================

Console.WriteLine("=== INVALID TRANSITION TESTS ===");

try
{
    premiumLoan.Return();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Second return blocked: {ex.Message}");
}

try
{
    premiumLoan.MarkLost();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Mark returned loan as lost blocked: {ex.Message}");
}

Console.WriteLine();

Console.WriteLine("=== ALL TESTS COMPLETED ===");