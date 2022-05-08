using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace UTSGrafkom
{
    class Rectangle : Mesh
    {
        public Rectangle()
        {
        }
        public override void createvertices(float x, float y, float z,float radiusX,float radiusY,float radiusZ)
        {
            //biar lebih fleksibel jangan inisialiasi posisi dan 
            //panjang kotak didalam tapi ditaruh ke parameter
            float _positionX = x;
            float _positionY = y;
            float _positionZ = z;


            //Buat temporary vector
            Vector3 temp_vector;
            //1. Inisialisasi vertex
            // Titik 1
            temp_vector.X = _positionX - radiusX / 2.0f; // x 
            temp_vector.Y = _positionY + radiusY / 2.0f; // y
            temp_vector.Z = _positionZ - radiusZ / 2.0f; // z

            vertices.Add(temp_vector);

            // Titik 2
            temp_vector.X = _positionX + radiusX / 2.0f; // x
            temp_vector.Y = _positionY + radiusY / 2.0f; // y
            temp_vector.Z = _positionZ - radiusZ / 2.0f; // z

            vertices.Add(temp_vector);
            // Titik 3
            temp_vector.X = _positionX - radiusX / 2.0f; // x
            temp_vector.Y = _positionY - radiusY / 2.0f; // y
            temp_vector.Z = _positionZ - radiusZ / 2.0f; // z
            vertices.Add(temp_vector);

            // Titik 4
            temp_vector.X = _positionX + radiusX / 2.0f; // x
            temp_vector.Y = _positionY - radiusY / 2.0f; // y
            temp_vector.Z = _positionZ - radiusZ / 2.0f; // z

            vertices.Add(temp_vector);

            // Titik 5
            temp_vector.X = _positionX - radiusX / 2.0f; // x
            temp_vector.Y = _positionY + radiusY / 2.0f; // y
            temp_vector.Z = _positionZ + radiusZ / 2.0f; // z

            vertices.Add(temp_vector);

            // Titik 6
            temp_vector.X = _positionX + radiusX / 2.0f; // x
            temp_vector.Y = _positionY + radiusY / 2.0f; // y
            temp_vector.Z = _positionZ + radiusZ / 2.0f; // z

            vertices.Add(temp_vector);

            // Titik 7
            temp_vector.X = _positionX - radiusX / 2.0f; // x
            temp_vector.Y = _positionY - radiusY / 2.0f; // y
            temp_vector.Z = _positionZ + radiusZ / 2.0f; // z

            vertices.Add(temp_vector);

            // Titik 8
            temp_vector.X = _positionX + radiusX / 2.0f; // x
            temp_vector.Y = _positionY - radiusY / 2.0f; // y
            temp_vector.Z = _positionZ + radiusZ / 2.0f; // z

            vertices.Add(temp_vector);
            //2. Inisialisasi index vertex
            vertexIndices = new List<uint> {
                // Segitiga Depan 1
                0, 1, 2,
                // Segitiga Depan 2
                1, 2, 3,
                // Segitiga Atas 1
                0, 4, 5,
                // Segitiga Atas 2
                0, 1, 5,
                // Segitiga Kanan 1
                1, 3, 5,
                // Segitiga Kanan 2
                3, 5, 7,
                // Segitiga Kiri 1
                0, 2, 4,
                // Segitiga Kiri 2
                2, 4, 6,
                // Segitiga Belakang 1
                4, 5, 6,
                // Segitiga Belakang 2
                5, 6, 7,
                // Segitiga Bawah 1
                2, 3, 6,
                // Segitiga Bawah 2
                3, 6, 7
            };
        }
    }
}
