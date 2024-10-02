using PrimeCalc.Math;
using Newtonsoft.Json;

const int MAX = 50;

int n = 0;
for (int i = 2; i <= MAX; i++)
{
    if (PrimeChecker.IsPrime(i))
    {
        Console.WriteLine(i);
        n++;
    }
}

Console.WriteLine($"Number of prime numbers in [2, {MAX}] = {n}");

PrintPrimesAsJson();

static void PrintPrimesAsJson()
{
    var primes = new List<int>();
    for (int i = 2; i <= MAX; i++)
    {
        if (PrimeChecker.IsPrime(i))
        {
            primes.Add(i);
        }
    }

    string json = JsonConvert.SerializeObject(
        new
        {
            Count = primes.Count,
            Primes = primes
        }
    );

    System.Console.WriteLine(json);
}