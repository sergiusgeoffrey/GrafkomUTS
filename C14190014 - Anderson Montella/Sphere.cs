using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace UTSGrafkom
{
    class Sphere : Mesh
    {
        public Sphere()
        {
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
           ;
             if(Render_Type == 0)
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
        public void createBall(float percent = 1)
        {
            int sharpness = 50;
            int hCount = sharpness;
            int vCount = sharpness;
            float radius = 0.5f;
            float divide = 1 / percent;
            float PI = (float)Math.PI;
            float hStep = 2 * PI / hCount;
            float vStep = PI / vCount;
            float hAngle, vAngle;

            for (int i = 0; i <= vCount; i++)
            {
                vAngle = PI / 2 - i * vStep;

                for (int j = 0; j <= hCount / divide; j++)
                {
                    hAngle = j * hStep;

                    float x = radius * (float)Math.Cos(vAngle) * (float)Math.Cos(hAngle);
                    float y = radius * (float)Math.Cos(vAngle) * (float)Math.Sin(hAngle);
                    float z = radius * (float)Math.Sin(vAngle);
                    vertices.Add(new Vector3(x, y, z));
                    normals.Add(new Vector3(x, y, z));
                }
            }
            vertices.Add(new Vector3(0, 0, 0));
            normals.Add(new Vector3(0, 0, 0));

            uint k1, k2;
            for (int i = 0; i < vCount; i++)
            {
                k1 = (uint)i * ((uint)(hCount / divide) + 1);
                k2 = (uint)k1 + (uint)(hCount / divide) + 1;

                for (int j = 0; j < hCount / divide; j++, k1++, k2++)
                {
                    if (i != 0)
                    {
                        vertexIndices.Add(k1);
                        vertexIndices.Add(k2);
                        vertexIndices.Add(k1 + 1);
                    }

                    if (i != (vCount - 1))
                    {
                        vertexIndices.Add(k1 + 1);
                        vertexIndices.Add(k2);
                        vertexIndices.Add(k2 + 1);
                    }

                    if (percent < 1.0f)
                    {
                        vertexIndices.Add(k1);
                        vertexIndices.Add((uint)(vertices.Count - 1));
                        vertexIndices.Add(k1 + 1);

                        vertexIndices.Add(k1 + 1);
                        vertexIndices.Add((uint)(vertices.Count - 1));
                        vertexIndices.Add(k2 + 1);
                    }
                }
            }
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
                for (float v = -_pi / 2; v <= _pi / 2 + increment; v += increment)
                {
                    temp_index++;
                    temp_vector.X = _X + _RadiusX * (float)Math.Cos(v) * (float)Math.Cos(u);
                    temp_vector.Y = _Y + _RadiusY * (float)Math.Cos(v) * (float)Math.Sin(u);
                    temp_vector.Z = _Z + _RadiusZ * (float)Math.Sin(v);
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
    }
}
