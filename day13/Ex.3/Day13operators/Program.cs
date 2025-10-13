using System;
namespace op
{
    class Operate
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter name:"); //Getting input from user for name
            string Myname = Console.ReadLine();
            Console.WriteLine("Enter age:");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Weight");
            double weight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Details:");
            Console.WriteLine("Adding age and weight:" + (age + weight)); //Performing arithmetic operator 
            bool eligiblity = true;
            Console.WriteLine("I am " + Myname + " My age is " + age + " I carry a weight of " + weight + " Am i eligible to vote " + eligiblity);
            if (age <= 18)
            {
                Console.WriteLine(+age+ " Not Eligibile to vote");
            }
            else
            { 
                Console.WriteLine(+age+ " Eligibile to vote");
            }
            if (weight <= 30 && weight >= 80)
            { 
                Console.WriteLine("Not a perfect weight");
            }
            else
            {
                Console.WriteLine("Perfect Weight");  /* In conditional statement i have used Comparison 
                                                       and Logical Operator*/
            }
        }
    }
}

