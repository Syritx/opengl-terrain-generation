using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace terrain_generation
{
    public class Game : GameWindow
    {
        Vector3[] vertices;
        int length = 100;
        float intensity = .1f;
        int layers = 15;
        int min = -10, max = 10;

        public Game(int width, int height)
            : base(width,height,GraphicsMode.Default,"Procedural Terrain Generation") {

            start();
        }

        void start() {

            // creating heightMaps
            vertices = Noise.GenerateHeightMaps(layers, length, intensity, min, max);

            RenderFrame += render;
            Resize += resize;
            Load += load;

            GL.Translate(0, -20, 0);
            Run(60);
        }

        void render(object sender, EventArgs e) {
            GL.Rotate(.1, 0, 1, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // rendering terrain
            for (int i = 0; i < vertices.Length; i++) {
                GL.Begin(PrimitiveType.Quads); // CHANGE THIS TO QUAD

                GL.Color3((double)114 / 255, (double)179 / 255, (double)29 / 255);
                GL.Vertex3(vertices[i]);

                try {
                    //GL.Color3(0, 0, 0);
                    GL.Vertex3(vertices[i + 1]);

                    int x1 = (int)vertices[i+length+1].X, x2 = (int)vertices[i+length].X;
                    if (x1 == x2) {
                        //GL.Color3(0.5, 0.5, 0.5);
                        GL.Vertex3(vertices[i+length+1]);
                        GL.Vertex3(vertices[i+length]);
                    }
                }catch (Exception ex) {}

                GL.End();
            }

            GL.Enable(EnableCap.Fog);

            // Fog
            float[] colors = { 230, 230, 230 };
            GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
            GL.Hint(HintTarget.FogHint, HintMode.Nicest);
            GL.Fog(FogParameter.FogColor, colors);

            GL.Fog(FogParameter.FogStart, (float)1000 / 100.0f);
            GL.Fog(FogParameter.FogEnd, 250.0f);

            SwapBuffers();
        }

        void resize(object sender, EventArgs e) {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(1, Width / Height, 1.0f, 200.0f);
            GL.LoadMatrix(ref perspectiveMatrix);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.End();
        }

        void load(object sender, EventArgs e) {
            GL.ClearColor(0, 0, 0, 0);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
