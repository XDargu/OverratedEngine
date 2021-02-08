using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OverratedEngine.Base
{
    public class MovableGameElement : DrawableGameElement
    {
        public Vector2 Speed;

        public MovableGameElement(Game game, Level level)
            : base(game, level)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;
            base.Update(gameTime);
        }
    }
}
