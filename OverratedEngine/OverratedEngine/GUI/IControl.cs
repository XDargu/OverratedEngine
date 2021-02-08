using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OverratedEngine.GUI
{
    public interface IControl
    {
        int Width { get; set; }
        int Height { get; set; }
        int Top { get; set; }
        int Left { get; set; }
        int Bottom { get; }
        int Right { get; }
        string Name { get; set; }
        Color Hue { get; set; }

        IControl Parent { get; set; }

        Point GetAbsoluteTopLeft();

        void Update(GameTime gameTime, KeyboardState keyboardState, KeyboardState keyboardStateOld, MouseState mouseState, MouseState mouseStateOld);
    }
}
