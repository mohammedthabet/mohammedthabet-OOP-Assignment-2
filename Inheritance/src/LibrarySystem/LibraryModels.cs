namespace LibrarySystem;

// ======================================================
// PERSON HIERARCHY
// ======================================================

public abstract class Person
{
    public int Id { get; }
    public string Name { get; }

    protected Person(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public abstract string GetRole();
}

public abstract class Member : Person
{
    public int MaxBorrowLimit { get; protected set; }

    protected Member(int id, string name, int maxBorrowLimit)
        : base(id, name)
    {
        MaxBorrowLimit = maxBorrowLimit;
    }
}

public sealed class Student : Member
{
    public Student(int id, string name)
        : base(id, name, 3)
    {
    }

    public override string GetRole() => "Student Member";
}

public sealed class Premium : Member
{
    public Premium(int id, string name)
        : base(id, name, 10)
    {
    }

    public override string GetRole() => "Premium Member";
}

// ======================================================
// STAFF HIERARCHY
// ======================================================

public abstract class Staff : Person
{
    protected Staff(int id, string name)
        : base(id, name)
    {
    }
}

public sealed class Librarian : Staff
{
    public Librarian(int id, string name)
        : base(id, name)
    {
    }

    public override string GetRole() => "Librarian";
}

public sealed class Shelver : Staff
{
    public Shelver(int id, string name)
        : base(id, name)
    {
    }

    public override string GetRole() => "Shelver";
}

public sealed class HeadLibrarian : Staff
{
    public HeadLibrarian(int id, string name)
        : base(id, name)
    {
    }

    public override string GetRole() => "Head Librarian";
}

// ======================================================
// LIBRARY ITEM HIERARCHY
// ======================================================

public abstract class LibraryItem
{
    public int Id { get; }
    public string Title { get; }

    public bool IsWithdrawn { get; private set; }
    public bool IsAvailable { get; internal set; } = true;

    protected LibraryItem(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public void Withdraw()
    {
        IsWithdrawn = true;
    }

    public abstract int GetLoanPeriodDays();

    public abstract decimal LateFeePerDay { get; }
}

public sealed class Book : LibraryItem
{
    public Book(int id, string title)
        : base(id, title)
    {
    }

    public override int GetLoanPeriodDays() => 14;

    public override decimal LateFeePerDay => 1m;
}

public sealed class DVD : LibraryItem
{
    public DVD(int id, string title)
        : base(id, title)
    {
    }

    public override int GetLoanPeriodDays() => 7;

    public override decimal LateFeePerDay => 2m;
}

public sealed class Magazine : LibraryItem
{
    public Magazine(int id, string title)
        : base(id, title)
    {
    }

    public override int GetLoanPeriodDays() => 3;

    public override decimal LateFeePerDay => 0.5m;
}

// ======================================================
// LOAN
// ======================================================

public enum LoanStatus
{
    Borrowed,
    Returned,
    Lost
}

public sealed class Loan
{
    private static readonly List<Loan> AllLoans = new();

    public Member Borrower { get; }
    public LibraryItem Item { get; }

    public DateOnly BorrowedOn { get; }
    public DateOnly DueOn { get; }

    public LoanStatus Status { get; private set; }

    public Loan(
        Member borrower,
        LibraryItem item,
        DateOnly borrowedOn)
    {
        if (item.IsWithdrawn)
            throw new InvalidOperationException(
                "Cannot borrow a withdrawn item.");

        if (!item.IsAvailable)
            throw new InvalidOperationException(
                "Cannot borrow an unavailable item.");

        int activeLoans = AllLoans.Count(
            loan =>
                loan.Borrower == borrower &&
                loan.Status == LoanStatus.Borrowed);

        if (activeLoans >= borrower.MaxBorrowLimit)
            throw new InvalidOperationException(
                $"{borrower.Name} reached the borrowing limit.");

        Borrower = borrower;
        Item = item;
        BorrowedOn = borrowedOn;

        DueOn = borrowedOn.AddDays(
            item.GetLoanPeriodDays());

        Status = LoanStatus.Borrowed;

        item.IsAvailable = false;

        AllLoans.Add(this);
    }

    public void Return()
    {
        EnsureBorrowed();

        Status = LoanStatus.Returned;
        Item.IsAvailable = true;
    }

    public void MarkLost()
    {
        EnsureBorrowed();

        Status = LoanStatus.Lost;
        Item.IsAvailable = false;
    }

    public decimal CalculateLateFee(DateOnly returnedOn)
    {
        if (returnedOn <= DueOn)
            return 0m;

        int lateDays =
            returnedOn.DayNumber - DueOn.DayNumber;

        return lateDays * Item.LateFeePerDay;
    }

    private void EnsureBorrowed()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Loan is no longer active.");
    }
}