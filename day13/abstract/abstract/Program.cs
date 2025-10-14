using System;
using System.Collections.Generic;
    abstract class Shape    
{
    public abstract double area();
    public abstract double perimeter();

    public virtual void Displaymessage()
    {
        Console.WriteLine("This is a geometric shapes");
    }
}

class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }
    public Rectangle(double length, double width)
    {
        Length = length;
        Width = width;
    }
    public override double area()
    {
        return Length * Width;
    }

    public override double perimeter()
    {
        return 2 * (Length + Width);
    }

    public override void Displaymessage()
    {
        Console.WriteLine("This is Rectangle");
    }
}
class Triangle : Shape
{
    public double Base_length { get; set; }
    public double Height { get; set; }
    public double sideA { get; set; }
    public double sideB { get; set; }

    public Triangle(double base_length,double height,double a, double b)
    {
        Base_length = base_length;
        Height = height;
        sideA = a;
        sideB = b;
    }

    public override double area()
    {
        return 0.5 * Base_length * Height;
    }

    public override double perimeter()
    {
        return sideA + sideB + Base_length;
    }

    public override void Displaymessage()
    {
        Console.WriteLine("This is Triangle");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }
    public Circle(double radius)
    {
        Radius = radius;
    }
    public override double area()
    {
        return Math.PI * Radius * Radius;
    }

    public override double perimeter()
    {
        return 2 * Math.PI * Radius;
    }
    public override void Displaymessage()
    {
        Console.WriteLine("This is Circle");
    }
}


class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>
       {
           new Rectangle(5,10),
           new Triangle(3,4,5,5),
           new Circle(7)
       };

        double totalArea = 0;
        double totalPerimeter = 0;

        foreach(var shape in shapes)
        {
            shape.Displaymessage();
            Console.WriteLine($"Area:{shape.area():F2}");
            Console.WriteLine($"Perimeter:{shape.perimeter():F2}");

            totalArea += shape.area();
            totalPerimeter += shape.perimeter();
        }
        Console.WriteLine($"Total area of all shapes:{totalArea:F2}");
        Console.WriteLine($"Total perimeter of all shapes:{totalPerimeter:F2}");

    }
}
