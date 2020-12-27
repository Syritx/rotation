using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System;

namespace rotation {

    class Polygon {

        Shader shader;
        int vao, vbo;
        float angle = 0, rotationSpeed;
        Vector2 size, position;

        public Polygon(float angle, Vector2 size, Vector2 position, float rotationSpeed) {

            this.angle = angle;
            this.size = size;
            this.position = position;
            this.rotationSpeed = rotationSpeed;
                

            shader = new Shader("Shaders/vertex.glsl", "Shaders/fragment.glsl");
            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            vao = GL.GenVertexArray();
        }

        public void Render() {
            angle+=(rotationSpeed/100);

            float xPosition = ((float)(size.X) * (float)Math.Cos(angle)) - ((float)(size.Y) * (float)Math.Sin(angle));
            float yPosition = ((float)(size.X) * (float)Math.Sin(angle)) + ((float)(size.Y) * (float)Math.Cos(angle));

            float xPosition90Deg = ((float)(size.X) * (float)Math.Cos(angle+Math.Tan(45))) - ((float)(size.Y) * (float)Math.Sin(angle+Math.Tan(45)));
            float yPosition90Deg = ((float)(size.X) * (float)Math.Sin(angle+Math.Tan(45))) + ((float)(size.Y) * (float)Math.Cos(angle+Math.Tan(45)));

            float[] vertices = {
                xPosition+position.X,  yPosition+position.Y, 0,
               -xPosition90Deg+position.X, -yPosition90Deg+position.Y, 0,

               -xPosition+position.X, -yPosition+position.Y, 0,
               -xPosition90Deg+position.X, -yPosition90Deg+position.Y, 0,

               -xPosition+position.X, -yPosition+position.Y, 0,
                xPosition90Deg+position.X,  yPosition90Deg+position.Y, 0,

               xPosition+position.X, yPosition+position.Y, 0,
               xPosition90Deg+position.X, yPosition90Deg+position.Y, 0,
            };

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3*sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader.UseShader();

            GL.DrawArrays(PrimitiveType.Lines, 0, vertices.Length);
        }
    }
}