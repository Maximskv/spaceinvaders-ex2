using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DreidelGame.ObjectModel
{    
    /// <summary>
    /// Represents the dreidel handle
    /// </summary>
    public class Box : CompositeGameComponent
    {
        private const float k_MinX = -.5f;
        private const float k_MaxX = .5f;
        private const float k_MinY = 3f;
        private const float k_MaxY = 7;
        private const float k_MinZ = -.5f;
        private const float k_MaxZ = .5f;
        private const int k_VerticesNum = 8;
        private readonly Color r_SideColor = Color.Brown;
        private const int k_TrianglesNum = 12;

        private Vector3 m_LeftBottomBack = new Vector3(k_MinX, k_MinY, k_MinZ);
        private Vector3 m_LeftBottomFront = new Vector3(k_MinX, k_MinY, k_MaxZ);
        private Vector3 m_LeftTopBack = new Vector3(k_MinX, k_MaxY, k_MinZ);
        private Vector3 m_LeftTopFront = new Vector3(k_MinX, k_MaxY, k_MaxZ);
        private Vector3 m_RightBottomBack = new Vector3(k_MaxX, k_MinY, k_MinZ);
        private Vector3 m_RightBottomFront = new Vector3(k_MaxX, k_MinY, k_MaxZ);
        private Vector3 m_RightTopBack = new Vector3(k_MaxX, k_MaxY, k_MinZ);
        private Vector3 m_RightTopFront = new Vector3(k_MaxX, k_MaxY, k_MaxZ);
        private VertexPositionColor[] m_Vertices;        

        /// <summary>
        /// Gets the number of triangles the box has
        /// </summary>
        public override int     TriangleNum
        {
            get { return k_TrianglesNum; }
        }

        /// <summary>
        /// CTOR. creates a new instance
        /// </summary>
        /// <param name="i_Game"></param>
        public Box(Game i_Game)
            : base(i_Game)
        {
        }

        /// <summary>
        /// Initialize the component vertices and buffers
        /// </summary>
        protected override void     LoadContent()
        {
            base.LoadContent();

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
        private void    createVertices()
        {
            m_Vertices = new VertexPositionColor[k_VerticesNum];

            m_Vertices[0] = new VertexPositionColor(
                m_LeftTopFront, 
                r_SideColor);

            m_Vertices[0] = new VertexPositionColor(
                m_LeftTopFront,
                r_SideColor);

            m_Vertices[1] = new VertexPositionColor(
                m_RightTopFront,
                r_SideColor);

            m_Vertices[2] = new VertexPositionColor(
                m_RightBottomFront,
                r_SideColor);

            m_Vertices[3] = new VertexPositionColor(
                m_LeftBottomFront,
                r_SideColor);

            m_Vertices[4] = new VertexPositionColor(
                m_RightTopBack,
                r_SideColor);

            m_Vertices[5] = new VertexPositionColor(
                m_RightBottomBack,
                r_SideColor);

            m_Vertices[6] = new VertexPositionColor(
                m_LeftTopBack,
                r_SideColor);

            m_Vertices[7] = new VertexPositionColor(
                m_LeftBottomBack,
                r_SideColor);       

            this.VerticesNum = k_VerticesNum;
        }

        /// <summary>
        /// Creates the component indices that the index buffer uses
        /// </summary>
        private void    createIndices()
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
            indices[6] = 6;
            indices[7] = 7;
            indices[8] = 5;
            indices[9] = 6;
            indices[10] = 5;
            indices[11] = 4;

            // Top face
            indices[12] = 0;
            indices[13] = 6;
            indices[14] = 4;
            indices[15] = 0;
            indices[16] = 4;
            indices[17] = 1;

            // Bottom face
            indices[18] = 2;
            indices[19] = 5;
            indices[20] = 7;
            indices[21] = 2;
            indices[22] = 7;
            indices[23] = 3;

            // Right face
            indices[24] = 1;
            indices[25] = 4;
            indices[26] = 5;
            indices[27] = 1;
            indices[28] = 5;
            indices[29] = 2;

            // Left face
            indices[30] = 3;
            indices[31] = 7;
            indices[32] = 6;
            indices[33] = 3;
            indices[34] = 6;
            indices[35] = 0;         

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
