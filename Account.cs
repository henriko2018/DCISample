namespace DCISample;

// Methodless role types

public interface ITransferMoneySink
{
}

// Methodful roles

public interface ITransferMoneySource
{
}

public static class TransferMondeySourceTraits
{
    public static void TransferFrom(this ITransferMoneySource self, ITransferMoneySink recipient, decimal amount)
    {
        // This methodful role can only be mixed into Account (and subtypes).
        Account _self = (Account)self;
        Account _recipient = (Account)recipient;

        // Self-contained readable and testable algorithm.
        _self.DecreaseBalance(amount);
        _self.Log("Withdrawing " + amount);
        _recipient.IncreaseBalance(amount);
        _recipient.Log("Depositing " + amount);
    }
}

// Context object
public class TransferMoneyContext
{
    // Properties for accessing the concrete objects relevant in this context through their methodless roles.
    public ITransferMoneySource Source { get; private set; }
    public ITransferMoneySink Sink { get; private set; }
    public decimal Amount { get; private set; }

    public TransferMoneyContext()
    {
        // Logic for retrieving source and sink accounts.
    }

    public TransferMoneyContext(ITransferMoneySource source, ITransferMoneySink sink, decimal amount)
    {
        Source = source;
        Sink = sink;
        Amount = amount;
    }

    public void Doit()
    {
        Source.TransferFrom(Sink, Amount);
        // Alternatively, the context could be passed to the source and sink objects.
    }
}

// *** Model ***

// Abstract domain object
public abstract class Account
{
    public abstract void DecreaseBalance(decimal amount);
    public abstract void IncreaseBalance(decimal amount);
    public abstract void Log(string message);
}

// Concrete domain object
public class SavingsAccount : Account, ITransferMoneySource, ITransferMoneySink
{
    private decimal _balance;

    public SavingsAccount()
    {
        _balance = 10000;
    }

    public override void DecreaseBalance(decimal amount)
    {
        _balance -= amount;
    }

    public override void IncreaseBalance(decimal amount)
    {
        _balance += amount;
    }

    public override void Log(string message)
    {
        Console.WriteLine(message);
    }

    public override string ToString()
    {
        return $"SavingsAccount balance: {_balance}";
    }
}