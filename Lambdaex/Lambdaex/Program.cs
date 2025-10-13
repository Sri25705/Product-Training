using System;

public class Lambdaex
{
    public static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var evenno = numbers.Where(n => n % 2 == 0).ToList();
        Console.WriteLine("Even number:");
        evenno.ForEach(n => Console.WriteLine(n));
    }
}