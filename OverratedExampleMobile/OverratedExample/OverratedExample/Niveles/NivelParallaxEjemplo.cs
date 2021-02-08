using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OverratedEngine.Base;
using OverratedExample.Personajes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OverratedEngine.Algorithms;
using OverratedEngine.Graphics;
using OverratedEngine.Algorithms.Heuristics;
using OverratedEngine.Util;
using OverratedEngine.GUI;
using OverratedEngine.GUI.Transitions;
using Microsoft.Xna.Framework.Input.Touch;

namespace OverratedExample.Niveles
{
    public class NivelParallaxEjemplo : Level
    {
        public NivelParallaxEjemplo(Game game)
            : base(game)
        {
        }

        MovableGameElement mario;
        float tiempoSinUsarCamara = 0;
        float tiempoSinCambiarDestino = 0;
        Vector2 destino;

        public override void Initialize()
        {
            base.Initialize();

            // En lugar de las capas por defecto, voy a usar nuevas
            camera.Layers.RemoveAt(0);
            camera.Layers.Add(new Layer(camera) { Parallax = new Vector2(0.5f, 0) });
            camera.Layers.Add(new Layer(camera) { Parallax = new Vector2(1f, 0) });
            camera.Layers.Add(new Layer(camera) { Parallax = new Vector2(2f, 0) });
            camera.Layers.Add(new Layer(camera) { Parallax = new Vector2(0, 0) });

            // Añado el fondo de primer nivel
            DrawableGameElement fondo = new DrawableGameElement(game, this);
            fondo.Initialize(Vector2.Zero);
            fondo.LoadContent("FondoParallax2");
            AddGameElement(fondo, camera.Layers[0]);

            // Añado el fondo de segundo nivel
            fondo = new DrawableGameElement(game, this);
            fondo.Initialize(Vector2.Zero);
            fondo.LoadContent("FondoParallax1");
            AddGameElement(fondo, camera.Layers[1]);

            // Añado el fondo de tercer nivel
            fondo = new DrawableGameElement(game, this);
            fondo.Initialize(Vector2.Zero);
            fondo.LoadContent("FondoParallax0");
            AddGameElement(fondo, camera.Layers[2]);

            // Añado a mario en el segundo nivel
            mario = new MovableGameElement(game, this);
            mario.Initialize(new Vector2(50, 480 - 27 * 3));
            mario.LoadContent("Mario", 2, 1);
            mario.Scale = 3;
            AddGameElement(mario, camera.Layers[1]);

            // Destino
            destino = new Vector2(camera.Position.X + OverratedHelper.Random.Next(800), 480 - 27 * 3);

            // Añado el medidor de FPS
            AddGameElement(new FPSCounter(game, this, "Fuentes/Fuente"), camera.Layers[3]);

            //Límites de la cámara
            camera.Limits = new Rectangle(0, 0, 1260, 480);

            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.HorizontalDrag;

            LoadContainer("Menus/MenuAStar");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            tiempoSinUsarCamara += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tiempoSinCambiarDestino += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Mario
            // Animación
            if (mario.Speed.Length() < 0.5f)
            {
                mario.Image_index = 1;
                mario.AnimationSpeed = 0;
            }
            else
                mario.AnimationSpeed = 200;

            // Dirección
            if (mario.Speed.X < 0)
                mario.Effect = SpriteEffects.FlipHorizontally;
            else
                mario.Effect = SpriteEffects.None;

            // Movimiento
            if (tiempoSinUsarCamara < 5)
                destino = new Vector2(camera.Position.X + 400, 480 - 27 * 3);
            else
            {
                if (tiempoSinCambiarDestino > 3)
                {
                    tiempoSinCambiarDestino = 0;
                    destino = new Vector2(camera.Position.X + OverratedHelper.Random.Next(800), 480 - 27 * 3);
                }
            }
            if (Vector2.Distance(mario.Position, destino) < 6)
                mario.Arrive(destino, 3);
            else
                mario.Seek(destino, 5);

            // Cámara
            foreach (GestureSample sample in getGestures())
            {
                if (sample.GestureType == GestureType.HorizontalDrag)
                {
                    camera.Move(sample.Delta, true);
                    tiempoSinUsarCamara = 0;
                }
            }


            Button b = (rootContainer.FindControlByName("BotonSiguiente") as Button);
            if (b.Clicked)
            {
                
            }
        }

        public override void OnBackButtonPressed()
        {
            game.Exit();
        }
    }
}
