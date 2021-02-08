using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OverratedEngine.Base;
using OverratedExamplePC.Personajes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OverratedEngine.Algorithms;
using OverratedEngine.Graphics;
using OverratedEngine.Algorithms.Heuristics;
using OverratedEngine.Util;
using OverratedEngine.GUI;
using OverratedEngine.GUI.Transitions;

namespace OverratedExamplePC.Niveles
{
    public class NivelAStarEjemplo : Level
    {
        public NivelAStarEjemplo(Game game, OverratedEngine.Base.OverratedEngine engine)
            : base(game, engine)
        {
        }

        MovableGameElement seguidor;

        List<Node> Mundo;
        Texture2D blanco;

        Vector2 destino;
        List<Vector2> camino;
        AStar aStar;
        bool noPath = false;

        public override void Initialize()
        {
            base.Initialize();

            Random r = new Random();

            // Creamos el mundo (lista de nodos)
            int nodos = 100;
            Mundo = new List<Node>();
            for (int i = 0; i < nodos; i++)
            {
                Mundo.Add(new Node(new Vector2(r.Next(800), r.Next(480))));
            }

            foreach (Node nodo in Mundo)
            {
                foreach (Node nodo2 in Mundo)
                {
                    if (nodo != nodo2)
                    {
                        if ((Vector2.Distance(nodo.Position, nodo2.Position) < 100) && (nodo.Neighbors.Count < 6) && (nodo2.Neighbors.Count < 6))
                        {
                            nodo2.Neighbors.Add(nodo);
                            nodo.Neighbors.Add(nodo2);
                        }
                    }
                }
            }

            //Camino
            aStar = new AStar(Mundo, new Manhattan());
            camino = aStar.FindVectorPath(Mundo[0], Mundo[1]);
            destino = Mundo[1].Position;

            // Añado el fondo
            DrawableGameElement fondo = new DrawableGameElement(game, this);
            fondo.Initialize(Vector2.Zero);
            fondo.LoadContent("Fondo");
            AddGameElement(fondo, camera.Layers[0]);

            // Añado el seguidor
            seguidor = new MovableGameElement(game, this);
            seguidor.Initialize(new Vector2(400, 240));
            seguidor.LoadContent("Seguidor");
            seguidor.Origin = new Vector2(seguidor.TextureRegion.Width / 2, seguidor.TextureRegion.Height / 2);
            AddGameElement(seguidor, camera.Layers[0]);

            foreach (Node node in Mundo)
            {
                DrawableGameElement nodo = new DrawableGameElement(game, this);
                nodo.Initialize(new Vector2(node.Position.X, node.Position.Y));
                nodo.LoadContent("Nodo");
                AddGameElement(nodo, camera.Layers[0]);
            }

            pd = new CommonGraphics(game);

            // Añado el medidor de FPS
            //AddGameElement(new FPSCounter(game, this, "Fuentes/Fuente"), camera.Layers[0]);

            LoadContainer("Menus/MenuAStar");
        }

        public override void LoadContent()
        {
            base.LoadContent();
            blanco = game.Content.Load<Texture2D>("Blanco");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (seguidor.Speed != Vector2.Zero)
                seguidor.Rotation = OverratedHelper.AngleBetweenVectors2D(seguidor.Speed, -Vector2.UnitY);

            /*if (!seguidor.FollowPath(camino, 5))
                if (noPath)
                    seguidor.Speed = Vector2.Zero;
                else
                    seguidor.Arrive(destino, 5);*/

            // Calcular un nuevo camino si pulsamos en la pantalla
            /*if (touches.Count > 0)
            {
                destino = touches[0].Position;

                // Buscamos el nodo más cercano al seguidor
                Node inicio = Mundo[0];
                Node fin = Mundo[1];
                foreach (Node nodo in Mundo)
                    if (Vector2.Distance(seguidor.Position, nodo.Position) < Vector2.Distance(seguidor.Position, inicio.Position))
                        inicio = nodo;

                // Buscamos el nodo más cercano al destino
                foreach (Node nodo in Mundo)
                    if (Vector2.Distance(destino, nodo.Position) < Vector2.Distance(destino, fin.Position))
                        fin = nodo;

                // Recalculamos el camino
                camino = aStar.FindVectorPath(inicio, fin);
                noPath = (camino.Count == 0);

                // Le decimos al seguidor que estamos empezando un nuevo camino
                seguidor.ResetFollowPath();
            }*/

            Button b = (rootContainer.FindControlByName("BotonSiguiente") as Button);
            if (b.Clicked)
            {
                engine.ChangeLevel(new NivelCamaraEjemplo(game, engine));
            }
        }

        public override void OnBackButtonPressed()
        {
            game.Exit();
        }

        CommonGraphics pd;
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Begin();
            foreach (Node node in Mundo)
            {
                foreach (Node node2 in node.Neighbors)
                    pd.DrawLine(spriteBatch, 1, Color.Black, camera.Layers[0].WorldToScreen(node.Position), camera.Layers[0].WorldToScreen(node2.Position));
            }
            if (camino.Count > 0)
            {
                spriteBatch.Draw(blanco, camera.Layers[0].WorldToScreen(camino[0]), null, Color.Red, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
                spriteBatch.Draw(blanco, camera.Layers[0].WorldToScreen(camino[camino.Count - 1]), null, Color.Yellow, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
            }
            spriteBatch.Draw(blanco, camera.Layers[0].WorldToScreen(destino), null, Color.Lime, 0, Vector2.Zero, 10, SpriteEffects.None, 0);
            spriteBatch.End();            
        }
    }
}
