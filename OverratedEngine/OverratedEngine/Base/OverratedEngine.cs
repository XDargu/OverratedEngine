using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OverratedEngine.Base
{
    public class OverratedEngine
    {
        Game game;
        Level level;

        public OverratedEngine(Game game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            level.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
            level.DrawGUI(spriteBatch);
        }

        public void ChangeLevel(Level level)
        {
            this.level = level;
            this.level.Initialize();
            this.level.LoadContent();
        }
    }
}
