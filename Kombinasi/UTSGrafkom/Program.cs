using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace UTSGrafkom
{
    class Program
    {
        static void Main(string[] args)
        {
            var ourWindow = new NativeWindowSettings()
            {
                Size = new Vector2i(1000, 1000),
            };
            using (var win = new Window(GameWindowSettings.Default, ourWindow))
            {
                win.Run();
            }
        }
    }
}
