using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OverratedEngine.Util;
using Microsoft.Xna.Framework.Input;
using OverratedEngine.GUI;

namespace OverratedEngine.Base
{
    public class Level
    {
        protected Game game;

        /// <summary>
        /// Update List of Game Elements
        /// </summary>
        protected List<GameElement> gameList;

        /// <summary>
        /// Level camera
        /// </summary>
        public Camera2D camera;

        /// <summary>
        /// Root GUI Container
        /// </summary>
        protected Container rootContainer;

        /// <summary>
        /// Engine
        /// </summary>
        protected OverratedEngine engine;

        // Controls
        protected MouseState mouseState;
        protected KeyboardState keyboardState;
        protected MouseState mouseStateOld;
        protected KeyboardState keyboardStateOld;

        /// <summary>
        /// MouseState of this update
        /// </summary>
        public MouseState GameMouseState { get { return mouseState; } }

        /// <summary>
        /// MouseState of the previous update
        /// </summary>
        public MouseState GameMouseStateOld { get { return mouseStateOld; } }

        public Level(Game game, OverratedEngine engine)
        {
            this.game = game;
            this.engine = engine;
        }

        public virtual void Initialize()
        {
            gameList = new List<GameElement>();
            camera = new Camera2D(game.GraphicsDevice.Viewport);
            camera.Layers.Add(new Layer(camera) { Tag = "default" });
            rootContainer = new Container();
            rootContainer.Initialize();

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            mouseStateOld = Mouse.GetState();
            keyboardStateOld = Keyboard.GetState();
        }

        public virtual void LoadContent()
        {
            rootContainer.LoadContent(game.GraphicsDevice, game.Content);
        }

        public virtual void Update(GameTime gameTime) 
        {
            //Back button
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                OnBackButtonPressed();
            }

            // Updates every element
            for (int i = 0; i < gameList.Count; i++)
            {
                if (gameList[i].Active)
                    gameList[i].Update(gameTime);
            }

            // Update controls
            mouseStateOld = mouseState;
            keyboardStateOld = keyboardState;
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            rootContainer.Update(gameTime, keyboardState, keyboardStateOld, mouseState, mouseStateOld);
        }

        /// <summary>
        /// Returns if an screen area is being touched
        /// </summary>
        /// <param name="area">Screen area</param>
        /// <returns>True if the area is being touched</returns>
        public bool IsTouchingArea(Rectangle area)
        {
            bool rtr = false;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (area.Contains((int)mouseState.X, (int)mouseState.Y))
                {
                    rtr = true;
                }
            }
            return rtr;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draws the Game Elements
            camera.Draw(spriteBatch);
        }

        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {
            // Draws the GUI
            spriteBatch.Begin();
            rootContainer.Draw(spriteBatch);
            spriteBatch.End();
        }

        public virtual void OnBackButtonPressed()
        {

        }

        /// <summary>
        /// Removes a Game Element from Update list and camera layer (if exists)
        /// </summary>
        /// <param name="element">Element to be removed</param>
        public void RemoveGameElement(GameElement element)
        {
            // Remove from Update list
            for (int i = 0; i < gameList.Count; i++)
            {
                if (gameList[i] == element)
                {
                    gameList.RemoveAt(i);
                    break;
                }
            }

            // Remove from Camera layer
            for (int i = 0; i < camera.Layers.Count; i++)
            {
                for (int e = 0; e < camera.Layers[i].Elements.Count; e++)
                {
                    if (camera.Layers[i].Elements[e] == element)
                    {
                        camera.Layers[i].Elements.RemoveAt(e);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a Game Element to the Update list
        /// </summary>
        /// <param name="element">Element to be added</param>
        public void AddGameElement(GameElement element)
        {
            gameList.Add(element);
        }

        /// <summary>
        /// Adds a Game Element to the Update list and to a camera layer
        /// </summary>
        /// <param name="element">Element to be added</param>
        /// <param name="layer">Camera layer</param>
        public void AddGameElement(GameElement element, Layer layer)
        {
            gameList.Add(element);
            layer.Elements.Add(element);
        }

        /// <summary>
        ///  Loads an XML container
        /// </summary>
        /// <param name="path">Xml asset tag to load</param>
        public void LoadContainer(string path)
        {
            Container loadedContainer = game.Content.Load<Container>(path);
            loadedContainer.Initialize();
            loadedContainer.LoadContent(game.GraphicsDevice, game.Content);
            rootContainer = loadedContainer;
        }

        protected bool ButtonClicked(String name)
        {
            Button b = (rootContainer.FindControlByNameRecursive(name) as Button);
            if (b == null)
                return false;
            return b.Clicked;
        }

        protected bool KeyTyped(Keys key)
        {
            if ((keyboardStateOld.IsKeyDown(key)) && (keyboardState.IsKeyUp(key)))
                return true;
            return false;
        }
    }
}
