using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OverratedEngine.GUI
{
    /// <summary>
    /// A simple control which shows text centered in its bounds
    /// </summary>
    public class Label : TextControl
    {

        /// <summary>
        /// Draws the control, called once per frame
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw with</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            TextColor = new Color(TextColor.R, TextColor.G, TextColor.B, 1f);
            DrawCenteredText(spriteBatch, Font, GetAbsoluteRect(), Text, TextColor);
        }
    }
}
