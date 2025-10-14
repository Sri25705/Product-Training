using System;
using System.Reflection;

class Person 
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Product
{
    public string Productname { get; set; }
    public double Price { get; set; }
}

class Objectserializer
{
    public static void SerializeObject(object obj)
    {
        if(obj==null)
        {
            Console.WriteLine("Null");
            return;
        }
        Type type = obj.GetType();
        Console.WriteLine($"Type{type.Name}");

        PropertyInfo[] properties = type.GetProperties();

        foreach(PropertyInfo prop in properties)
        {
            object value = prop.GetValue(obj, null);
            Console.WriteLine($"{prop.Name}={value}");
        }
        Console.WriteLine();
    }
}
class Program
{
    static void Main()
    {
        Person person = new Person { Name="sri",Age=22};
        Product product = new Product { Productname="Laptop",Price=75000};

        Console.WriteLine("Reflection");

        Objectserializer.SerializeObject(person);
        Objectserializer.SerializeObject(product);

        Console.WriteLine("completed");

    }
}
