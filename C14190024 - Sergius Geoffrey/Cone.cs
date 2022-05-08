using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace UTSGrafkom
{
    class Cone : Mesh
    {
        public Cone()
        {
        }

        public override void createvertices(float x, float y, float z, float lengthX, float lengthY, float lengthZ)
        {
            float _X = x;
            float _Y = y;
            float _Z = z;

            float _RadiusX = lengthX;
            float _RadiusY = lengthY;
            float _RadiusZ = lengthZ;
            Vector3 temp_vector = new Vector3();
            float _pi = 3.14159f;
            int count = 30;
            int temp_index = -1;
            float increment = 2 * _pi / count;
            for (float u = -_pi; u <= _pi + increment; u += increment)
            {
                for (float v = -_pi / 2; v <= 0; v += increment)
                {
                    temp_index++;
                    temp_vector.X = _X + _RadiusX * (float)Math.Cos(u) * v;
                    temp_vector.Y = _Y + _RadiusY * (float)Math.Sin(u) * v;
                    temp_vector.Z = _Z + _RadiusZ * v;
                    vertices.Add(temp_vector);
                    if (u != -_pi)
                    {
                        if ((temp_index % count) + 1 < count)
                        {
                            vertexIndices.Add(Convert.ToUInt32(temp_index));
                            vertexIndices.Add(Convert.ToUInt32(temp_index - count));
                            vertexIndices.Add(Convert.ToUInt32(temp_index - count + 1));
                        }
                        if (temp_index % count > 0)
                        {
                            vertexIndices.Add(Convert.ToUInt32(temp_index));
                            vertexIndices.Add(Convert.ToUInt32(temp_index - count));
                            vertexIndices.Add(Convert.ToUInt32(temp_index - 1));
                        }
                    }
                }
                if (u == -_pi)
                {
                    count = vertices.Count;
                    Console.WriteLine(count);
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
                //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                //GL.DrawElements(PrimitiveType.Triangles,
                //vertexIndices.Count,
                //DrawElementsType.UnsignedInt, 0);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                GL.DrawElements(PrimitiveType.Triangles,
                vertexIndices.Count,
                DrawElementsType.UnsignedInt, 0);
            }
            foreach (var meshobj in child)
            {
                meshobj.render(_cam);
            }
        }
    }
}
