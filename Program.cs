// Test controller:

using DCISample;

SavingsAccount source = new();
SavingsAccount sink = new();

Console.WriteLine("Before:");
Console.WriteLine("Source: " + source);
Console.WriteLine("Sink: " + sink);

Console.WriteLine("Run transfer:");
new TransferMoneyContext(source, sink, 1000).Doit();

Console.WriteLine("After:");
Console.WriteLine("Source: " + source);
Console.WriteLine("Sink: " + sink);
