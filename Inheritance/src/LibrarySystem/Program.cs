using LibrarySystem;

Console.WriteLine("=== LIBRARY SYSTEM PROOF ===");


// ==================================================
// STAFF
// ==================================================

Console.WriteLine("\n=== STAFF PAY ===");

List<Staff> staff =
[
    new Librarian(
        1,
        "Nora",
        "01000000001",
        new DateOnly(2024, 1, 1),
        5000m),

    new Shelver(
        2,
        "Omar",
        "01000000002",
        new DateOnly(2024, 2, 1),
        4000m,
        "A"),

    new HeadLibrarian(
        3,
        "Mona",
        "01000000003",
        new DateOnly(2023, 1, 1),
        7000m)
];

foreach (Staff employee in staff)
{
    Console.WriteLine(
        $"{employee.FullName}: " +
        $"${employee.CalculateMonthlyPay():0.00}");
}


// ==================================================
// LIBRARY ITEMS
// ==================================================

Console.WriteLine("\n=== LIBRARY ITEMS ===");

List<LibraryItem> items =
[
    new Book(
        "B-001",
        "Clean Code",
        1m),

    new DVD(
        "D-001",
        "C# Course",
        2m),

    new Magazine(
        "M-001",
        "Tech Monthly",
        0.50m)
];

foreach (LibraryItem item in items)
{
    Console.WriteLine(
        $"{item.Title}: " +
        $"{item.LoanPeriodDays} days, " +
        $"daily late fee = " +
        $"${item.CalculateDailyLateFee():0.00}");
}


// ==================================================
// WITHDRAWN ITEM
// ==================================================

Console.WriteLine("\n=== WITHDRAWN ITEM TEST ===");

Student ali =
    new Student(
        10,
        "Ali",
        "01011111111");

Book withdrawnBook =
    new Book(
        "B-002",
        "Refactoring",
        1m);

withdrawnBook.Withdraw();

try
{
    ali.Borrow(
        1,
        withdrawnBook,
        new DateOnly(2026, 9, 1));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Blocked: {ex.Message}");
}


// ==================================================
// ALREADY LOANED ITEM
// ==================================================

Console.WriteLine("\n=== ALREADY-LOANED ITEM TEST ===");

Book sharedBook =
    new Book(
        "B-003",
        "Domain-Driven Design",
        1m);

ali.Borrow(
    2,
    sharedBook,
    new DateOnly(2026, 9, 1));

Premium sara =
    new Premium(
        11,
        "Sara",
        "01022222222");

try
{
    sara.Borrow(
        3,
        sharedBook,
        new DateOnly(2026, 9, 2));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Blocked: {ex.Message}");
}


// ==================================================
// STUDENT BORROWING LIMIT
// ==================================================

Console.WriteLine("\n=== STUDENT LIMIT TEST ===");

Student mohamed =
    new Student(
        20,
        "Mohamed",
        "01033333333");

mohamed.Borrow(
    10,
    new Book("B-010", "Book 1", 1m),
    new DateOnly(2026, 9, 1));

mohamed.Borrow(
    11,
    new Book("B-011", "Book 2", 1m),
    new DateOnly(2026, 9, 1));

mohamed.Borrow(
    12,
    new Book("B-012", "Book 3", 1m),
    new DateOnly(2026, 9, 1));

try
{
    mohamed.Borrow(
        13,
        new Book(
            "B-013",
            "Book 4",
            1m),
        new DateOnly(2026, 9, 1));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Blocked: {ex.Message}");
}


// ==================================================
// PREMIUM LATE FEE + READING POINTS
// ==================================================

Console.WriteLine("\n=== PREMIUM LATE FEE TEST ===");

Premium premium =
    new Premium(
        30,
        "Premium User",
        "01044444444");

DVD dvd =
    new DVD(
        "D-010",
        "Design Patterns Course",
        2m);

Loan premiumLoan =
    premium.Borrow(
        20,
        dvd,
        new DateOnly(2026, 9, 1));

DateOnly returnDate =
    premiumLoan.DueDate.AddDays(5);

decimal lateFee =
    premiumLoan.Return(returnDate);

Console.WriteLine(
    $"Borrowed: {premiumLoan.BorrowDate}");

Console.WriteLine(
    $"Due: {premiumLoan.DueDate}");

Console.WriteLine(
    $"Returned: {premiumLoan.ReturnDate}");

Console.WriteLine(
    $"Late fee: ${lateFee:0.00}");

Console.WriteLine(
    $"Reading points: {premium.ReadingPoints}");

Console.WriteLine(
    $"Status: {premiumLoan.Status}");


// ==================================================
// INVALID TRANSITIONS
// ==================================================

Console.WriteLine("\n=== INVALID TRANSITION TESTS ===");

try
{
    premiumLoan.Return(returnDate);
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
        $"Returned loan -> Lost blocked: {ex.Message}");
}


// ==================================================
// PRICING METHOD TEST
// ==================================================

Console.WriteLine("\n=== PRICING METHOD TEST ===");

Book pricingBook =
    new Book(
        "B-100",
        "Pricing Test",
        1m);

Console.WriteLine(
    $"Before: ${pricingBook.CalculateDailyLateFee():0.00}");

pricingBook.ChangeBaseLateFee(1.50m);

Console.WriteLine(
    $"After: ${pricingBook.CalculateDailyLateFee():0.00}");


// ==================================================
// MUST NOT COMPILE PROOFS
// Keep these lines commented.
// ==================================================

Console.WriteLine("\n=== COMPILE-TIME PROTECTION ===");

// MUST NOT COMPILE:
// Person constructor is protected.
// var p = new Person(
//     1,
//     "Test",
//     "01000000000");

// MUST NOT COMPILE:
// Member constructor is protected.
// var m = new Member(
//     1,
//     "Test",
//     "01000000000",
//     3,
//     0m);

// MUST NOT COMPILE:
// Staff constructor is protected.
// var s = new Staff(
//     1,
//     "Test",
//     "01000000000",
//     DateOnly.FromDateTime(DateTime.Today),
//     5000m,
//     0m);

// MUST NOT COMPILE:
// LibraryItem constructor is protected.
// var item = new LibraryItem(
//     "X",
//     "Test",
//     7,
//     1m,
//     1m);

// MUST NOT COMPILE:
// FullName is get-only.
// mohamed.FullName = "Changed";

// MUST NOT COMPILE:
// Loans is IReadOnlyList.
// mohamed.Loans.Add(premiumLoan);

// MUST NOT COMPILE:
// IsOnLoan has a private setter.
// dvd.IsOnLoan = false;

// MUST NOT COMPILE:
// BaseLateFee has a private setter.
// dvd.BaseLateFee = 100m;

// MUST NOT COMPILE:
// ReadingPoints is computed get-only.
// premium.ReadingPoints = 500;

Console.WriteLine(
    "Compile-time protection examples are included as comments.");

Console.WriteLine(
    "\n=== ALL TESTS COMPLETED ===");