Random random = new Random();
int target = random.Next(1, 101);
int guess = 0;
int attempts = 0;

Console.WriteLine("try to guess the number from 1 to 100");
while (guess != target)
{
Console.WriteLine("enter the guess number");
guess = Convert.ToInt32(Console.ReadLine());
attempts++;
if (guess > target)
{
Console.WriteLine("Too high number");
}
else if (guess > target) {
        Console.WriteLine("Too Low Number");
    }
    else
    {
        Console.WriteLine($"Correct Number{target}");
        Console.WriteLine($"The no of attempts{attempts}");
    }
}

