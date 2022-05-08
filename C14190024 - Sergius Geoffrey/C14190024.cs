using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace UTSGrafkom
{
    class C14190024 : GameWindow
    {
        string shader_vert = "../../../Shaders/shader.vert";
        string shader_frag = "../../../Shaders/shader.frag";
        List<Mesh> Board = new List<Mesh>();
        List<Mesh> BlackBishop = new List<Mesh>();

        List<Vector3d> positions = new List<Vector3d>();
        List<Curve> Indikator = new List<Curve>();

        Camera _camera;
        bool _firstMove = true;
        Vector2 _lastPos;
        Mesh square;
        public C14190024(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            //Camera
            //View = Matrix4.LookAt(new Vector3(0.0f, 2.0f, 3.0f),
            //                      new Vector3(0.0f, 0.0f, 0.0f),
            //                      new Vector3(0.0f, 1.0f, 0.0f));
            //Projection = Matrix4.CreatePerspectiveFieldOfView((MathHelper.DegreesToRadians(45)), (float)Size.X / (float)Size.Y, 0.1f, 100.0f);
            //View = Matrix4.Identity;
            //Projection = Matrix4.Identity;

            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
            CursorGrabbed = true;

            GL.ClearColor(0.4f, 0.6f, 0.3f, 1.0f);

            #region Board
            //BOARD
            Rectangle board = new Rectangle();
            board.createvertices(-0.45f, -0.225f, 0.5f, 5.6f, 0.08f, 5.6f);
            board.setupObject(shader_vert, shader_frag);
            board.setColor(0.443f, 0.262f, 0.054f);
            Board.Add(board);
            float pZ;
            float pX = 0.0f;
            for (int i = 0; i < 4; i++)
            {
                pZ = 0.0f;
                for (int j = 0; j < 4; j++)
                {
                    square = new Rectangle();
                    square.createvertices(2.0f + pX, -0.2f, -1.95f + pZ, 0.69f, 0.06f, 0.69f);
                    square.setupObject(shader_vert, shader_frag);
                    square.setColor(0.317f, 0.145f, 0.062f);
                    Board.Add(square);
                    pZ += 1.4f;
                    square = new Rectangle();
                    square.createvertices(2.0f + pX, -0.2f, -2.65f + pZ, 0.69f, 0.06f, 0.69f);
                    square.setupObject(shader_vert, shader_frag);
                    square.setColor(0.827f, 0.564f, 0.192f);
                    Board.Add(square);
                }
                pX -= 1.4f;
            }
            pX = 0;
            for (int j = 0; j < 4; j++)
                {
                    pZ = 0;
                    for (float i = 0.0f; i < 4.0f; i++)
                    {
                        square = new Rectangle();
                        square.createvertices(1.3f + pX, -0.2f, -1.95f + pZ, 0.69f, 0.06f, 0.69f);
                        square.setupObject(shader_vert, shader_frag);
                        square.setColor(0.827f, 0.564f, 0.192f);
                        Board.Add(square);
                        pZ += 1.4f;
                        square = new Rectangle();
                        square.createvertices(1.3f + pX, -0.2f, -2.65f + pZ, 0.69f, 0.06f, 0.69f);
                        square.setupObject(shader_vert, shader_frag);
                        square.setColor(0.317f, 0.145f, 0.062f);
                        Board.Add(square);
                    }
                    pX -= 1.4f;
                }
            #endregion

            #region Black Bishop
            //Head
                //top
                Sphere topBB = new Sphere();
                topBB.createvertices(0.0f, 0.63f, 0.0f, 0.03f, 0.03f, 0.03f);
                topBB.setupObject(shader_vert, shader_frag);
                topBB.setColor(0.3f, 0.3f, 0.3f);
                BlackBishop.Add(topBB);
                //head1
                Cone head1BB = new Cone();
                head1BB.createvertices(0.0f, 0.0f, 0.65f, 0.05f, 0.05f, 0.1f);
                head1BB.setupObject(shader_vert, shader_frag);
                head1BB.rotate(270.0f, 0, 0);
                head1BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(head1BB);
                //headMid
                Sphere head2BB = new Sphere();
                head2BB.createvertices(0.0f, 0.47f, 0.0f, 0.09f, 0.09f, 0.09f);
                head2BB.setupObject(shader_vert, shader_frag);
                head2BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(head2BB);
                //head3
                Cone head3BB = new Cone();
                head3BB.createvertices(0.0f, 0.0f, -0.34f, 0.05f, 0.05f, 0.1f);
                head3BB.setupObject(shader_vert, shader_frag);
                head3BB.rotate(90.0f, 0, 0);
                head3BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(head3BB);

            //NECK
                //Neck Body
                Cone Neck1BB = new Cone();
                Neck1BB.createvertices(0.0f, 0.0f, 0.47f, 0.04f, 0.04f, 0.1f);
                Neck1BB.setupObject(shader_vert, shader_frag);
                Neck1BB.rotate(270.0f, 0, 0);
                Neck1BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(Neck1BB);
                //Upper Neck ring
                Cylinder Neck2BB = new Cylinder();
                Neck2BB.createvertices(0.0f, 0.0f, 0.37f, 0.045f, 0.045f, 0.02f);
                Neck2BB.setupObject(shader_vert, shader_frag);
                Neck2BB.rotate(270.0f, 0, 0);
                Neck2BB.setColor(0.3f, 0.3f, 0.3f);
                BlackBishop.Add(Neck2BB);
                //Lower Neck ring
                Cylinder Neck3BB = new Cylinder();
                Neck3BB.createvertices(0.0f, 0.0f, 0.35f, 0.05f, 0.05f, 0.02f);
                Neck3BB.setupObject(shader_vert, shader_frag);
                Neck3BB.rotate(270.0f, 0, 0);
                Neck3BB.setColor(0.3f, 0.3f, 0.3f);
                BlackBishop.Add(Neck3BB);
                //Cover for bottleNeck
                Sphere CoverBB = new Sphere();
                CoverBB.createvertices(0.0f, 0.0f, 0.339f, 0.1f, 0.1f, 0.00f);
                CoverBB.setupObject(shader_vert, shader_frag);
                CoverBB.rotate(270.0f, 0, 0);
                CoverBB.setColor(0.3f, 0.3f, 0.3f);
                BlackBishop.Add(CoverBB);
                //BottleNeckThing
                ModifiedTorus Neck4BB = new ModifiedTorus();
                Neck4BB.createvertices(0.0f, 0.0f, 0.32f, 0.08f, 0.02f);
                Neck4BB.setupObject(shader_vert, shader_frag);
                Neck4BB.rotate(270.0f, 0, 0.0f);
                Neck4BB.setColor(0.3f, 0.3f, 0.3f);
                BlackBishop.Add(Neck4BB);

            //BODY
                //MainBody
                Cylinder bodyBB = new Cylinder();
                bodyBB.createvertices(0.0f, 0.0f, 0.15f, 0.06f, 0.06f, 0.9f);
                bodyBB.setupObject(shader_vert, shader_frag);
                bodyBB.rotate(270.0f, 0, 0);
                bodyBB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(bodyBB);

            //BOTTOM
                //Bottom most upper part
                Cone bottom1BB = new Cone();
                bottom1BB.createvertices(0.0f, 0.0f, 0.23f, 0.07f, 0.07f, 0.09f);
                bottom1BB.setupObject(shader_vert, shader_frag);
                bottom1BB.rotate(270.0f, 0, 0);
                bottom1BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(bottom1BB);
                //bottom2 bottom
                Sphere bottom2BB = new Sphere();
                bottom2BB.createvertices(0.0f, 0.13f, 0.0f, 0.08f, 0.08f, 0.08f);
                bottom2BB.setupObject(shader_vert, shader_frag);
                bottom2BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(bottom2BB);
                //Bottom Upper part
                Cylinder bottom3BB = new Cylinder();
                bottom3BB.createvertices(0.0f, 0.0f, 0.057f, 0.11f, 0.11f, 0.45f);
                bottom3BB.setupObject(shader_vert, shader_frag);
                bottom3BB.rotate(270.0f, 0, 0);
                bottom3BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(bottom3BB);
                //Bottom middle part
                Cone bottom4BB = new Cone();
                bottom4BB.createvertices(0.0f, 0.0f, 0.15f, 0.125f, 0.125f, 0.1f);
                bottom4BB.setupObject(shader_vert, shader_frag);
                bottom4BB.rotate(270.0f, 0, 0);
                bottom4BB.setColor(0.2f, 0.2f, 0.2f);
                BlackBishop.Add(bottom4BB);
                //Most bottom part
                Cylinder bottom5BB = new Cylinder();
                bottom5BB.createvertices(0.0f, 0.0f, 0.0f, 0.19f, 0.19f, 0.4f);
                bottom5BB.setupObject(shader_vert, shader_frag);
                bottom5BB.rotate(90.0f, 0, 0);
                bottom5BB.setColor(0.3f, 0.3f, 0.3f);
                BlackBishop.Add(bottom5BB);

            for (int i = 0; i < BlackBishop.Count; i++)
            {
                BlackBishop[i].scale(1.2f);
                BlackBishop[i].translate(0.6f, -0.08f, -1.95f);
            }
            #endregion

            #region indicators
            positions.Add(new Vector3d(0.35, -0.2, 0.0));
            positions.Add(new Vector3d(0.2, 0.05, 0.0));
            positions.Add(new Vector3d(0.0, 0.4, 0.0));
            positions.Add(new Vector3d(-0.2, 0.05, 0.0));
            positions.Add(new Vector3d(-0.35, -0.2, 0.0));

            float recolor=0;
            float move = 0;
            for (int i = 0; i < 8; i++)
            {
               
                Curve Kurva = new Curve();
                Kurva.createvertices(positions);
                Kurva.setupObject(shader_vert, shader_frag);
                Kurva.rotate(0.0f, 0.0f, 0.0f);
                if (i < 5)
                    Kurva.setColor(1.0f - recolor, 0.0f + recolor, 0.0f);
                else
                {
                    if (i < 6)
                        Kurva.setColor(0.0f, 1.0f - recolor, 0.0f + recolor);
                    else
                        Kurva.setColor(0.5f , 0.0f, 0.5f+recolor);
                }
                Kurva.translate(-2.9f + move, 0.0f, -2.3f);
                move += 0.7f;
                recolor += 0.25f;
                Indikator.Add(Kurva);
            }
            #endregion
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            for (int i = 0; i < Board.Count; i++)
            {
                Board[i].render(_camera);
                //Board[i].rotate(0.0f, 0.07f, 0.0f);
            }
            for (int i = 0; i < BlackBishop.Count; i++)
            {
                BlackBishop[i].render(_camera);
                //BlackBishop[i].rotate(0.0f, 0.07f, 0.0f);
            }
            for(int i=0;i< Indikator.Count;i++)
            {
                Indikator[i].render(_camera);
            }
            SwapBuffers();
            base.OnRenderFrame(args);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (!IsFocused)
            {
                return;
            }
            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;
            var input = KeyboardState;
            
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.Up))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time; //Forward 
            }

            if (input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.Down))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time; //Backwards
            }

            if (input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.Left))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time; // Left
            }

            if (input.IsKeyDown(Keys.D) || input.IsKeyDown(Keys.Right))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time; // Right
            }

            if (input.IsKeyDown(Keys.Up) || input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time; // Up
            }

            if (input.IsKeyDown(Keys.Down)||input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time; // Down
            }

            var mouse = MouseState;
            if (_firstMove) // this bool variable is initially set to true
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // reversed since y-coordinates range from bottom to top
            }
            base.OnUpdateFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {

            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
            base.OnResize(e);
        }
    }
}