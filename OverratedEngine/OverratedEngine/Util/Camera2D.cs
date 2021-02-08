using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OverratedEngine.Util
{
    public class Camera2D
    {
        Viewport viewport;

        private Rectangle? _limits;
        public Rectangle? Limits
        {
            get { return _limits; }
            set
            {
                if (value != null)
                {
                    // Assign limit but make sure it's always bigger than the viewport
                    _limits = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,
                        Width = System.Math.Max(viewport.Width, value.Value.Width),
                        Height = System.Math.Max(viewport.Height, value.Value.Height)
                    };

                    // Validate camera position with new limit
                    Position = Position;
                }
                else
                {
                    _limits = null;
                }
            }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;

                // If there's a limit set and the camera is not transformed clamp position to limits
                if (Limits != null && Zoom == 1.0f && Rotation == 0.0f)
                {
                    position.X = MathHelper.Clamp(position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - viewport.Width);
                    position.Y = MathHelper.Clamp(position.Y, Limits.Value.Y, Limits.Value.Y + Limits.Value.Height - viewport.Height);
                }
            }
        }
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }
        public List<Layer> Layers { get; set; }

        public Camera2D(Viewport viewport)
        {
            this.viewport = viewport;
            Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            Zoom = 1.0f;
            Layers = new List<Layer>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in Layers)
                layer.Draw(spriteBatch);
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            // To add parallax, simply multiply it by the position
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                // The next line has a catch. See note below.
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1) *
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void Move(Vector2 displacement, bool respectRotation)
        {
            if (respectRotation)
            {
                displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-Rotation));
            }

            Position += displacement;
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        }

        public Layer getDefaultLayer()
        {
            return Layers[0];
        }
    }
}
