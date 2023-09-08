using static System.Console;
using System.Diagnostics;

Stopwatch watch = new();
Write("Press ENTER to start.");
ReadLine();
watch.Start();

int max = 45;
IEnumerable<int> numbers = Enumerable.Range(1, max);
WriteLine($"Calculating Fibonacci sequence up to {max}. Plaese wait...");
int[] fibonacciNumbers = numbers.AsParallel().Select(number => Fibonacci(number)).OrderBy(number => number).ToArray();
watch.Stop();
Write("{0:#,##0} elapsed milliseconds.", watch.ElapsedMilliseconds);
Write("Result:");
foreach(int number in fibonacciNumbers)
{
    Write($"    {number}");
}







static int Fibonacci(int term) => term switch
{
    1 => 0,
    2 => 1,
    _ => Fibonacci(term - 1) + Fibonacci(term - 2)
};