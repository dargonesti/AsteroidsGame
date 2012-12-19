#region File Description
//-----------------------------------------------------------------------------
// Shapes.cs
//
// Author: JCBDigger
// URL: http://Games.DiscoverThat.co.uk
//-----------------------------------------------------------------------------
// Draw primitive shapes in 3D space
//-----------------------------------------------------------------------------
#endregion

#region JCB Notes
//-----------------------------------------------------------------------------
// See:
// http://msdn.microsoft.com/en-us/library/bb196414.aspx
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Engine
{
    public class Shapes
    {

        #region Initialise

        public struct ShapeList
        {
            public VertexPositionColor[] shape;
        }

        // Display
        private GraphicsDevice graphicsDevice;
        private BasicEffect basicEffect;
        // Built in shapes
        public VertexPositionColor[] scalableRedLoopZ;
        public VertexPositionColor[] scalableRedLoopY;
        public VertexPositionColor[] scalableRedLoopX;
        public VertexPositionColor[] scalableGreenLoopZ;
        public VertexPositionColor[] scalableGreenLoopY;
        public VertexPositionColor[] scalableGreenLoopX;
        public VertexPositionColor[] scalableLine;
        public VertexPositionColor[] scalableCylinder;
        private const int loopHigherQuality = 14;
        private const int loopLowerQuality = 9;
        // Saved shapes
        public List<ShapeList> savedShapes = new List<ShapeList>();


        /// <summary>
        /// Draw primitive shapes in 3D space
        /// </summary>
        /// <param name="gDevice">GraphicsDevice</param>
        public Shapes(GraphicsDevice gDevice)
        {
            graphicsDevice = gDevice;
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;

            // Setup built in shapes
            scalableLine = GetVerticalLine(1.0f, Color.Red);
            scalableRedLoopZ = GetCircle(1.0f, loopHigherQuality, Color.Red, Vector3.Zero, "Z");
            scalableRedLoopY = GetCircle(1.0f, loopHigherQuality, Color.Red, Vector3.Zero, "Y");
            scalableRedLoopX = GetCircle(1.0f, loopHigherQuality, Color.Red, Vector3.Zero, "X");
            scalableGreenLoopZ = GetCircle(1.0f, loopHigherQuality, Color.Green, Vector3.Zero, "Z");
            scalableGreenLoopY = GetCircle(1.0f, loopHigherQuality, Color.Green, Vector3.Zero, "Y");
            scalableGreenLoopX = GetCircle(1.0f, loopHigherQuality, Color.Green, Vector3.Zero, "X");
            scalableCylinder = GetCylinder(1.0f, 1.0f, loopHigherQuality, Color.Red);

        }


        #endregion

        // Stored shapes are drawn ONCE from the EditorScreen.cs Draw(...) method

        /// <summary>
        /// Store a list of points that make up a shape when a line is drawn
        /// between each point.
        /// </summary>
        /// <param name="vertices">Points or corners</param>
        /// <param name="colour">Colour of the lines</param>
        /// <param name="Closed">If true then the last point is connected back to the first</param>
        public void StoreNewShape(ref Vector3[] vertices, Color colour, bool Closed)
        {
            int points = vertices.Length;

            ShapeList vertexList = new ShapeList();
            if (Closed)
            {
                vertexList.shape = new VertexPositionColor[points + 1];
            }
            else
            {
                vertexList.shape = new VertexPositionColor[points];
            }

            for (int i = 0; i < points; i++)
            {
                vertexList.shape[i] = new VertexPositionColor(vertices[i], colour);
            }

            if (Closed)
            {
                vertexList.shape[points] = vertexList.shape[0];
            }

            savedShapes.Add(vertexList);

        }

        public void StoreNewCube(ref Vector3[] vertices, Color colour)
        {
            if (vertices.Length > 7)
            {
                // A cube in one pass needs 16 points
                Vector3[] quad = new Vector3[16];
                quad[0] = vertices[0];
                quad[1] = vertices[1];
                quad[2] = vertices[2];
                quad[3] = vertices[3];
                quad[4] = vertices[0];
                quad[5] = vertices[4];
                quad[6] = vertices[5];
                quad[7] = vertices[1];
                quad[8] = vertices[2];
                quad[9] = vertices[6];
                quad[10] = vertices[5];
                quad[11] = vertices[6];
                quad[12] = vertices[7];
                quad[13] = vertices[3];
                quad[14] = vertices[7];
                quad[15] = vertices[4];
                StoreNewShape(ref quad, colour, false);
            }
        }

        public void StoreNewCircle(Vector3 centre, float radius, Color colour, string onPlane)
        {
            ShapeList vertexList = new ShapeList();
            vertexList.shape = GetCircle(radius, loopLowerQuality, colour, centre, onPlane);
            savedShapes.Add(vertexList);
        }

        public void StoreNewSphere(Vector3 centre, float radius, Color colour)
        {
            StoreNewCircle(centre, radius, colour, "X");
            StoreNewCircle(centre, radius, colour, "Y");
            StoreNewCircle(centre, radius, colour, "Z");
        }

        /// <summary>
        /// Delete any existing saved shapes
        /// </summary>
        public void ClearStoredShapes()
        {
            savedShapes.Clear();
        }

        /// <summary>
        /// Return a list of points forming a vertical cylinder with only one edge
        /// </summary>
        /// <param name="radius">Radius</param>
        /// <param name="hight">Height</param>
        /// <param name="sides">The sides of the top and bottom circles
        ///  the more sides the smoother the circles will appear</param>
        /// <param name="colour">The colour e.g. Color.White or Color.Red</param>
        public VertexPositionColor[] GetCylinder(float radius, float height, int sides, Color colour)
        {
            // 2 circles and a line down one side
            int points = (sides + 1) * 2;
            VertexPositionColor[] pointList = new VertexPositionColor[points];
            VertexPositionColor[] circleList = GetCircle(1.0f, sides, Color.Red, Vector3.Zero, "Y");

            for (int i = 0; i < circleList.Length; i++)
            {
                // Add the top and bottom circles at the same time
                pointList[i] = circleList[i];
                pointList[i + circleList.Length] = circleList[i];
                pointList[i + circleList.Length].Position.Y += height;
            }
            // Make sure the circle is complete the last point is the same as the first
            return pointList;
        }

        /// <summary>
        /// Return a list of points forming a circle.
        /// onPlane, X = Vertical, Y = Horizontal, Z = the other
        /// </summary>
        /// <param name="sides">The more sides the smoother the circle will appear</param>
        /// <param name="colour">The colour e.g. Color.White or Color.Red</param>
        public VertexPositionColor[] GetCircle(float radius, int sides, Color colour, Vector3 centre, string onPlane)
        {
            int points = sides + 1;
            VertexPositionColor[] pointList = new VertexPositionColor[points];

            // This function creates a circle so the maxArc is always 2PI (360 degrees)
            float maxArc = 2 * (float)Math.PI;   // in Radians
            float fStep = maxArc / (float)sides;

            float fAngle = 0;
            for (int i = 0; i < sides; i++)
            {
                if (onPlane == "X")
                {
                    // X plane Vertical forward
                    pointList[i] = new VertexPositionColor(
                        new Vector3(centre.X,
                            ((float)Math.Sin(fAngle) * radius) + centre.Y,
                            ((float)Math.Cos(fAngle) * radius) + centre.Z),
                            colour);
                }
                else if (onPlane == "Y")
                {
                    // Y plane Horizontal
                    pointList[i] = new VertexPositionColor(
                        new Vector3(((float)Math.Sin(fAngle) * radius) + centre.X,
                            centre.Y,
                            ((float)Math.Cos(fAngle) * radius) + centre.Z),
                            colour);
                }
                else
                {
                    // Z plane Vertical side
                    pointList[i] = new VertexPositionColor(
                        new Vector3(((float)Math.Sin(fAngle) * radius) + centre.X,
                            ((float)Math.Cos(fAngle) * radius) + centre.Y,
                            centre.Z),
                            colour);
                }
                // Move the point round by a segment of the circle
                fAngle += fStep;
            }
            // Make sure the circle is complete the last point is the same as the first
            pointList[sides] = pointList[0];
            return pointList;
        }

        /// <summary>
        /// Return two points forming a vertical line
        /// </summary>
        /// <param name="length">Length</param>
        /// <param name="colour">The colour e.g. Color.White or Color.Red</param>
        public VertexPositionColor[] GetVerticalLine(float length, Color colour)
        {
            VertexPositionColor[] pointList = new VertexPositionColor[2];

            pointList[0] = new VertexPositionColor(Vector3.Zero, colour);
            pointList[1] = new VertexPositionColor(new Vector3(0.0f, length, 0.0f), colour);

            return pointList;
        }

        /// <summary>
        /// Change the colour of an existing list of type VertexPositionColor
        /// </summary>
        /// <param name="pointList">Array of points</param>
        /// <param name="colour">The colour e.g. Color.White or Color.Red</param>
        public void ChangeColour(ref VertexPositionColor[] pointList, Color colour)
        {
            for (int i = 0; i < pointList.Length; i++)
            {
                pointList[i].Color = colour;
            }
        }

        #region Draw

        /// <summary>
        /// Draw the built in cylinder to any scale.  
        /// This is two circles drawn with flat sides and a line between them
        /// </summary>
        /// <param name="vLocation">Centre of the circle at one end</param>
        /// <param name="fRadius">Radius of the loop at each end</param>
        /// <param name="fHeight">Height of the cylinder</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        public void DrawScalableCylinder(Vector3 vLocation, float fRadius, float fHeight, Matrix mtxView, Matrix mtxProjection)
        {
            Vector3 scale = new Vector3(fRadius, fHeight, fRadius);
            Matrix mtxWorld = Matrix.Identity * Matrix.CreateScale(scale) * Matrix.CreateTranslation(vLocation);
            basicEffect.World = mtxWorld;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref scalableCylinder);
        }

        /// <summary>
        /// Draw the built in horizontal loop to any scale.  
        /// This is a circle drawn with flat sides.
        /// </summary>
        /// <param name="vLocation">Centre of the shape</param>
        /// <param name="fRadius">Radius of the loop</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        public void DrawHorizontalLoop(Vector3 vLocation, float fRadius, Matrix mtxView, Matrix mtxProjection)
        {
            Matrix mtxWorld = Matrix.Identity * Matrix.CreateScale(fRadius) * Matrix.CreateTranslation(vLocation);
            basicEffect.World = mtxWorld;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref scalableRedLoopY);
        }

        /// <summary>
        /// Draw the built in horizontal and vertical loops to any scale.  
        /// This is to represent a sphere.
        /// </summary>
        /// <param name="vLocation">Centre of the shape</param>
        /// <param name="fRadius">Radius of the loops</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        public void DrawRedSphere(Vector3 vLocation, float fRadius, Matrix mtxView, Matrix mtxProjection)
        {
            Matrix mtxWorld = Matrix.Identity * Matrix.CreateScale(fRadius) * Matrix.CreateTranslation(vLocation);
            basicEffect.World = mtxWorld;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref scalableRedLoopZ);
            DrawLines(ref scalableRedLoopY);
            DrawLines(ref scalableRedLoopX);
        }

        public void DrawGreenSphere(Vector3 vLocation, float fRadius, Matrix mtxView, Matrix mtxProjection)
        {
            Matrix mtxWorld = Matrix.Identity * Matrix.CreateScale(fRadius) * Matrix.CreateTranslation(vLocation);
            basicEffect.World = mtxWorld;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref scalableGreenLoopZ);
            DrawLines(ref scalableGreenLoopY);
            DrawLines(ref scalableGreenLoopX);
        }

        /// <summary>
        /// Draw the built in vertical line to any length.
        /// </summary>
        /// <param name="vLocation">Lower point for the line</param>
        /// <param name="fLength">The line length</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        public void DrawVerticalLine(Vector3 vLocation, float fLength, Matrix mtxView, Matrix mtxProjection)
        {
            Matrix mtxWorld = Matrix.Identity * Matrix.CreateScale(fLength) * Matrix.CreateTranslation(vLocation);
            basicEffect.World = mtxWorld;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref scalableLine);
        }

        /// <summary>
        /// Draw all the stored shapes.
        /// </summary>
        /// <param name="mtxView">Camera ViewMatrix</param>
        /// <param name="mtxProjection">Camera ProjectionMatrix</param>
        public void DrawStoredShapes(Matrix mtxView, Matrix mtxProjection)
        {
            for (int i = 0; i < savedShapes.Count; i++)
            {
                DrawShapeAtOrigin(savedShapes[i].shape, mtxView, mtxProjection);
            }

        }

        /// <summary>
        /// Draw a shape like a box made up of corners.  The last corner will be joined to the 
        /// first to close the shape.
        /// </summary>
        /// <param name="vertices">Corner points</param>
        /// <param name="colour">The colour</param>
        /// <param name="mtxView">Camera ViewMatrix</param>
        /// <param name="mtxProjection">Camera ProjectionMatrix</param>
        public void DrawClosedShape(ref Vector3[] vertices, Color colour, Matrix mtxView, Matrix mtxProjection)
        {
            int points = vertices.Length;
            VertexPositionColor[] vertexList = new VertexPositionColor[points + 1];

            for (int i = 0; i < points; i++)
            {
                vertexList[i] = new VertexPositionColor(vertices[i], colour);
            }
            vertexList[points] = vertexList[0];

            DrawShape(ref vertexList, Matrix.Identity, mtxView, mtxProjection);
        }

        /// <summary>
        /// Draw a shape based at the origin
        /// </summary>
        /// <param name="pointList">Array of points to draw lines between</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        public void DrawShapeAtOrigin(VertexPositionColor[] pointList, Matrix mtxView, Matrix mtxProjection)
        {
            basicEffect.World = Matrix.Identity;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref pointList);
        }

        /// <summary>
        /// Draw a single thin line.
        /// </summary>
        /// <param name="from">Start point</param>
        /// <param name="to">Finish point</param>
        /// <param name="colour">Colour</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        public void DrawLine(Vector3 from, Vector3 to, Color colour, Matrix mtxView, Matrix mtxProjection)
        {
            VertexPositionColor[] pointList = new VertexPositionColor[2];
            pointList[0] = new VertexPositionColor(from, colour);
            pointList[1] = new VertexPositionColor(to, colour);
            DrawShapeAtOrigin(pointList, mtxView, mtxProjection);
        }

        public void DrawTriangle(Vector3 a, Vector3 b, Vector3 c, Color colour, Matrix mtxView, Matrix mtxProjection)
        {
            VertexPositionColor[] pointList = new VertexPositionColor[4];
            pointList[0] = new VertexPositionColor(a, colour);
            pointList[1] = new VertexPositionColor(b, colour);
            pointList[2] = new VertexPositionColor(c, colour);
            // Closed shape
            pointList[3] = pointList[0];
            DrawShapeAtOrigin(pointList, mtxView, mtxProjection);
        }

        public void DrawDot(Vector3 centre, Color colour, Matrix mtxView, Matrix mtxProjection)
        {
            float radius = 0.1f;
            VertexPositionColor[] pointList = GetCircle(radius, 4, colour, centre, "X");
            DrawShapeAtOrigin(pointList, mtxView, mtxProjection);
            pointList = GetCircle(radius, 4, colour, centre, "Y");
            DrawShapeAtOrigin(pointList, mtxView, mtxProjection);
        }

        /// <summary>
        /// Draw a shape
        /// </summary>
        /// <param name="pointList">Array of points to draw lines between</param>
        /// <param name="vLocation">Origin point for the shape</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        public void DrawShape(ref VertexPositionColor[] pointList, Vector3 vLocation, Matrix mtxView, Matrix mtxProjection)
        {
            Matrix mtxWorld = Matrix.CreateTranslation(vLocation);
            basicEffect.World = mtxWorld;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref pointList);
        }

        /// <summary>
        /// Draw a transformed shape.
        /// </summary>
        /// <param name="pointList">Array of points to draw lines between</param>
        /// <param name="mtxTransform">Origin, rotation and scale of the shape</param>
        /// <param name="mtxView">View matrix</param>
        /// <param name="mtxProjection">Projection matrix</param>
        /// <example>
        ///     mtxTransform = Matrix.CreateTranslation(70, 20, 15) * 
        ///         Matrix.CreateRotationX(MathHelper.ToRadians(45)) *
        ///         Matrix.CreateRotationY(MathHelper.ToRadians(30)) *
        ///         Matrix.CreateScale(0.5f)
        /// </example>
        public void DrawShape(ref VertexPositionColor[] pointList, Matrix mtxTransform, Matrix mtxView, Matrix mtxProjection)
        {
            basicEffect.World = mtxTransform;
            basicEffect.View = mtxView;
            basicEffect.Projection = mtxProjection;

            DrawLines(ref pointList);
        }

        // Draw the shape from the line list which is an
        // array of points to draw lines between
        private void DrawLines(ref VertexPositionColor[] verticesList)
        {
            if (verticesList.Length > 1)
            {
                basicEffect.CurrentTechnique.Passes[0].Apply();
                graphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.LineStrip,
                    verticesList,
                    0,   // which vertex to start at
                    (verticesList.Length - 1)    // number of primitives to draw
                );
            }

        }

        #endregion

    }
}
