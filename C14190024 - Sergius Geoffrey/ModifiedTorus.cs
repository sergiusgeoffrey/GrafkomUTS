using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;


namespace UTSGrafkom
{
    class ModifiedTorus : Mesh
    {
        public ModifiedTorus()
        {

        }
        public override void createvertices(float x, float y, float z, float radius, float width)
        {
            float _X = x;
            float _Y = y;
            float _Z = z;

            float _Radius = radius;
            Vector3 temp_vector = new Vector3();
            float _pi = 3.14159f;
            int count = 30;
            int temp_index = 0;
            float increment = 2 * _pi / count;
            int counter = 0;
            for (float u = 0; u <= 2 * _pi; u += increment)
            {
                counter = 0;
                for (float v = 0; v <= 2 * _pi; v += increment)
                {
                    counter++;
                    temp_index++;                              //supposed to be Cos 
                    temp_vector.X = _X + (_Radius + width * (float)Math.Sin(u)) * (float)Math.Cos(v);
                                                              //supposed to be Cos 
                    temp_vector.Y = _Y + (_Radius + width * (float)Math.Sin(u)) * (float)Math.Sin(v);
                    temp_vector.Z = _Z + width * (float)Math.Sin(u);
                    vertices.Add(temp_vector);

                }
            }
            uint k1, k2;
            int hCount = 30;
            int vCount = 30;
            for (int i = 0; i < vCount; i++)
            {
                k1 = (uint)i * ((uint)(hCount) + 1);
                k2 = (uint)k1 + (uint)(hCount) + 1;
                for (int j = 0; j < hCount; j++, k1++, k2++)
                {
                    vertexIndices.Add(k1);
                    vertexIndices.Add(k2);
                    vertexIndices.Add(k1 + 1);

                    vertexIndices.Add(k1 + 1);
                    vertexIndices.Add(k2);
                    vertexIndices.Add(k2 + 1);
                }
            }


        }

        public override void render(Camera _cam, int Render_Type = 0)
        {
            //render itu akan selalu terpanggil setiap frame
            _shader.Use();
            _shader.SetVector3("Color", Color);
            _shader.SetMatrix4("transform", transform);
            _shader.SetMatrix4("view", _cam.GetViewMatrix());
            _shader.SetMatrix4("projection", _cam.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject);
            //perlu diganti di parameter 2
            if (Render_Type == 0)
            {
                GL.DrawElements(PrimitiveType.Triangles,
                vertexIndices.Count,
                DrawElementsType.UnsignedInt, 0);
            }
            else if (Render_Type == 1)//face
            {
                GL.DrawElements(PrimitiveType.Points,
                vertexIndices.Count,
                DrawElementsType.UnsignedInt, 0);
            }
            else if (Render_Type == 2)//wireframe
            {
                GL.DrawElements(PrimitiveType.Lines,
                vertexIndices.Count,
                DrawElementsType.UnsignedInt, 0);
            }
            else if (Render_Type == 3)// true wireframe
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                GL.DrawElements(PrimitiveType.Triangles,
                vertexIndices.Count,
                DrawElementsType.UnsignedInt, 0);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
            foreach (var meshobj in child)
            {
                meshobj.render(_cam);
            }
        }
    }
}

