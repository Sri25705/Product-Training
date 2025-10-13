using System;
using System.Runtime.CompilerServices;
using System.Timers;

public class Clock
{
    public event EventHandler OnTick;
    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            OnTick?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);
        }
    }
}
 
public class Display
{
    public void Sub(Clock clock)
    {
        clock.OnTick += ShowTime;
    }
    private void ShowTime(object Sender,EventArgs e)
        {
            Console.WriteLine($"Current Time:{DateTime.Now:HH:mm:ss}");
        }
}
public class Program
{
    public static void Main()
    {
        Clock clock = new Clock();
        Display display = new Display();
        display.Sub(clock);

        Console.WriteLine("Clock started");
        clock.Start();
        Console.WriteLine("\n Clocked stopped");        
    }
}