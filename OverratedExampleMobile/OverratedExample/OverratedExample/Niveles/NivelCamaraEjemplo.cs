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
    public class NivelCamaraEjemplo : Level
    {
        public NivelCamaraEjemplo(Game game)
            : base(game)
        {
        }

        Pelota pelota;
        List<MovableGameElement> seguidores;

        public override void Initialize()
        {
            base.Initialize();

            // Añado el fondo
            DrawableGameElement fondo = new DrawableGameElement(game, this);
            fondo.Initialize(Vector2.Zero);
            fondo.Scale = 2;
            fondo.LoadContent("Fondo");
            AddGameElement(fondo, camera.Layers[0]);

            // Añado la pelota
            pelota = new Pelota(game, this);
            pelota.Initialize(new Vector2(400, 240));
            pelota.LoadContent();
            AddGameElement(pelota, camera.Layers[0]);

            // Añado los seguidores
            seguidores = new List<MovableGameElement>();
            seguidores.Add(pelota);
            for (int i = 0; i < 10; i++)
            {
                Pelota seguidor = new Pelota(game, this);
                seguidor.Initialize(new Vector2(500 + 10 * i, 240));
                seguidor.LoadContent();
                seguidor.isLider = false;
                AddGameElement(seguidor, camera.Layers[0]);
                seguidores.Add(seguidor);
            }

            // Añado el medidor de FPS
            AddGameElement(new FPSCounter(game, this, "Fuentes/Fuente"), camera.Layers[0]);

            //Límites de la cámara
            camera.Zoom = 2;
            camera.Limits = new Rectangle(0, 0, 1600, 960);

            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.Pinch;

            LoadContainer("Menus/MenuAStar");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            camera.LookAt(pelota.Position);

            for (int i = 0; i < 10; i++)
            {
                if (seguidores[i].Speed != Vector2.Zero)
                    seguidores[i].Rotation = OverratedHelper.AngleBetweenVectors2D(seguidores[i].Speed, Vector2.UnitX);
                if (i != 0)
                {
                    seguidores[i].Follow(seguidores[i - 1], 60, 5);
                    seguidores[i].Separation(seguidores, 50, 3);
                }
            }

            // Cámara
            foreach (GestureSample sample in getGestures())
            {
                if (sample.GestureType == GestureType.Pinch)
                {
                    Vector2 oldPosition1 = sample.Position - sample.Delta;
                    Vector2 oldPosition2 = sample.Position2 - sample.Delta2;
                    float newDistance = Vector2.Distance(sample.Position, sample.Position2);
                    float oldDistance = Vector2.Distance(oldPosition1, oldPosition2);
                    float scaleFactor = oldDistance / newDistance;
                    camera.Zoom *= scaleFactor;
                    camera.Limits = new Rectangle(0, 0, 1600, 960);
                }
            }


            Button b = (rootContainer.FindControlByName("BotonSiguiente") as Button);
            if (b.Clicked)
            {
                OEngine.GetInstance().ChangeLevel(new NivelParallaxEjemplo(game));
            }
        }

        public override void OnBackButtonPressed()
        {
            game.Exit();
        }
    }
}
