using System;
using System.Collections;
using System.Collections.Generic;

namespace LuckyNumber
{
    class Program
    {


        static void Main(string[] args)
        {

            long res;

            Console.WriteLine("A number is called lucky if the sum of its digits, as well as the sum\n" +
                              "of the squares of its digits is a prime number. How many numbers between\n" +
                              "A and B(both inclusive) are lucky?\n");
            Console.WriteLine("Constraints:  Numbers must fall between 1 <= A <= B <= 10^18\n");
            Console.Write("What is the number for A? ");
            long a = long.Parse(Console.ReadLine());
            Console.Write("What is the number for B? ");
            long b = long.Parse(Console.ReadLine());

            res = luckyNumbers(a, b);

            Console.WriteLine("There are {0} lucky numbers between A and B.", res);
            Console.ReadLine();

        }


        private static long luckyNumbers(long a, long b)
        {
            int addNumber = 0, addSquareNumber = 0, maxSumOfSquares = 0;
            long count = 0;
            long tempNumber = (long)Math.Pow(10,18) - 1;

            while (tempNumber != 0)
            {
                maxSumOfSquares += (int)(tempNumber * tempNumber) % 10;
                tempNumber /= 10;
            }

            var primes = GeneratePrimesSieveOfEratosthenes(maxSumOfSquares);

            if (a >= 1 && a <= b && b <= (long)(Math.Pow(10, 18)))
            {
                if (a <= 10)
                    a = 11;
                for (long i = a; i <= b; i++)
                {
                    tempNumber = i;
                    while (tempNumber != 0)
                    {
                        addNumber += (int)(tempNumber % 10);
                        addSquareNumber += (int)(tempNumber * tempNumber) % 10;
                        tempNumber /= 10;
                    }

                    if (primes.ContainsKey(addNumber) && primes.ContainsKey(addSquareNumber))
                    //if (IsPrime(addNumber) && IsPrime(addSquareNumber))
                    {
                        count++;
                        //Console.WriteLine("{0} is a lucky number", i);
                    }

                    addNumber = 0;
                    addSquareNumber = 0;
                }
            }

            return count;
        }

        public static bool IsPrime(long number)
        {
            //Prime number:  An integer greater than one is called a prime number if its only positive divisors (factors) are one and itself. 
            bool isPrime = true;

            if (number == 1)
            {
                return false;
            }
            else if (number == 2 || number == 3)
            {
                return true;
            }
            else if (number % 2 != 0)
            {
                for (long i = 3; i < number; i += 2)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (!isPrime)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }


        public static int ApproximateNthPrime(int nn)
        {
            double n = (double)nn;
            double p;
            if (nn >= 7022)
            {
                p = n * Math.Log(n) + n * (Math.Log(Math.Log(n)) - 0.9385);
            }
            else if (nn >= 6)
            {
                p = n * Math.Log(n) + n * Math.Log(Math.Log(n));
            }
            else if (nn > 0)
            {
                p = new int[] { 2, 3, 5, 7, 11 }[nn - 1];
            }
            else
            {
                p = 0;
            }
            return (int)p;
        }

        // Find all primes up to and including the limit
        public static BitArray SieveOfEratosthenes(int limit)
        {
            BitArray bits = new BitArray(limit + 1, true);
            bits[0] = false;
            bits[1] = false;
            for (int i = 0; i * i <= limit; i++)
            {
                if (bits[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        bits[j] = false;
                    }
                }
            }
            return bits;
        }

        public static Dictionary<int,bool> GeneratePrimesSieveOfEratosthenes(int n)
        {
            int limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfEratosthenes(limit);
            Dictionary<int, bool> primes = new Dictionary<int, bool>();
            //    List<int> prime = new List<int>();
            for (int i = 0, found = 0; i < limit && found < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(i,true);
                    found++;
                }
            }
            return primes;
        }
    }
}
