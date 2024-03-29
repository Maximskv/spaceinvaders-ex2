using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DreidelGame.ObjectModel
{
    /// <summary>
    /// A cube that contains ColorPosition vertices
    /// </summary>
    public class CubePosition : Cube
    {                
        private readonly Color r_BoxColor = Color.BurlyWood;
        private readonly Color r_FrontColor = Color.Yellow;
        private readonly Color r_BackColor = Color.Red;
        private readonly Color r_LeftColor = Color.Green;
        private readonly Color r_RightColor = Color.Blue;
        private const int k_VerticesNum = 24;
        protected const int k_TrianglesNum = 12;

        private VertexPositionColor[] m_Vertices;

        /// <summary>
        /// Gets the number of triangles the pyramid has
        /// </summary>
        public override int     TriangleNum
        {
            get { return k_TrianglesNum; }
        }

        public CubePosition(Game i_Game)
            : base(i_Game)
        {
        }     

        /// <summary>
        /// Create the cube sides as VertexPositionColor, and in addition creates all the letters
        /// that are presented in each cube side
        /// </summary>
        protected override void     CreateSides()
        {
            createVertices();
            createIndices();
            InitBuffers();

            Add(new TriangleHolder<VertexPositionColor>(
                this.Game,
                VertexPositionColor.VertexElements,
                k_TrianglesNum,
                k_VerticesNum,
                false,
                this.ComponentVertexBuffer,
                this.ComponentIndexBuffer,
                this.BufferIndices));
        }

        /// <summary>
        /// Creates the component vertices
        /// </summary>
        private void createVertices()
        {
            m_Vertices = new VertexPositionColor[k_VerticesNum];

            createFrontVertices();
            createBackVertices();
            createRightVertices();
            createLeftVertices();
            createTopVertices();
            createBottomVertices();

            this.VerticesNum = k_VerticesNum;
        }

        /// <summary>
        /// Create the bottom side vertices
        /// </summary>
        private void createBottomVertices()
        {
            m_Vertices[20] = new VertexPositionColor(
                m_VerticesCoordinates[3],
                r_UpDownColor);
            m_Vertices[21] = new VertexPositionColor(
                m_VerticesCoordinates[4],
                r_UpDownColor);
            m_Vertices[22] = new VertexPositionColor(
                m_VerticesCoordinates[7],
                r_UpDownColor);
            m_Vertices[23] = new VertexPositionColor(
                m_VerticesCoordinates[0],
                r_UpDownColor);
        }

        /// <summary>
        /// Create the top side vertices
        /// </summary>
        private void createTopVertices()
        {
            m_Vertices[16] = new VertexPositionColor(
                m_VerticesCoordinates[1],
                r_UpDownColor);
            m_Vertices[17] = new VertexPositionColor(
                m_VerticesCoordinates[6],
                r_UpDownColor);
            m_Vertices[18] = new VertexPositionColor(
                m_VerticesCoordinates[5],
                r_UpDownColor);
            m_Vertices[19] = new VertexPositionColor(
                m_VerticesCoordinates[2],
                r_UpDownColor);
        }

        /// <summary>
        /// Create the left side vertices
        /// </summary>
        private void createLeftVertices()
        {
            m_Vertices[12] = new VertexPositionColor(
                m_VerticesCoordinates[7],
                r_LeftColor);
            m_Vertices[13] = new VertexPositionColor(
                m_VerticesCoordinates[6],
                r_LeftColor);
            m_Vertices[14] = new VertexPositionColor(
                m_VerticesCoordinates[1],
                r_LeftColor);
            m_Vertices[15] = new VertexPositionColor(
                m_VerticesCoordinates[0],
                r_LeftColor);
        }

        /// <summary>
        /// Create the right side vertices
        /// </summary>
        private void createRightVertices()
        {
            m_Vertices[8] = new VertexPositionColor(
                m_VerticesCoordinates[3],
                r_RightColor);
            m_Vertices[9] = new VertexPositionColor(
                m_VerticesCoordinates[2],
                r_RightColor);
            m_Vertices[10] = new VertexPositionColor(
                m_VerticesCoordinates[5],
                r_RightColor);
            m_Vertices[11] = new VertexPositionColor(
                m_VerticesCoordinates[4],
                r_RightColor);
        }

        /// <summary>
        /// Create the back side vertices
        /// </summary>
        private void createBackVertices()
        {
            m_Vertices[4] = new VertexPositionColor(
                m_VerticesCoordinates[4],
                r_BackColor);
            m_Vertices[5] = new VertexPositionColor(
                m_VerticesCoordinates[5],
                r_BackColor);
            m_Vertices[6] = new VertexPositionColor(
                m_VerticesCoordinates[6],
                r_BackColor);
            m_Vertices[7] = new VertexPositionColor(
                m_VerticesCoordinates[7],
                r_BackColor);
        }

        /// <summary>
        /// Create the front side vertices
        /// </summary>
        private void createFrontVertices()
        {
            m_Vertices[0] = new VertexPositionColor(
                m_VerticesCoordinates[0],
                r_FrontColor);
            m_Vertices[1] = new VertexPositionColor(
                m_VerticesCoordinates[1],
                r_FrontColor);
            m_Vertices[2] = new VertexPositionColor(
                m_VerticesCoordinates[2],
                r_FrontColor);
            m_Vertices[3] = new VertexPositionColor(
                m_VerticesCoordinates[3],
                r_FrontColor);
        }

        /// <summary>
        /// Creates the component indices that the index buffer uses
        /// </summary>
        private void createIndices()
        {
            short[] indices = new short[36];  

            // Front face
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            // Back face
            indices[6] = 4;
            indices[7] = 5;
            indices[8] = 6;
            indices[9] = 4;
            indices[10] = 6;
            indices[11] = 7;

            // Right face
            indices[12] = 8;
            indices[13] = 9;
            indices[14] = 10;
            indices[15] = 8;
            indices[16] = 10;
            indices[17] = 11;

            // Left face
            indices[18] = 12;
            indices[19] = 13;
            indices[20] = 14;
            indices[21] = 12;
            indices[22] = 14;
            indices[23] = 15;

            // Top face
            indices[24] = 16;
            indices[25] = 17;
            indices[26] = 18;
            indices[27] = 16;
            indices[28] = 18;
            indices[29] = 19;

            // Bottom face
            indices[30] = 20;
            indices[31] = 21;
            indices[32] = 22;
            indices[33] = 20;
            indices[34] = 22;
            indices[35] = 23;

            BufferIndices = indices;
        }

        /// <summary>
        /// Initialize the VertexBuffer and IndexBuffer components.
        /// </summary>
        public override void InitBuffers()
        {
            VertexBuffer vBuffer;
            IndexBuffer iBuffer;

            vBuffer = new VertexBuffer(
                this.GraphicsDevice,
                VertexPositionColor.SizeInBytes * m_Vertices.Length,
                BufferUsage.WriteOnly);

            vBuffer.SetData<VertexPositionColor>(m_Vertices, 0, m_Vertices.Length);

            iBuffer = new IndexBuffer(
                this.GraphicsDevice,
                typeof(short),
                this.BufferIndices.Length,
                BufferUsage.WriteOnly);

            iBuffer.SetData<short>(this.BufferIndices);

            this.ComponentVertexBuffer = vBuffer;
            this.ComponentIndexBuffer = iBuffer;
        }
    }
}
