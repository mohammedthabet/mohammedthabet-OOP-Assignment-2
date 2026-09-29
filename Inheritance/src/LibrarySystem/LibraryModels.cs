namespace LibrarySystem;


// ==================================================
// PERSON
// ==================================================

public class Person
{
    public int PersonId { get; }
    public string FullName { get; }
    public string Phone { get; }

    protected Person(
        int personId,
        string fullName,
        string phone)
    {
        if (personId <= 0)
            throw new ArgumentException(
                "Person ID must be greater than zero.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException(
                "Full name is required.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException(
                "Phone is required.");

        PersonId = personId;
        FullName = fullName;
        Phone = phone;
    }
}


// ==================================================
// STAFF
// ==================================================

public class Staff : Person
{
    public DateOnly HireDate { get; }

    public decimal MonthlySalary { get; private set; }

    protected decimal ResponsibilityAllowance { get; }

    protected Staff(
        int personId,
        string fullName,
        string phone,
        DateOnly hireDate,
        decimal monthlySalary,
        decimal responsibilityAllowance)
        : base(personId, fullName, phone)
    {
        if (monthlySalary <= 0)
            throw new ArgumentException(
                "Monthly salary must be greater than zero.");

        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        ResponsibilityAllowance = responsibilityAllowance;
    }

    public decimal CalculateMonthlyPay()
    {
        return MonthlySalary + ResponsibilityAllowance;
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new ArgumentException(
                "Raise percentage must be greater than zero.");

        MonthlySalary +=
            MonthlySalary * percentage / 100m;
    }
}


public sealed class Librarian : Staff
{
    public Librarian(
        int personId,
        string fullName,
        string phone,
        DateOnly hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            0m)
    {
    }
}


public sealed class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(
        int personId,
        string fullName,
        string phone,
        DateOnly hireDate,
        decimal monthlySalary,
        string section)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            0m)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new ArgumentException(
                "Section is required.");

        Section = section;
    }

    public void Reassign(string newSection)
    {
        if (string.IsNullOrWhiteSpace(newSection))
            throw new ArgumentException(
                "Section is required.");

        Section = newSection;
    }
}


public sealed class HeadLibrarian : Staff
{
    public HeadLibrarian(
        int personId,
        string fullName,
        string phone,
        DateOnly hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            400m)
    {
    }
}


// ==================================================
// MEMBER
// ==================================================

public class Member : Person
{
    private readonly List<Loan> _loans = new();

    public IReadOnlyList<Loan> Loans => _loans;

    public int MaxBorrowLimit { get; }

    public decimal LateFeeDiscountPercentage { get; }

    protected Member(
        int personId,
        string fullName,
        string phone,
        int maxBorrowLimit,
        decimal lateFeeDiscountPercentage)
        : base(personId, fullName, phone)
    {
        if (maxBorrowLimit <= 0)
            throw new ArgumentException(
                "Borrow limit must be greater than zero.");

        if (lateFeeDiscountPercentage < 0 ||
            lateFeeDiscountPercentage > 100)
        {
            throw new ArgumentException(
                "Late fee discount must be between 0 and 100.");
        }

        MaxBorrowLimit = maxBorrowLimit;
        LateFeeDiscountPercentage =
            lateFeeDiscountPercentage;
    }

    public Loan Borrow(
        int loanId,
        LibraryItem item,
        DateOnly borrowedOn)
    {
        if (item.IsWithdrawn)
            throw new InvalidOperationException(
                "Cannot borrow a withdrawn item.");

        if (item.IsOnLoan)
            throw new InvalidOperationException(
                "Cannot borrow an item that is already on loan.");

        int activeLoans = 0;

        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                activeLoans++;
        }

        if (activeLoans >= MaxBorrowLimit)
            throw new InvalidOperationException(
                $"{FullName} reached the borrowing limit.");

        Loan newLoan = new Loan(
            loanId,
            this,
            item,
            borrowedOn);

        _loans.Add(newLoan);

        item.MarkAsOnLoan();

        return newLoan;
    }
}


public sealed class Student : Member
{
    public Student(
        int personId,
        string fullName,
        string phone)
        : base(
            personId,
            fullName,
            phone,
            3,
            0m)
    {
    }
}


public sealed class Premium : Member
{
    public Premium(
        int personId,
        string fullName,
        string phone)
        : base(
            personId,
            fullName,
            phone,
            10,
            20m)
    {
    }

