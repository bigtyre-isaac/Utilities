var random = new Random();

while (true)
{
    int next = random.Next(0, 9);   
    Console.WriteLine(next);
    await Task.Delay(500);
}