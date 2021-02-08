using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OverratedEngine.Base
{
    public class DrawableGameElement : GameElement
    {
        private float animation_counter;
        private int image_index;

        /// <summary>
        /// Amount of horizontal strips
        /// </summary>
        private int horizontal_strips;

        /// <summary>
        /// Amount of vertical strips
        /// </summary>
        private int vertical_strips;

        /// <summary>
        /// Animation speed, in milliseconds
        /// </summary>
        public int AnimationSpeed;

        /// <summary>
        /// Texture of the Drawable Game Element
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// Texture region to be drawn
        /// </summary>
        public Rectangle TextureRegion;        

        /// <summary>
        /// Color of the Drawable Game Element
        /// </summary>
        public Color ElementColor;

        /// <summary>
        /// Rotation of the Drawable Game Element
        /// </summary>
        public float Rotation;

        /// <summary>
        /// Origin of the Drawable Game Element
        /// </summary>
        public Vector2 Origin;

        /// <summary>
        /// Texture effect of the Drawable Game Element
        /// </summary>
        public SpriteEffects Effect;

        /// <summary>
        /// Depth of the Drawable Game Element
        /// </summary>
        public float Depth;

        /// <summary>
        /// Size of the Drawable Game Element
        /// </summary>
        public float Scale;

        /// <summary>
        /// Alpha of the Drawable Game Element
        /// </summary>
        public float Alpha;

        /// <summary>
        /// Indicates if the Drawable Game Element is visible
        /// </summary>
        public bool Visible;

        public DrawableGameElement(Game game, Level level)
            : base(game, level)
        {
        }

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);
            ElementColor = Color.White;
            Alpha = 1f;
            Rotation = 0f;
            Origin = Vector2.Zero;
            Effect = SpriteEffects.None;
            Depth = 0f;
            Scale = 1f;
            Visible = true;
            animation_counter = 0;
            image_index = 0;
            AnimationSpeed = 50;
        }

        /// <summary>
        /// Loads the texture of the Drawable Game Element
        /// </summary>
        /// <param name="texture_path">Texture path</param>
        public void LoadContent(string texture_path)
        {
            LoadContent(texture_path, 1, 1);
        }

        /// <summary>
        /// Updates the Drawable Game Element
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if ((vertical_strips != 1) && (horizontal_strips != 1))
            {
                animation_counter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (animation_counter > AnimationSpeed)
                {
                    animation_counter = 0;
                    image_index = (image_index + 1) % (vertical_strips * horizontal_strips);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Loads the texture of the Drawable Game Element as an animation
        /// </summary>
        /// <param name="texture_path">Texture path</param>
        /// <param name="horizontal_strips">Amount of horizontal strips of the animation</param>
        /// <param name="vertical_strips">Amount of vertical strips of the animation</param>
        public void LoadContent(string texture_path, int horizontal_strips, int vertical_strips)
        {
            texture = game.Content.Load<Texture2D>(texture_path);
            TextureRegion = new Rectangle(0, 0, texture.Width, texture.Height);
            this.horizontal_strips = horizontal_strips;
            this.vertical_strips = vertical_strips;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(
                    texture, 
                    Position, 
                    new Rectangle(
                        TextureRegion.X + ((TextureRegion.Width / horizontal_strips) * (image_index % horizontal_strips)), 
                        TextureRegion.Y + ((TextureRegion.Height / vertical_strips) * (int)Math.Floor((float)image_index / horizontal_strips)), 
                        TextureRegion.Width / horizontal_strips, 
                        TextureRegion.Height / vertical_strips), 
                    ElementColor * Alpha, 
                    Rotation, 
                    Origin, 
                    Scale, 
                    Effect, 
                    Depth);

            base.Draw(spriteBatch);
        }
    }
}
