using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OverratedEngine.Base
{
    public class GameElement
    {
        /// <summary>
        /// Game class
        /// </summary>
        protected Game game;

        /// <summary>
        /// Level of the Game Element
        /// </summary>
        protected Level level;

        /// <summary>
        /// Game Element position
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Game Element tag
        /// </summary>
        public string Tag;

        /// <summary>
        /// If false, the Game Element won't update
        /// </summary>
        public bool Active;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameElement(Game game, Level level)
        {
            this.game = game;
            this.level = level;
        }

        /// <summary>
        /// Initializes the Game Element in a certain position
        /// </summary>
        /// <param name="position">Position of the Game Element</param>
        public virtual void Initialize(Vector2 position)
        {
            this.Position = position;
            this.Active = true;
        }

        public virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

    }
}
