using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Filter
{
    class Program
    {
        private static CancellationToken CancelToken;
        private static ConcurrentQueue<string> InputLines { get; } = new();

        static void Main(string[] args)
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            CancelToken = cancellationTokenSource.Token;


            string filter = null;
            if (args.Length > 0) { 
                filter = string.Join(' ', args);
            }
            else
            {
                Console.Write("Enter filter: ");
                filter = Console.ReadLine();
            }

            _ = Task.Run(() => ReadInput());

            while (!CancelToken.IsCancellationRequested)
            {
                while (InputLines.TryDequeue(out var line))
                {
                    if (Regex.IsMatch(line, filter))
                    {
                        Console.WriteLine(line);
                    }
                }
            }

            Console.WriteLine("Exited");
        }

        private static void ReadInput()
        {
            while (!CancelToken.IsCancellationRequested) {
                var line = Console.ReadLine();
                if (line == null) continue;

                InputLines.Enqueue(line);
            }
        }
    }
}
