using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation.aimesh
{
    public class NaviMeshTest
    {
        public const int AIMESH_TEXTURE_SIZE = 1024;
        public BinaryReader buffer;
        public __AIMESHFILE fileStream;

        public double lowX, lowY, highX, highY, lowestZ;
        public ScanLine[] scanlineLowest = new ScanLine[AIMESH_TEXTURE_SIZE];
        public ScanLine[] scanlineHighest = new ScanLine[AIMESH_TEXTURE_SIZE];
        public float[] heightMap;
        public float[] xMap;
        public float[] yMap;
        public float mapWidth, mapHeight;
        public bool loaded;
        public NavigationMesh NavigationMesh;
        public Actor PathObject;
        public Actor ControlObject;
        public NaviMeshTest(byte[] data) : this(new MemoryStream(data))
        {
        }

        public NaviMeshTest(string filePath) : this(File.ReadAllBytes(filePath))
        {
        }
        public NaviMeshTest(Stream stream)
        {
            buffer = new BinaryReader(stream);
            for (var i = 0; i < AIMESH_TEXTURE_SIZE; i++) // For every scanline for the triangle rendering
            {
                scanlineLowest[i] = new ScanLine { u = 1e10f, v = 1e10f, x = 1e10f, y = 1e10f, z = 1e10f };
                scanlineHighest[i] = new ScanLine { u = -1e10f, v = -1e10f, x = -1e10f, y = -1e10f, z = -1e10f };
            }

            heightMap = new float[AIMESH_TEXTURE_SIZE * AIMESH_TEXTURE_SIZE]; // Shows occupied or not
            xMap = new float[AIMESH_TEXTURE_SIZE * AIMESH_TEXTURE_SIZE]; // Shows x
            yMap = new float[AIMESH_TEXTURE_SIZE * AIMESH_TEXTURE_SIZE]; // Shows y

            fileStream = new __AIMESHFILE
            {
                magic = buffer.ReadChars(8),
                version = buffer.ReadInt32(),
                triangle_count = buffer.ReadInt32(),
                zero = new[] { buffer.ReadInt32(), buffer.ReadInt32() }
            };

            for (var i = 0; i < fileStream.triangle_count; i++)
            {
                var triangle = new Triangle();
                for (var j = 0; j < 3; j++)
                {
                    triangle.Face.v1[j] = buffer.ReadSingle();
                }
                for (var j = 0; j < 3; j++)
                {
                    triangle.Face.v2[j] = buffer.ReadSingle();
                }
                for (var j = 0; j < 3; j++)
                {
                    triangle.Face.v3[j] = buffer.ReadSingle();
                }

                triangle.unk1 = buffer.ReadInt16();
                triangle.unk2 = buffer.ReadInt16();
                triangle.triangle_reference = buffer.ReadInt16();

                fileStream.triangles[i] = triangle;
            }

            NavigationMesh = new NavigationMesh();
            outputMesh(AIMESH_TEXTURE_SIZE, AIMESH_TEXTURE_SIZE);

            //writeFile(AIMESH_TEXTURE_SIZE, AIMESH_TEXTURE_SIZE);

            loaded = true;
        }
        bool outputMesh(int width, int height)
        {
            mapHeight = mapWidth = 0;
            for (var i = 0; i < width * height; i++)
            {
                heightMap[i] = -99999.99f; // Clear the map
            }

            lowX = 9e9;
            lowY = 9e9;
            highX = 0;
            highY = 0;
            lowestZ = 9e9; // Init triangle

            for (var i = 0; i < fileStream.triangle_count; i++)
            // Need to find the absolute values.. So we can map it to AIMESH_TEXTURE_SIZExAIMESH_TEXTURE_SIZE instead of 13000x15000
            {
                // Triangle low X check
                if (fileStream.triangles[i].Face.v1[0] < lowX)
                {
                    lowX = fileStream.triangles[i].Face.v1[0];
                }
                if (fileStream.triangles[i].Face.v2[0] < lowX)
                {
                    lowX = fileStream.triangles[i].Face.v2[0];
                }
                if (fileStream.triangles[i].Face.v3[0] < lowX)
                {
                    lowX = fileStream.triangles[i].Face.v3[0];
                }

                // Triangle low Y check
                if (fileStream.triangles[i].Face.v1[2] < lowY)
                {
                    lowY = fileStream.triangles[i].Face.v1[2];
                }
                if (fileStream.triangles[i].Face.v2[2] < lowY)
                {
                    lowY = fileStream.triangles[i].Face.v2[2];
                }
                if (fileStream.triangles[i].Face.v3[2] < lowY)
                {
                    lowY = fileStream.triangles[i].Face.v3[2];
                }

                // Triangle high X check
                if (fileStream.triangles[i].Face.v1[0] > highX)
                {
                    highX = fileStream.triangles[i].Face.v1[0];
                }
                if (fileStream.triangles[i].Face.v2[0] > highX)
                {
                    highX = fileStream.triangles[i].Face.v2[0];
                }
                if (fileStream.triangles[i].Face.v3[0] > highX)
                {
                    highX = fileStream.triangles[i].Face.v3[0];
                }

                // Triangle high Y check
                if (fileStream.triangles[i].Face.v1[2] > highY)
                {
                    highY = fileStream.triangles[i].Face.v1[2];
                }
                if (fileStream.triangles[i].Face.v2[2] > highY)
                {
                    highY = fileStream.triangles[i].Face.v2[2];
                }
                if (fileStream.triangles[i].Face.v3[2] > highY)
                {
                    highY = fileStream.triangles[i].Face.v3[2];
                }
            }

            mapWidth = (float)(highX + lowX);
            mapHeight = (float)(highY + lowY);

            // If the width or width larger?
            if (highY - lowY < highX - lowX)
            {
                highX = 1.0f / (highX - lowX) * width; // We're wider than we're high, map on width
                highY = highX; // Keep aspect ratio Basically, 1 y should be 1 x.
                lowY = 0; // Though we need to project this in the middle, no offset
            }
            else
            {
                highY = 1.0f / (highY - lowY) * height; // We're higher than we're wide, map on width
                highX = highY; // Keep aspect ratio Basically, 1 x should be 1 y.
                               // lowX = 0; // X is already in the middle? ??????
            }

            //   PathObject = new Actor();
            //   PathObject.Create(NavigationMesh, Vector3.Zero, null);

            //voir si utile : 
            //   ControlObject = new Actor();
            //   ControlObject.Create(NavigationMesh, Vector3.Zero, null);

            for (var i = 0; i < fileStream.triangle_count; i++) // For every triangle
            {
                var t_Triangle = new Triangle(); // Create a triangle that is warped to heightmap coordinates
                t_Triangle.Face.v1[0] = (float)((fileStream.triangles[i].Face.v1[0] - lowX) * highX);
                t_Triangle.Face.v1[1] = fileStream.triangles[i].Face.v1[1];
                t_Triangle.Face.v1[2] = (float)((fileStream.triangles[i].Face.v1[2] - lowY) * highY);

                t_Triangle.Face.v2[0] = (float)((fileStream.triangles[i].Face.v2[0] - lowX) * highX);
                t_Triangle.Face.v2[1] = fileStream.triangles[i].Face.v2[1];
                t_Triangle.Face.v2[2] = (float)((fileStream.triangles[i].Face.v2[2] - lowY) * highY);

                t_Triangle.Face.v3[0] = (float)((fileStream.triangles[i].Face.v3[0] - lowX) * highX);
                t_Triangle.Face.v3[1] = fileStream.triangles[i].Face.v3[1];
                t_Triangle.Face.v3[2] = (float)((fileStream.triangles[i].Face.v3[2] - lowY) * highY);

                /*
                // Draw just the wireframe.
                drawLine(t_Triangle.Face.v1[0], t_Triangle.Face.v1[2], t_Triangle.Face.v2[0], t_Triangle.Face.v2[2], t_Info, width, height);
                drawLine(t_Triangle.Face.v2[0], t_Triangle.Face.v2[2], t_Triangle.Face.v3[0], t_Triangle.Face.v3[2], t_Info, width, height);
                drawLine(t_Triangle.Face.v3[0], t_Triangle.Face.v3[2], t_Triangle.Face.v1[0], t_Triangle.Face.v1[2], t_Info, width, height);
                */

                // Draw this triangle onto the heightmap using an awesome triangle drawing function
                AddPlaneToNavigationMesh(t_Triangle);
            }
            NavigationMesh.LinkCells();

            //writeFile(t_Info, width, height);
            return true;
        }
        void AddPlaneToNavigationMesh(Triangle triangle)
        {
            Vector3 vertA = new(triangle.Face.v1[0], triangle.Face.v1[1], triangle.Face.v1[2]);
            Vector3 vertB = new(triangle.Face.v2[0], triangle.Face.v2[1], triangle.Face.v2[2]);
            Vector3 vertC = new(triangle.Face.v3[0], triangle.Face.v3[1], triangle.Face.v3[2]);


            // Add the triangle to your navigation mesh
            var tr = new Actor.Triangle
            {
                Vert = new Vector3[3] { vertA, vertB, vertC }
            };

            if (IsValidTriangle(vertA, vertB, vertC))
            {
                NavigationMesh.AddCell(vertA, vertB, vertC);  // Make sure this function works correctly
                                                              // PathObject.AddTriangle(tr);
                                                              // ControlObject.AddTriangle(tr);
            }
            else
            {
                Debug.Assert(false, "The triangle is not valid and cannot be added to the navigation mesh.");
            }
        }
        bool IsValidTriangle(Vector3 a, Vector3 b, Vector3 c)
        {
            // Make sure the vertices are not aligned or identical
            return a != b && b != c && c != a;
        }
    }
}
//            new Plane(vertA, vertB, vertC);