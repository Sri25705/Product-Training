using System;

public delegate int Calculation(int a, int b);

public class Delegateex
    {
        public static int Add(int a, int b) => a + b;
        public static int Subtract(int a, int b) => a - b;
        public static int Multiply(int a, int b) => a * b;
        public static int Divide(int a,int b)
        {
            if (b == 0)
                throw new DivideByZeroException("cannot divide by zero");
            return a / b;
        }
       public static void Main(string[]args)
    {
        Calculation operation;

        operation = Add;
        Console.WriteLine($"Addition:{operation(10, 5)}");

        operation = Subtract;
        Console.WriteLine($" Subtract:{operation(10, 5)}");

        operation = Multiply;
        Console.WriteLine($"Multiply:{operation(10, 5)}");

        operation = Divide;
        Console.WriteLine($"Divide:{operation(10, 5)}");
    }

}