using PrimeCalc.Math;
using Newtonsoft.Json;
using PrimeCalc.Client.Resources;
using System.Globalization;

const int MAX = 50;

Messages.Culture = CultureInfo.GetCultureInfo("de-AT"); // laedt Messages aus Messages.de-AT.resx

Console.WriteLine(Messages.WelcomeMessage);

int n = 0;
for (int i = 2; i <= MAX; i++)
{
    if (PrimeChecker.IsPrime(i))
    {
        Console.WriteLine(i);
        n++;
    }
}

Console.WriteLine($"{Messages.NumberOfPrimesInRange} [2, {MAX}] = {n}");

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

    Console.WriteLine(json);
}