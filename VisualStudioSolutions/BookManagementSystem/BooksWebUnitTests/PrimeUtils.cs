using System;
using System.Collections.Generic;
using System.Text;

namespace BooksWebUnitTests
{
    class PrimeUtils
    {
        public static bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i < number; i++)
                if (number % i == 0)
                    return false;

            return true;

        }
    }
}
