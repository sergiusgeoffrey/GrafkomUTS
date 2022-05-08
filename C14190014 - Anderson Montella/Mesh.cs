using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using System.Globalization;

namespace UTSGrafkom
{
    class Mesh
    {
        //Vector 3 pastikan menggunakan OpenTK.Mathematics
        //tanpa protected otomatis komputer menganggap sebagai private
        protected List<Vector3d> vertices = new List<Vector3d>();
        protected List<Vector3d> textureVertices = new List<Vector3d>();
        protected List<Vector3d> normals = new List<Vector3d>();
        protected List<uint> vertexIndices = new List<uint>();
        protected int _vertexBufferObject;
        protected int _elementBufferObject;
        protected int _vertexArrayObject;
        protected Shader _shader;
        protected Matrix4 transform;
        protected Vector3 Color;
        protected int counter = 0;
        public List<Mesh> child = new List<Mesh>();
        public Mesh()
        {
        }
        public void setColor(float r, float g,float b)
        {
            Color = new Vector3(r, g, b);
        }
        public void setupObject(string vert, string frag)
        {

            //inisialisasi Transformasi
            transform = Matrix4.Identity;
            //inisialisasi buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            //parameter 2 yg kita panggil vertices.Count == array.length
            GL.BufferData<Vector3d>(BufferTarget.ArrayBuffer,
                vertices.Count * Vector3d.SizeInBytes,
                vertices.ToArray(),
                BufferUsageHint.StaticDraw);
            //inisialisasi array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Double, false, 3 * sizeof(double), 0);
            GL.EnableVertexAttribArray(0);
            //inisialisasi index vertex
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            //parameter 2 dan 3 perlu dirubah
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                vertexIndices.Count * sizeof(uint),
                vertexIndices.ToArray(), BufferUsageHint.StaticDraw);
            //inisialisasi shader
            _shader = new Shader(vert, frag);
            _shader.Use();
            scale(1.0f);
        }

        public virtual void createvertices(float x, float y, float z, float lengthX, float lengthY, float lengthZ) { }
        public virtual void createvertices(float x, float y, float z, float lengthX, float lengthY) { }
        public virtual void createvertices(List<Vector3d> points) { }

        public virtual void render(PrimitiveType mode = PrimitiveType.TriangleFan) { }
        public virtual void render(Camera _cam,int Render_Type = 0)
        {
            //render itu akan selalu terpanggil setiap frame
            _shader.Use();
            _shader.SetVector3("Color", Color);
            _shader.SetMatrix4("transform", transform);
            _shader.SetMatrix4("view", _cam.GetViewMatrix());
            _shader.SetMatrix4("projection", _cam.GetProjectionMatrix());
            //_shader.SetMatrix4("transform", transform * Window.View * Window.Projection);
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
        public List<Vector3d> getVertices()
        {
            return vertices;
        }
        public List<uint> getVertexIndices()
        {
            return vertexIndices;
        }

        public void setVertexIndices(List<uint> temp)
        {
            vertexIndices = temp;
        }
        public int getVertexBufferObject()
        {
            return _vertexBufferObject;
        }

        public int getElementBufferObject()
        {
            return _elementBufferObject;
        }

        public int getVertexArrayObject()
        {
            return _vertexArrayObject;
        }

        public Shader getShader()
        {
            return _shader;
        }

        public Matrix4 getTransform()
        {
            return transform;
        }

        public void rotate(float x, float y, float z)
        {
            transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(x));
            transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(y));
            transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(z));
            foreach (var meshobj in child)
            {
                meshobj.rotate(x, y, z);
            }
        }
        public void scale(float sc = 1.0f)
        {
            transform = transform * Matrix4.CreateScale(sc);
        }
        public void scale(float x=1.0f,float y=1.0f,float z=1.0f)
        {
            transform = transform * Matrix4.CreateScale(x,y,z);
        }
        public void translate(float x=0.0f,float y=0.0f ,float z=0.0f)
        {
            transform = transform * Matrix4.CreateTranslation(x, y, z);
        }

        
        public double FixedStringToDouble(string input)
        {
            double processed = double.Parse(input, CultureInfo.InvariantCulture);
            return processed;
        }

        public void LoadObjFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
            }
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
                    words.RemoveAll(s => s == string.Empty);
                    if (words.Count == 0)
                        continue;

                    string type = words[0];
                    words.RemoveAt(0);


                    switch (type)
                    {
                        case "v":
                            vertices.Add(new Vector3d(FixedStringToDouble(words[0]) / 10, FixedStringToDouble(words[1]) / 10, FixedStringToDouble(words[2]) / 10));
                            break;

                        case "vt":
                            textureVertices.Add(new Vector3d(FixedStringToDouble(words[0]), FixedStringToDouble(words[1]),
                                                            words.Count < 3 ? 0 : FixedStringToDouble(words[2])));
                            break;

                        case "vn":
                            normals.Add(new Vector3d(FixedStringToDouble(words[0]), FixedStringToDouble(words[1]), FixedStringToDouble(words[2])));
                            break;

                        case "f":
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                    continue;

                                string[] comps = w.Split('/');

                                vertexIndices.Add(uint.Parse(comps[0]) - 1);

                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

    }
}
