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
    class C14190014 : GameWindow
    {
        string shader_vert = "../../../Shaders/shader.vert";
        string shader_frag = "../../../Shaders/shader.frag";
        List<Mesh> WhitePawn = new List<Mesh>();
        List<Mesh> WhiteKing = new List<Mesh>();
        List<Mesh> BlackKing = new List<Mesh>();
        List<Mesh> BlackPawn = new List<Mesh>();
  

        List<Vector3d> positions = new List<Vector3d>();
        List<Curve> Indikator = new List<Curve>();

        Camera _camera;
        bool _firstMove = true;
        Vector2 _lastPos;

        public C14190014(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
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


            #region White King
            Cylinder PlatformWK = new Cylinder();
            PlatformWK.createvertices(0.0f, 0.0f, 0.2f, 0.148f, 0.148f, 0.148f);
            PlatformWK.setupObject(shader_vert, shader_frag);
            PlatformWK.rotate(90f, 0, 0f);
            PlatformWK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(PlatformWK);

            Cylinder Platform2WK = new Cylinder();
            Platform2WK.createvertices(0.0f, 0.0f, 0.18f, 0.134f, 0.134f, 0.134f);
            Platform2WK.setupObject(shader_vert, shader_frag);
            Platform2WK.rotate(90f, 0, 0f);
            Platform2WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Platform2WK);

            Cylinder Platform3WK = new Cylinder();
            Platform3WK.createvertices(0.0f, 0.0f, 0.12f, 0.09f, 0.09f, 0.09f);
            Platform3WK.setupObject(shader_vert, shader_frag);
            Platform3WK.rotate(90f, 0, 0f);
            Platform3WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Platform3WK);

            Cylinder Platform4WK = new Cylinder();
            Platform4WK.createvertices(0.0f, 0.0f, -0.02f, 0.05f, 0.05f, 0.05f);
            Platform4WK.setupObject(shader_vert, shader_frag);
            Platform4WK.rotate(90f, 0, 0f);
            Platform4WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Platform4WK);

            Cylinder Platform5WK = new Cylinder();
            Platform5WK.createvertices(0.0f, 0.0f, -0.08f, 0.05f, 0.05f, 0.05f);
            Platform5WK.setupObject(shader_vert, shader_frag);
            Platform5WK.rotate(90f, 0, 0f);
            Platform5WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Platform5WK);

            Cylinder Platform6WK = new Cylinder();
            Platform6WK.createvertices(0.0f, 0.0f, -0.18f, 0.1f, 0.18f, 0.1f);
            Platform6WK.setupObject(shader_vert, shader_frag);
            Platform6WK.rotate(90f, 0, 0f);
            Platform6WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Platform6WK);

            Cone Curve1WK = new Cone();
            Curve1WK.createvertices(0.0f, 0.0f, 0.16f, 0.06f, 0.06f, 0.2f);
            Curve1WK.setupObject(shader_vert, shader_frag);
            Curve1WK.rotate(-90f, 0f, 0f);
            Curve1WK.setColor(0.823f, 0.823f, 0.823f);
            WhiteKing.Add(Curve1WK);

            Cone Curve2WK = new Cone();
            Curve2WK.createvertices(0.0f, 0.0f, -0.09f, 0.096f, 0.096f, 0.07f);
            Curve2WK.setupObject(shader_vert, shader_frag);
            Curve2WK.rotate(-90f, 0, 0f);
            Curve2WK.setColor(0.823f, 0.823f, 0.823f);
            WhiteKing.Add(Curve2WK);

            Sphere CoverWK = new Sphere();
            CoverWK.createvertices(0.0f, 0.0f, -0.117f, 0.06f, 0.06f, 0.0f);
            CoverWK.setupObject(shader_vert, shader_frag);
            CoverWK.rotate(90f, 0, 0f);
            CoverWK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(CoverWK);

            Cone Curve3WK = new Cone();
            Curve3WK.createvertices(0.0f, 0.0f, 0.18f, 0.036f, 0.036f, 0.04f);
            Curve3WK.setupObject(shader_vert, shader_frag);
            Curve3WK.rotate(-90f, 0, 0f);
            Curve3WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Curve3WK);

            Cone Curve4WK = new Cone();
            Curve4WK.createvertices(0.0f, 0.0f, -0.08f, 0.07f, 0.07f, 0.12f);
            Curve4WK.setupObject(shader_vert, shader_frag);
            Curve4WK.rotate(90f, 0, 0f);
            Curve4WK.setColor(0.823f, 0.823f, 0.823f);
            WhiteKing.Add(Curve4WK);

            Cone Cross1WK = new Cone();
            Cross1WK.createvertices(0.0f, 0.0f, 0.38f, 0.036f, 0.036f, 0.078f);
            Cross1WK.setupObject(shader_vert, shader_frag);
            Cross1WK.rotate(-90f, 0, 0f);
            Cross1WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Cross1WK);

            Rectangle Cross2WK = new Rectangle();
            Cross2WK.createvertices(0.0f, 0.34f, 0.0f, 0.12f, 0.05f, 0.087f);
            Cross2WK.setupObject(shader_vert, shader_frag);
            Cross2WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Cross2WK);

            Sphere Cross3WK = new Sphere();
            Cross3WK.createvertices(0.0f, 0.37f, 0.0f, 0.025f, 0.025f, 0.025f);
            Cross3WK.setupObject(shader_vert, shader_frag);
            Cross3WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Cross3WK);

            Cone Cross4WK = new Cone();
            Cross4WK.createvertices(0.0f, 0.0f, 0.41f, 0.020f, 0.020f, 0.03f);
            Cross4WK.setupObject(shader_vert, shader_frag);
            Cross4WK.rotate(-90f, 0, 0f);
            Cross4WK.setColor(0.6f, 0.6f, 0.6f);
            WhiteKing.Add(Cross4WK);

            for (int i = 0; i < WhiteKing.Count; i++)
            {
                WhiteKing[i].scale(1.69f);
                WhiteKing[i].translate(-1.0f, 0.0f, 0.8f);
            }
            #endregion

            #region White Pawn
            Sphere CoverWP = new Sphere();
            CoverWP.createvertices(0.0f, 0.0f, 0.23f, 0.11f, 0.11f, 0.0f);
            CoverWP.setupObject(shader_vert, shader_frag);
            CoverWP.rotate(270.0f, 0, 0);
            CoverWP.setColor(0.6f, 0.6f, 0.6f);
            WhitePawn.Add(CoverWP);

            Cylinder Platform1WP = new Cylinder();
            Platform1WP.createvertices(0.0f, 0.0f, 0.0f, 0.15f, 0.15f, 0.15f);
            Platform1WP.setupObject(shader_vert, shader_frag);
            Platform1WP.rotate(90f, 0, 0);
            Platform1WP.setColor(0.6f, 0.6f, 0.6f);
            WhitePawn.Add(Platform1WP);

            Cylinder Platform2WP = new Cylinder();
            Platform2WP.createvertices(0.0f, 0.0f, -0.098f, 0.098f, 0.98f);
            Platform2WP.setupObject(shader_vert, shader_frag);
            Platform2WP.rotate(90f, 0, 0);
            Platform2WP.setColor(0.6f, 0.6f, 0.6f);
            WhitePawn.Add(Platform2WP);

            Cylinder Platform3WP = new Cylinder();
            Platform3WP.createvertices(0.0f, 0.0f, -0.24f, 0.12f, 0.2f);
            Platform3WP.setupObject(shader_vert, shader_frag);
            Platform3WP.rotate(90f, 0, 0);
            Platform3WP.setColor(0.6f, 0.6f, 0.6f);
            WhitePawn.Add(Platform3WP);

            Cone Curve1WP = new Cone();
            Curve1WP.createvertices(0.0f, 0.0f, 0.19f, 0.06f, 0.06f, 0.06f);
            Curve1WP.setupObject(shader_vert, shader_frag);
            Curve1WP.rotate(-90f, 0, 0);
            Curve1WP.setColor(0.6f, 0.6f, 0.6f);
            WhitePawn.Add(Curve1WP);

            Cone Curve2WP = new Cone();
            Curve2WP.createvertices(0.0f, 0.0f, 0.15f, 0.1f, 0.1f, 0.1f);
            Curve2WP.setupObject(shader_vert, shader_frag);
            Curve2WP.rotate(-90f, 0, 0);
            Curve2WP.setColor(0.823f, 0.823f, 0.823f);
            WhitePawn.Add(Curve2WP);

            Cone Curve3WP = new Cone();
            Curve3WP.createvertices(0.0f, 0.0f, -0.12f, 0.07f, 0.07f, 0.07f);
            Curve3WP.setupObject(shader_vert, shader_frag);
            Curve3WP.rotate(90f, 0, 0);
            Curve3WP.setColor(0.6f, 0.6f, 0.6f);
            WhitePawn.Add(Curve3WP);

            Sphere BallWP = new Sphere();
            BallWP.createvertices(0.0f, 0.1f, 0.0f, 0.08f, 0.08f, 0.08f);
            BallWP.setupObject(shader_vert, shader_frag);
            BallWP.setColor(0.823f, 0.823f, 0.823f);
            WhitePawn.Add(BallWP);

            Sphere Ball2WP = new Sphere();
            Ball2WP.createvertices(0.0f, 0.299f, 0.0f, 0.08f, 0.08f, 0.08f);
            Ball2WP.setupObject(shader_vert, shader_frag);
            Ball2WP.setColor(0.823f, 0.823f, 0.823f);
            WhitePawn.Add(Ball2WP);

            for (int i = 0; i < WhitePawn.Count; i++)
            {
                WhitePawn[i].scale(1.4f);
                WhitePawn[i].translate(-1.5f, 0.0f, 0.0f);
                
            }
            #endregion

            #region Black King
            Cylinder PlatformBK = new Cylinder();
            PlatformBK.createvertices(0.0f, 0.0f, 0.2f, 0.148f, 0.148f, 0.148f);
            PlatformBK.setupObject(shader_vert, shader_frag);
            PlatformBK.rotate(90f, 0, 0f);
            PlatformBK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(PlatformBK);

            Cylinder Platform2BK = new Cylinder();
            Platform2BK.createvertices(0.0f, 0.0f, 0.18f, 0.134f, 0.134f, 0.134f);
            Platform2BK.setupObject(shader_vert, shader_frag);
            Platform2BK.rotate(90f, 0, 0f);
            Platform2BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Platform2BK);

            Cylinder Platform3BK = new Cylinder();
            Platform3BK.createvertices(0.0f, 0.0f, 0.12f, 0.09f, 0.09f, 0.09f);
            Platform3BK.setupObject(shader_vert, shader_frag);
            Platform3BK.rotate(90f, 0, 0f);
            Platform3BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Platform3BK);

            Cylinder Platform4BK = new Cylinder();
            Platform4BK.createvertices(0.0f, 0.0f, -0.02f, 0.05f, 0.05f, 0.05f);
            Platform4BK.setupObject(shader_vert, shader_frag);
            Platform4BK.rotate(90f, 0, 0f);
            Platform4BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Platform4BK);

            Cylinder Platform5BK = new Cylinder();
            Platform5BK.createvertices(0.0f, 0.0f, -0.08f, 0.05f, 0.05f, 0.05f);
            Platform5BK.setupObject(shader_vert, shader_frag);
            Platform5BK.rotate(90f, 0, 0f);
            Platform5BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Platform5BK);

            Cylinder Platform6BK = new Cylinder();
            Platform6BK.createvertices(0.0f, 0.0f, -0.18f, 0.1f, 0.18f, 0.1f);
            Platform6BK.setupObject(shader_vert, shader_frag);
            Platform6BK.rotate(90f, 0, 0f);
            Platform6BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Platform6BK);

            Cone Curve1BK = new Cone();
            Curve1BK.createvertices(0.0f, 0.0f, 0.16f, 0.06f, 0.06f, 0.2f);
            Curve1BK.setupObject(shader_vert, shader_frag);
            Curve1BK.rotate(-90f, 0f, 0f);
            Curve1BK.setColor(0.2f, 0.2f, 0.2f);
            BlackKing.Add(Curve1BK);

            Cone Curve2BK = new Cone();
            Curve2BK.createvertices(0.0f, 0.0f, -0.09f, 0.096f, 0.096f, 0.07f);
            Curve2BK.setupObject(shader_vert, shader_frag);
            Curve2BK.rotate(-90f, 0, 0f);
            Curve2BK.setColor(0.2f, 0.2f, 0.2f);
            BlackKing.Add(Curve2BK);

            Sphere CoverBK = new Sphere();
            CoverBK.createvertices(0.0f, 0.0f, -0.117f, 0.06f, 0.06f, 0.0f);
            CoverBK.setupObject(shader_vert, shader_frag);
            CoverBK.rotate(90f, 0, 0f);
            CoverBK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(CoverBK);

            Cone Curve3BK = new Cone();
            Curve3BK.createvertices(0.0f, 0.0f, 0.18f, 0.036f, 0.036f, 0.04f);
            Curve3BK.setupObject(shader_vert, shader_frag);
            Curve3BK.rotate(-90f, 0, 0f);
            Curve3BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Curve3BK);

            Cone Curve4BK = new Cone();
            Curve4BK.createvertices(0.0f, 0.0f, -0.08f, 0.07f, 0.07f, 0.12f);
            Curve4BK.setupObject(shader_vert, shader_frag);
            Curve4BK.rotate(90f, 0, 0f);
            Curve4BK.setColor(0.2f, 0.2f, 0.2f);
            BlackKing.Add(Curve4BK);

            Cone Cross1BK = new Cone();
            Cross1BK.createvertices(0.0f, 0.0f, 0.38f, 0.036f, 0.036f, 0.078f);
            Cross1BK.setupObject(shader_vert, shader_frag);
            Cross1BK.rotate(-90f, 0, 0f);
            Cross1BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Cross1BK);

            Rectangle Cross2BK = new Rectangle();
            Cross2BK.createvertices(0.0f, 0.34f, 0.0f, 0.12f, 0.05f, 0.087f);
            Cross2BK.setupObject(shader_vert, shader_frag);
            Cross2BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Cross2BK);

            Sphere Cross3BK = new Sphere();
            Cross3BK.createvertices(0.0f, 0.37f, 0.0f, 0.025f, 0.025f, 0.025f);
            Cross3BK.setupObject(shader_vert, shader_frag);
            Cross3BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Cross3BK);

            Cone Cross4BK = new Cone();
            Cross4BK.createvertices(0.0f, 0.0f, 0.41f, 0.020f, 0.020f, 0.03f);
            Cross4BK.setupObject(shader_vert, shader_frag);
            Cross4BK.rotate(-90f, 0, 0f);
            Cross4BK.setColor(0.3f, 0.3f, 0.3f);
            BlackKing.Add(Cross4BK);

            for (int i = 0; i < BlackKing.Count; i++)
            {
                BlackKing[i].scale(1.69f);
                BlackKing[i].translate(0.0f, 0.0f, 0.0f);
               
            }
            #endregion

            #region Black Pawn            
            Sphere CoverBP = new Sphere();
            CoverBP.createvertices(0.0f, 0.0f, 0.23f, 0.11f, 0.11f, 0.0f);
            CoverBP.setupObject(shader_vert, shader_frag);
            CoverBP.rotate(270.0f, 0, 0);
            CoverBP.setColor(0.3f, 0.3f, 0.3f);
            BlackPawn.Add(CoverBP);

            Cylinder Platform1BP = new Cylinder();
            Platform1BP.createvertices(0.0f, 0.0f, 0.0f, 0.15f, 0.15f, 0.15f);
            Platform1BP.setupObject(shader_vert, shader_frag);
            Platform1BP.rotate(90f, 0, 0);
            Platform1BP.setColor(0.3f, 0.3f, 0.3f);
            BlackPawn.Add(Platform1BP);

            Cylinder Platform2BP = new Cylinder();
            Platform2BP.createvertices(0.0f, 0.0f, -0.098f, 0.098f, 0.98f);
            Platform2BP.setupObject(shader_vert, shader_frag);
            Platform2BP.rotate(90f, 0, 0);
            Platform2BP.setColor(0.2f, 0.2f, 0.2f);
            BlackPawn.Add(Platform2BP);

            Cylinder Platform3BP = new Cylinder();
            Platform3BP.createvertices(0.0f, 0.0f, -0.24f, 0.12f, 0.2f);
            Platform3BP.setupObject(shader_vert, shader_frag);
            Platform3BP.rotate(90f, 0, 0);
            Platform3BP.setColor(0.2f, 0.2f, 0.2f);
            BlackPawn.Add(Platform3BP);

            Cone Curve1BP = new Cone();
            Curve1BP.createvertices(0.0f, 0.0f, 0.19f, 0.06f, 0.06f, 0.06f);
            Curve1BP.setupObject(shader_vert, shader_frag);
            Curve1BP.rotate(-90f, 0, 0);
            Curve1BP.setColor(0.3f, 0.3f, 0.3f);
            BlackPawn.Add(Curve1BP);

            Cone Curve2BP = new Cone();
            Curve2BP.createvertices(0.0f, 0.0f, 0.15f, 0.1f, 0.1f, 0.1f);
            Curve2BP.setupObject(shader_vert, shader_frag);
            Curve2BP.rotate(-90f, 0, 0);
            Curve2BP.setColor(0.2f, 0.2f, 0.2f);
            BlackPawn.Add(Curve2BP);

            Cone Curve3BP = new Cone();
            Curve3BP.createvertices(0.0f, 0.0f, -0.12f, 0.07f, 0.07f, 0.07f);
            Curve3BP.setupObject(shader_vert, shader_frag);
            Curve3BP.rotate(90f, 0, 0);
            Curve3BP.setColor(0.3f, 0.3f, 0.3f);
            BlackPawn.Add(Curve3BP);

            Sphere BallBP = new Sphere();
            BallBP.createvertices(0.0f, 0.1f, 0.0f, 0.08f, 0.08f, 0.08f);
            BallBP.setupObject(shader_vert, shader_frag);
            BallBP.setColor(0.2f, 0.2f, 0.2f);
            BlackPawn.Add(BallBP);

            Sphere Ball2BP = new Sphere();
            Ball2BP.createvertices(0.0f, 0.299f, 0.0f, 0.08f, 0.08f, 0.08f);
            Ball2BP.setupObject(shader_vert, shader_frag);
            Ball2BP.setColor(0.2f, 0.2f, 0.2f);
            BlackPawn.Add(Ball2BP);

            for (int i = 0; i < BlackPawn.Count; i++)
            {
                BlackPawn[i].scale(1.4f);
                BlackPawn[i].translate(1.0f, 0.0f, 0f);
            }
            #endregion
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

         
            for (int i = 0; i < WhitePawn.Count; i++)
            {
                //WhitePawn[i].rotate(0.0f, 0.07f, 0.0f);
                WhitePawn[i].render(_camera);
                
                BlackPawn[i].render(_camera);
                //BlackPawn[i].rotate(0.0f, 0.07f, 0.0f);
            }
            for (int i = 0; i < WhiteKing.Count; i++)
            {
                WhiteKing[i].render(_camera);
                //WhiteKing[i].rotate(0.0f, 0.07f, 0.0f);
                BlackKing[i].render(_camera);
                //BlackKing[i].rotate(0.0f, 0.07f, 0.0f);
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