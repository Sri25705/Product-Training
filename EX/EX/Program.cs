using System;
using System.Linq.Expressions;
using System.Transactions;
  class excep
    {
        static void Main(string[] args)
        {
        try
        {
            Console.WriteLine("Enter the divisible number:");
            int n1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the division number:");
            int n2 = Convert.ToInt32(Console.ReadLine());

            int result = Dividenum(n1, n2);
            Console.WriteLine($"Result: {n1}/{n2}={result}");
        }
        catch(FormatException)
        {
            Console.WriteLine("Enter only numbers");
        }
        catch(DivideByZeroException)
        {
            Console.WriteLine("Division by zero is not applicable");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"error{ex.Message}");
        }
        finally{
        Console.WriteLine("Completed");
    }
}
static int Dividenum(int a,int b)
{
    return a / b;
}
}