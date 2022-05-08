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
                Size = new Vector2i(800, 800),
            };
            using (var win = new C14190024(GameWindowSettings.Default, ourWindow))
            {
                win.Run();
            }
        }
    }
}
