namespace LibrarySystem;

// ========================
// PERSON HIERARCHY
// ========================

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


// ========================
// STAFF HIERARCHY
// ========================

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


// ========================
// LIBRARY ITEM HIERARCHY
// ========================

public abstract class LibraryItem
{
    public int Id { get; }
    public string Title { get; }

    protected LibraryItem(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public abstract int GetLoanPeriodDays();
}

public sealed class Book : LibraryItem
{
    public Book(int id, string title)
        : base(id, title)
    {
    }

    public override int GetLoanPeriodDays() => 14;
}

public sealed class DVD : LibraryItem
{
    public DVD(int id, string title)
        : base(id, title)
    {
    }

    public override int GetLoanPeriodDays() => 7;
}

public sealed class Magazine : LibraryItem
{
    public Magazine(int id, string title)
        : base(id, title)
    {
    }

    public override int GetLoanPeriodDays() => 3;
}


// ========================
// LOAN
// ========================

public enum LoanStatus
{
    Borrowed,
    Returned,
    Lost
}

public sealed class Loan
{
    public Member Borrower { get; }
    public LibraryItem Item { get; }
    public DateOnly BorrowedOn { get; }
    public LoanStatus Status { get; private set; }

    public Loan(Member borrower, LibraryItem item, DateOnly borrowedOn)
    {
        Borrower = borrower;
        Item = item;
        BorrowedOn = borrowedOn;
        Status = LoanStatus.Borrowed;
    }

    public void Return()
    {
        Status = LoanStatus.Returned;
    }

    public void MarkLost()
    {
        Status = LoanStatus.Lost;
    }
}