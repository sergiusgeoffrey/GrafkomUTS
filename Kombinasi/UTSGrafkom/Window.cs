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
    class Window : GameWindow
    {
        string shader_vert = "../../../Shaders/shader.vert";
        string shader_frag = "../../../Shaders/shader.frag";
        List<Mesh> Board = new List<Mesh>();
        List<Mesh> WhitePawn = new List<Mesh>();
        List<Mesh> WhiteKing = new List<Mesh>();
        List<Mesh> BlackKing = new List<Mesh>();
        List<Mesh> BlackPawn = new List<Mesh>();
        List<Mesh> BlackBishop = new List<Mesh>();

        List<Vector3d> positions = new List<Vector3d>();
        List<Curve> Indikator = new List<Curve>();
        List<Curve> Indikator2 = new List<Curve>();
        Camera _camera;
        bool _firstMove = true;
        Vector2 _lastPos;
        Mesh square;
        bool kingEat = false;
        bool bishopMove = false;
        bool bishopBack = false;
        bool kingBack = false;
        bool pawnUp = true;
        bool bishopUp = true;
        bool interrupt = false;
        bool onEnd = false;
        bool indicatorX = true;
        int count = 0;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {

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
                WhiteKing[i].translate(-2.2f, 0.225f, 0.85f);
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
                WhitePawn[i].translate(-1.5f, -0.1f, 0.15f);
                
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
                BlackKing[i].translate(-0.1f, 0.225f, 0.15f);
               
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
                BlackPawn[i].translate(2.0f, -0.1f, 1.55f);
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
                    if (i == 6)
                        Kurva.setColor(0.0f, 1.0f - recolor, 0.0f + recolor);
                    else if (i == 7)
                        Kurva.setColor(0.5f, 0.0f, 0.5f + recolor);
                    else
                        Kurva.setColor(0.5f, 0.5f, 0.5f);
                }
                Kurva.translate(-2.9f + move, 0.0f, -2.3f);
                move += 0.7f;
                recolor += 0.25f;
                Indikator.Add(Kurva);
    
            }
            recolor = 0;
            move = 0;
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
                    if (i == 6)
                        Kurva.setColor(0.0f, 1.0f - recolor, 0.0f + recolor);
                    else if (i == 7)
                        Kurva.setColor(0.5f, 0.0f, 0.5f + recolor);
                    else
                        Kurva.setColor(0.5f, 0.5f, 0.5f);
                }
                Kurva.rotate(0, 90.0f, 0);
                Kurva.translate(-3.25f , .0f, -1.95f+move);
                move += 0.7f;
                recolor += 0.25f;
                Indikator2.Add(Kurva);

            }
            #endregion
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            for(int i=0;i<Indikator.Count;i++)
            {
                if (indicatorX == true)
                {
                    Indikator[i].render(_camera);
                    Indikator2[i].render(_camera);
                }
            }            
            for (int i = 0; i < Board.Count; i++)
            {
                Board[i].render(_camera);
            }
            for (int i = 0; i < WhitePawn.Count; i++)
            {
                if (pawnUp)
                {
                    WhitePawn[i].render(_camera);
                }
                BlackPawn[i].render(_camera);
            }
            for (int i = 0; i < WhiteKing.Count; i++)
            {
                WhiteKing[i].render(_camera);
                BlackKing[i].render(_camera);
            }
            for (int i = 0; i < BlackBishop.Count; i++)
            {
                if (bishopUp)
                    BlackBishop[i].render(_camera);
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
            if (input.IsKeyDown(Keys.E))
            {
                indicatorX = true;
            }
            if (input.IsKeyDown(Keys.R))
            {
                indicatorX = false;
            }
            if (input.IsKeyDown(Keys.P) && bishopMove == false && interrupt == false && onEnd == false)  
            {
                count = 0;
                bishopMove = true;
                bishopBack = false;
                kingBack = false;
                interrupt = true;
            }
            if (input.IsKeyDown(Keys.O) && bishopBack == false && interrupt == false && onEnd == true)  
            {
                count = 0;
                bishopBack = true;
                bishopMove = false;
                interrupt = true;
               
                bishopUp = true;
                kingBack = true;
            }
            for (int i = 0; i < BlackBishop.Count; i++)
            {
                if (bishopMove)
                {
                    if (count < 525)
                    {
                        BlackBishop[i].translate(-0.004f, 0f, 0.004f);
                        interrupt = true;
                    }
                    else
                    {
                        pawnUp = false;
                        bishopMove = false;
                        interrupt = false;
                        onEnd = true;
                        kingEat = true;
                  
                    }
                }
                if(bishopBack)
                {
                    if (count < 525)
                    {
                        BlackBishop[i].translate(0.004f, 0f, -0.004f);
                        interrupt = true;
                    }
                    else
                    {
                        bishopBack = false;
                        interrupt = false;
                        onEnd = false;
                        kingEat = false;
      
                    }
                }
            }
            for (int i = 0; i < WhiteKing.Count; i++)
            {
                if (kingEat)
                {
                    if (count < 700)
                    {
                        WhiteKing[i].translate(0.004f, 0, -0.004f);
                        interrupt = true;
                    }
                    else if (count > 700)
                    {
                        bishopUp = false;
                        interrupt = false;
                    }
                }
                if (kingBack)
                {
                    if (count < 700)
                    {
                        WhiteKing[i].translate(-0.004f, 0, 0.004f);
                        interrupt = true;
                    }
                    else
                    {
                        pawnUp = true;
                        interrupt = false;
                    }
                }
            }
            count++;
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