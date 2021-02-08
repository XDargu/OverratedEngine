using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OverratedEngine.Base;
using Microsoft.Xna.Framework;

namespace OverratedExamplePC.Personajes
{
    public class Pelota : MovableGameElement
    {
        public bool isLider;

        public Pelota(Game game, Level level)
            : base(game, level)
        {
            isLider = true;
        }

        public override void LoadContent()
        {
            base.LoadContent("Ball", 2, 2);
            Origin = new Vector2(texture.Width / (2 * 2), texture.Height / (2 * 2));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (isLider)
            {                
                if (level.GameMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    Speed = Vector2.Normalize(new Vector2(level.GameMouseState.X, level.GameMouseState.Y) - Position);
                else
                    Speed = Vector2.Zero;
            }
        }
    }
}
