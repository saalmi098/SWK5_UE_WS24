using static System.Math;

namespace PrimeCalc.Math;

public class PrimeChecker
{
    public static bool IsPrime(int number)
    {
        for (int i = 2; i <= Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}