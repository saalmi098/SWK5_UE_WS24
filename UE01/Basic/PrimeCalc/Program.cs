using PrimeCalc.Math;

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