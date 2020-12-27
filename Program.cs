using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

using OpenTK.Graphics.OpenGL;

namespace rotation
{
    class Program {

        static void Main(string[] args) {
            NativeWindowSettings nws = new NativeWindowSettings() {
                Title = "Hello",
                Size = new Vector2i(1000,1000),
                StartFocused = true,
                StartVisible = true,
                APIVersion = new Version(3,2),
                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,
            };
            GameWindowSettings gws = new GameWindowSettings();

            new GameScene(gws, nws);
        }
    }

    class GameScene : GameWindow {

        Polygon[] polygon = new Polygon[2];

        public GameScene(GameWindowSettings GWS, NativeWindowSettings NWS) : base(GWS,NWS) {
            Run();
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            polygon[0].Render();
            polygon[1].Render();
            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e) {
            base.OnResize(e);
        }

        protected override void OnLoad() {
            base.OnLoad();
            float angle = 10;
            Console.WriteLine(Math.Tan(angle));
            polygon[0] = new Polygon((float)Math.Tan(angle), new Vector2(.5f, 0f), new Vector2(new Random().Next(-20, 20)/10,new Random().Next(-10, 10)/10), 10);
            polygon[1] = new Polygon((float)Math.Tan(angle), new Vector2(.2f, 0f), new Vector2(new Random().Next(-20, 20)/10,new Random().Next(-10, 10)/10), 1);
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0,0,0,1.0f);
        }
    }
}
