using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OverratedEngine.Util
{
    public class Boton
    {
        #region Enum de estados

        /// <summary>
        /// Estado del botón
        /// </summary>
        public enum EstadoBoton
        {
            SinPulsar = 0,
            Pulsando = 1,
            Click = 2
        }

        #endregion

        #region Atributos públicos

        /// <summary>
        /// Estado del botón
        /// </summary>
        public EstadoBoton Estado { get { return estado; } }

        /// <summary>
        /// Posición del botón
        /// </summary>
        public Vector2 Posicion { get { return new Vector2(area_boton.X, area_boton.Y); } set { area_boton.X = (int)value.X; area_boton.Y = (int)value.Y; } }

        /// <summary>
        /// Flag de vibración
        /// </summary>
        public bool Vibracion { get; set; }

        /// <summary>
        /// Flag de emitir sonido
        /// </summary>
        public bool Sonido { get; set; }

        /// <summary>
        /// Texto del botón
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Offset del texto del botón
        /// </summary>
        public Vector2 OffsetTexto { get; set; }

        /// <summary>
        /// Color del texto cuando el botón no está siendo pulsado
        /// </summary>
        public Color ColorNormal { get; set; }

        /// <summary>
        /// Color del texto cuando el botón está pulsado
        /// </summary>
        public Color ColorPulsado { get; set; }

        /// <summary>
        /// Alpha del botón
        /// </summary>
        public float AlphaBoton { get; set; }

        /// <summary>
        /// Alpha del texto
        /// </summary>
        public float AlphaTexto { get; set; }

        /// <summary>
        /// Flag de actividad del botón
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Flag de visibilidad del botón
        /// </summary>
        public bool Visible { get; set; }

        #endregion

        #region Atributos privados

        /// <summary>
        /// Efecto de sonido
        /// </summary>
        private SoundEffect click;

        /// <summary>
        /// Fuente del botón
        /// </summary>
        private SpriteFont fuente;

        /// <summary>
        /// Estado del botón
        /// </summary>
        private EstadoBoton estado;

        /// <summary>
        /// Textura del botón sin pulsar
        /// </summary>
        private Texture2D imagen_normal;

        /// <summary>
        /// Textura del botón siendo pulsado
        /// </summary>
        private Texture2D imagen_pulsado;

        /// <summary>
        /// Área que ocupa el botón
        /// </summary>
        private Rectangle area_boton;

        #endregion

        #region Constructor

        /// <summary>
        /// Crea un nuevo botón
        /// </summary>
        /// <param name="area">Área que ocupará el botón</param>
        public Boton(Rectangle area)
        {
            this.area_boton = area;
            this.Texto = "";
            this.OffsetTexto = Vector2.Zero;
            this.ColorNormal = Color.White;
            this.ColorPulsado = Color.White;
            this.Activo = true;
            this.Visible = true;
            this.Vibracion = false;
            this.Sonido = false;
            this.AlphaBoton = 1;
            this.AlphaTexto = 1;
            this.estado = EstadoBoton.SinPulsar;
        }

        #endregion

        #region Carga de contenido

        /// <summary>
        /// Carga las imágenes sin pulsar y pulsado del botón
        /// </summary>
        /// <param name="game">Clase game</param>
        /// <param name="texturaNormal">Ruta de la imágen del botón sin pulsar</param>
        /// <param name="texturaPulsado">Ruta de la imágen del botón al ser pulsado</param>
        public void LoadContent(Game game, string texturaNormal, string texturaPulsado)
        {
            this.imagen_normal = game.Content.Load<Texture2D>(texturaNormal);
            this.imagen_pulsado = game.Content.Load<Texture2D>(texturaPulsado);
        }

        /// <summary>
        /// Carga la imágen del botón
        /// </summary>
        /// <param name="game">Clase game</param>
        /// <param name="textura">Ruta de la imágen del botón</param>
        public void LoadContent(Game game, string textura)
        {
            LoadContent(game, textura, textura);
        }

        #endregion

        #region Actualización

        /// <summary>
        /// Actualiza el botón
        /// </summary>
        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            bool pulsado = false;
            if (state.LeftButton == ButtonState.Pressed)
            {
                if (area_boton.Contains((int)state.X, (int)state.Y))
                    pulsado = true;                    
            }

            if (pulsado)
            {
                switch (estado)
                {
                    case EstadoBoton.Click:
                        estado = EstadoBoton.Pulsando;
                        break;
                    case EstadoBoton.SinPulsar:
                        estado = EstadoBoton.Pulsando;
                        break;
                }
            }
            else
            {
                switch (estado)
                {
                    case EstadoBoton.Click:
                        estado = EstadoBoton.SinPulsar;
                        break;
                    case EstadoBoton.Pulsando:
                        estado = EstadoBoton.Click;
                        break;
                }
            }
                
        }

        #endregion

        #region Dibujado

        /// <summary>
        /// Dibuja el botón
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (estado == EstadoBoton.Pulsando)
                {
                    spriteBatch.Draw(imagen_pulsado, area_boton, Color.White * AlphaBoton);
                    if (!Texto.Equals(""))
                        spriteBatch.DrawString(fuente, Texto, OffsetTexto + new Vector2(area_boton.Center.X, area_boton.Center.Y), ColorPulsado * AlphaTexto, 0, fuente.MeasureString(Texto) / 2, 1.0f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(imagen_normal, area_boton, Color.White * AlphaBoton);
                    if (!Texto.Equals(""))
                        spriteBatch.DrawString(fuente, Texto, OffsetTexto + new Vector2(area_boton.Center.X, area_boton.Center.Y), ColorNormal * AlphaTexto, 0, fuente.MeasureString(Texto) / 2, 1.0f, SpriteEffects.None, 0);
                }
            }
        }

        #endregion

        #region Métodos de carga

        /// <summary>
        /// Carga el sonido del botón
        /// </summary>
        /// <param name="game">Clase game</param>
        /// <param name="sonido">Ruta del sonido del botón</param>
        public void setSonido(Game game, string sonido)
        {
            click = game.Content.Load<SoundEffect>(sonido);
        }

        /// <summary>
        /// Carga la fuente del botón
        /// </summary>
        /// <param name="game">Clase game</param>
        /// <param name="fuente">Ruta de la fuente del botón</param>
        public void setFuente(Game game, string fuente)
        {
            this.fuente = game.Content.Load<SpriteFont>(fuente);
        }

        #endregion

        #region Otros métodos

        public void setArea(Rectangle area)
        {
            area_boton = area;
        }

        public Rectangle getArea()
        {
            return area_boton;
        }

        #endregion
    }
}
