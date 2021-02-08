using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OverratedEngine.Graphics
{
    public class CommonGraphics
    {
        Texture2D texture;

        public CommonGraphics(Game game)
        {
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }

        public void DrawRectangle(SpriteBatch spritebatch, Rectangle rectangle, float size, Color color)
        {
            DrawLine(spritebatch, size, color, new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y));
            DrawLine(spritebatch, size, color, new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X, rectangle.Y + rectangle.Height));
            DrawLine(spritebatch, size, color, new Vector2(rectangle.X + rectangle.Width, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));
            DrawLine(spritebatch, size, color, new Vector2(rectangle.X, rectangle.Y + rectangle.Height), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));
        }

        public void DrawLine(SpriteBatch spritebatch, float size, Color color, Vector2 start, Vector2 end)
        {
            float angulo = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            float length = Vector2.Distance(start, end);

            Vector2 inicioReal = new Vector2(start.X + (float)Math.Sin(angulo) * size / 2, start.Y - (float)Math.Cos(angulo) * size / 2);

            spritebatch.Draw(texture, inicioReal, null, color,
                       angulo, Vector2.Zero, new Vector2(length, size),
                       SpriteEffects.None, 1);
        }

        public void DrawArrow(SpriteBatch spritebatch, float size, Color color, Vector2 start, Vector2 end)
        {
            float angulo = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            float length = Vector2.Distance(start, end);

            Vector2 inicioReal = new Vector2(start.X + (float)Math.Sin(angulo) * size / 2, start.Y - (float)Math.Cos(angulo) * size / 2);

            spritebatch.Draw(texture, inicioReal, null, color,
                       angulo, Vector2.Zero, new Vector2(length, size),
                       SpriteEffects.None, 1);
            
            //Dibujar la punta de flecha
            Vector2 nuevoInicio;
            Vector2 nuevoFin;

            nuevoInicio = end;
            nuevoFin = new Vector2(end.X + (float)Math.Cos(angulo) * 2, end.Y + (float)Math.Sin(angulo) * 2);

            int iteraciones = (int)size / 2 + 10;
            for (int i = 0; i < iteraciones; i++)
            {
                length = Vector2.Distance(nuevoInicio, nuevoFin);
                inicioReal = new Vector2(nuevoInicio.X + (float)Math.Sin(angulo) * (size + 10 - 2 * i) / 2, nuevoInicio.Y - (float)Math.Cos(angulo) * (size + 10 - 2 * i) / 2);
                spritebatch.Draw(texture, inicioReal, null, color,
                            angulo, Vector2.Zero, new Vector2(length, size + 10 - 2 * i),
                            SpriteEffects.None, 1);

                nuevoInicio = nuevoFin;
                nuevoFin = new Vector2(nuevoInicio.X + (float)Math.Cos(angulo) * 2, nuevoInicio.Y + (float)Math.Sin(angulo) * 2);
            }            
        }
    }
}
