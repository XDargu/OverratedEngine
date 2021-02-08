using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OverratedEngine.Base;
using Microsoft.Xna.Framework;
using OverratedEngine.GUI;
using OverratedEngine.GUI.Transitions;

namespace OverratedExamplePC.Niveles
{
    public class NivelMenuEjemplo : Level
    {
        public NivelMenuEjemplo(Game game, OverratedEngine.Base.OverratedEngine engine)
            : base(game, engine)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            // Cargamos el menú
            LoadContainer("Menus/MenuEjemplo");

            (rootContainer.FindControlByName("BotonSiguiente") as Button).Tapped += new EventHandler(botonSiguientePulsado);
            (rootContainer.FindControlByName("BotonMover") as Button).Tapped += new EventHandler(botonMoverPulsado);
        }

        public override void OnBackButtonPressed()
        {
            game.Exit();
            base.OnBackButtonPressed();
        }

        private void botonSiguientePulsado(object sender, EventArgs e)
        {
            engine.ChangeLevel(new NivelAStarEjemplo(game, engine));
        }

        private void botonMoverPulsado(object sender, EventArgs e)
        {
            if (((Button)sender).Left < 300)
            {
                ((Button)sender).ApplyTransition(Transition.CreateFlyOut(((Button)sender), new Point(570, 350), (570 - ((Button)sender).Left) / 200));
            }
            else
            {
                ((Button)sender).ApplyTransition(Transition.CreateFlyOut(((Button)sender), new Point(30, 350), (((Button)sender).Left - 20) / 200));
            }
        }
    }
}
