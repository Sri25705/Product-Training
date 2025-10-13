using System;
using System.Collections.Generic;

public class stack<T>
{
    public List<T> elements = new List<T>();

    public void Push(T item)
    {
        elements.Add(item);
        Console.WriteLine(item);
    }

    public T Pop()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("Stack is empty");

        T top = elements[elements.Count -1];
        elements.RemoveAt(elements.Count - 1);
        return top;
    }

    public T Peek()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("Stack is empty");

        return elements[elements.Count - 1];
    }

}

class Program
{
    static void Main()
    {
        Stack<int> intStack = new Stack<int>();
        intStack.Push(1);
        intStack.Push(2);
        intStack.Push(3);
        Console.WriteLine($"Top element:{intStack.Peek()}");
        Console.WriteLine($"Popped element:{intStack.Pop()}");
        Console.WriteLine($"Top after pop:{intStack.Peek()}");

        Stack<string> stringStack = new Stack<string>();
        stringStack.Push("Sri");
        stringStack.Push("Swetha");
        stringStack.Push("Siva");
        Console.WriteLine($"Top element:{stringStack.Peek()}");
        Console.WriteLine($"Popped element:{stringStack.Pop()}");
        Console.WriteLine($"Top after pop:{stringStack.Peek()}");
    }


}