    // Computed property:
    // We do not store ReadingPoints.
    // They are calculated from the loan history.
    public int ReadingPoints
    {
        get
        {
            int returnedLoans = 0;

            foreach (Loan loan in Loans)
            {
                if (loan.Status == LoanStatus.Returned)
                    returnedLoans++;
            }

            return returnedLoans * 5;
        }
    }
}


// ==================================================
// LIBRARY ITEM
// ==================================================

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }

    public int LoanPeriodDays { get; }

    // Can only be changed from inside LibraryItem.
    public decimal BaseLateFee { get; private set; }

    public bool IsOnLoan { get; private set; }
    public bool IsWithdrawn { get; private set; }

    // Each child sends its multiplier through base(...).
    protected decimal LateFeeMultiplier { get; }

    protected LibraryItem(
        string catalogNumber,
        string title,
        int loanPeriodDays,
        decimal baseLateFee,
        decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException(
                "Catalog number is required.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Title is required.");

        if (loanPeriodDays <= 0)
            throw new ArgumentException(
                "Loan period must be greater than zero.");

        if (baseLateFee <= 0)
            throw new ArgumentException(
                "Late fee must be greater than zero.");

        if (lateFeeMultiplier <= 0)
            throw new ArgumentException(
                "Late fee multiplier must be greater than zero.");

        CatalogNumber = catalogNumber;
        Title = title;
        LoanPeriodDays = loanPeriodDays;
        BaseLateFee = baseLateFee;
        LateFeeMultiplier = lateFeeMultiplier;
    }

    // One calculation works for Book, DVD and Magazine.
    // No if/switch based on item type.
    public decimal CalculateDailyLateFee()
    {
        return BaseLateFee * LateFeeMultiplier;
    }

    // Dedicated pricing method.
    public void ChangeBaseLateFee(decimal newFee)
    {
        if (newFee <= 0)
            throw new ArgumentException(
                "Late fee must be greater than zero.");

        BaseLateFee = newFee;
    }

    public void Withdraw()
    {
        if (IsOnLoan)
            throw new InvalidOperationException(
                "Cannot withdraw an item that is currently on loan.");

        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }

    internal void MarkAsOnLoan()
    {
        IsOnLoan = true;
    }

    internal void MarkAsReturned()
    {
        IsOnLoan = false;
    }
}


public sealed class Book : LibraryItem
{
    public Book(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            21,
            baseLateFee,
            1m)
    {
    }
}


public sealed class DVD : LibraryItem
{
    public DVD(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            7,
            baseLateFee,
            2m)
    {
    }
}


public sealed class Magazine : LibraryItem
{
    public Magazine(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            3,
            baseLateFee,
            0.5m)
    {
    }
}


// ==================================================
// LOAN
// ==================================================

public enum LoanStatus
{
    Borrowed,
    Returned,
    Lost
}


public sealed class Loan
{
    public int LoanId { get; }

    public Member Borrower { get; }

    public LibraryItem Item { get; }

    public DateOnly BorrowDate { get; }

    public DateOnly DueDate { get; }

    public DateOnly? ReturnDate { get; private set; }

    public LoanStatus Status { get; private set; }

    public Loan(
        int loanId,
        Member borrower,
        LibraryItem item,
        DateOnly borrowDate)
    {
        if (loanId <= 0)
            throw new ArgumentException(
                "Loan ID must be greater than zero.");

        LoanId = loanId;
        Borrower = borrower;
        Item = item;
        BorrowDate = borrowDate;

        DueDate =
            borrowDate.AddDays(item.LoanPeriodDays);

        Status = LoanStatus.Borrowed;
    }

    public decimal Return(DateOnly returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Loan is no longer active.");

        if (returnDate < BorrowDate)
            throw new InvalidOperationException(
                "Return date cannot be before borrow date.");

        ReturnDate = returnDate;

        int lateDays =
            returnDate.DayNumber - DueDate.DayNumber;

        if (lateDays < 0)
            lateDays = 0;

        // Ask the item for its daily late fee.
        decimal fee =
            lateDays * Item.CalculateDailyLateFee();

        decimal discount =
            fee *
            Borrower.LateFeeDiscountPercentage /
            100m;

        fee -= discount;

        Status = LoanStatus.Returned;

        Item.MarkAsReturned();

        return fee;
    }

    public void MarkLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Loan is no longer active.");

        Status = LoanStatus.Lost;

        Item.MarkAsReturned();
    }
}