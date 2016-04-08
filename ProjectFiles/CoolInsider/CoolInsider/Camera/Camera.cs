using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace TreasureHunter
{
    public class Camera
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Camera Class. 
         * 
         */

        Matrix view;
        Matrix centerTranslation;
        Matrix zoomMatrix;
        Vector2 target;
        Vector2 position;
        Rectangle levelBounds;

        float zoom;
        float zoomTarget;

        /// <summary>
        /// Constructor. Sets the position of the camera and the boundaries it will operate in.
        /// </summary>
        /// <param name="Position">Starting position of the camera</param>
        /// <param name="Boundary">Camera will stay within the defined bounds</param>
        public Camera(Vector2 Position, Rectangle Boundary){
            position = Position;
            zoom = zoomTarget = 1;
            centerTranslation = Matrix.CreateTranslation(
                TreasureHunter.graphics.PreferredBackBufferWidth * .5f  - Avatar.Instance().kinetics.boundingBox.Width/2f,
                TreasureHunter.graphics.PreferredBackBufferHeight * .5f - Avatar.Instance().kinetics.boundingBox.Height/2f, 0);
            levelBounds = new Rectangle(Boundary.Left   + (int)centerTranslation.Translation.X,
                                        Boundary.Top    + (int)centerTranslation.Translation.Y,
                                        Boundary.Right  - (int)centerTranslation.Translation.X*2,
                                        Boundary.Bottom - (int)centerTranslation.Translation.Y*2);
            
            zoomMatrix = Matrix.CreateScale(zoom, zoom, 1);
        }

        /// <summary>
        /// Sets the target of the camera, that the camera will actively approach
        /// </summary>
        /// <param name="Target">Target position</param>
        public void SetTarget(Vector2 Target){
            target.X = Target.X;
            target.Y = Target.Y;
        }

        /// <summary>
        /// Updates the camera to move toward the targets.
        /// </summary>
        public void Update() {
            position += Movement.Approach(position, target, .1f);

            if (zoom != zoomTarget)
            {
                zoom += Movement.Approach(zoom, zoomTarget, .1f);
                if (Math.Abs(zoom - zoomTarget) < .0001) zoom = zoomTarget;
                zoomMatrix = Matrix.CreateScale(zoom, zoom, 1);
            }

            view = Matrix.CreateTranslation(new Vector3(-position.X,-position.Y,0))*zoomMatrix*centerTranslation;
        }

        /// <summary>
        /// Sets the zoom value.
        /// </summary>
        /// <param name="Value">Value between 0 and 5)</param>
        public void Zoom(float Value){
            zoomTarget = MathHelper.Clamp(Value,1,5);
        }

        /// <summary>
        /// Returns the view/translation matrix
        /// </summary>
        /// <returns>View/Translation maxtrix</returns>
        public Matrix View() {
            return view;
        }

        /// <summary>
        /// Returns the top side y coordinate
        /// </summary>
        public float TOP {
            get { return -view.Translation.Y; }
        }

        /// <summary>
        /// Returns the bottom side y coordinate
        /// </summary>
        public float Bottom
        {
            get { return -view.Translation.Y+TreasureHunter.graphics.PreferredBackBufferHeight; }
        }

        /// <summary>
        /// Returns the left side X coordinate
        /// </summary>
        public float Left
        {
            get { return -view.Translation.X; }
        }

        /// <summary>
        /// Returns the right side X coordinate
        /// </summary>
        public float Right
        {
            get { return -view.Translation.X+TreasureHunter.graphics.PreferredBackBufferWidth; }
        }
    }
}
