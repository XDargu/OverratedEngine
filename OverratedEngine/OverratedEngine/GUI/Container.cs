using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OverratedEngine.GUI
{
    /// <summary>
    /// Holds a set of controls in positions relative to this container control
    /// </summary>
    public class Container : Control
    {
        #region Fields

        private List<IControl> controls = new List<IControl>();
        private List<IControl> markedForRemove = new List<IControl>();
        private List<IControl> markedForAdd = new List<IControl>();

        #endregion

        #region Properties

        /// <summary>
        /// The controls this container holds
        /// </summary>
        public List<IControl> Controls
        {
            get { return controls; }
        }

        /// <summary>
        /// Add a control to the container immediately
        /// </summary>
        /// <param name="_control">The control to add</param>
        public void AddControl(IControl _control)
        {
            controls.Add(_control);
            _control.Parent = this;
        }

        /// <summary>
        /// Removes a control from the container immediately
        /// </summary>
        /// <param name="_control">The control to remove</param>
        public void RemoveControl(IControl _control)
        {
            controls.Remove(_control);
        }

        /// <summary>
        /// Marks a control to be added during the next container update
        /// </summary>
        /// <param name="_control">The control to add</param>
        public void MarkForAdd(IControl _control)
        {
            markedForAdd.Add(_control);
        }

        /// <summary>
        /// Marks a control to be removed during the next container update
        /// </summary>
        /// <param name="_control">The control to remove</param>
        public void MarkForRemove(IControl _control)
        {
            markedForRemove.Add(_control);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the container and its child controls
        /// </summary>
        public override void Initialize()
        {
            foreach (Control control in controls)
            {
                control.Parent = this;
                control.Initialize();
            }
        }

        /// <summary>
        /// Loads the container and its child controls
        /// </summary>
        public override void LoadContent(GraphicsDevice _graphics, ContentManager _content)
        {
            base.LoadContent(_graphics, _content);

            foreach (Control control in controls)
            {
                control.LoadContent(_graphics, _content);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the container and its visible child controls
        /// </summary>
        public override void Update(GameTime gameTime, KeyboardState keyboardState, KeyboardState keyboardStateOld, MouseState mouseState, MouseState mouseStateOld)
        {
            base.Update(gameTime, keyboardState, keyboardStateOld, mouseState, mouseStateOld);

            if (Visible)
            {
                foreach (Control ctrl in controls)
                {
                    if (ctrl.Visible)
                    {
                        ctrl.Update(gameTime, keyboardState, keyboardStateOld, mouseState, mouseStateOld);
                    }
                }
            }

            foreach (IControl ctrl in markedForRemove)
            {
                RemoveControl(ctrl);
            }
            markedForRemove.Clear();

            foreach (IControl ctrl in markedForAdd)
            {
                AddControl(ctrl);
            }
            markedForAdd.Clear();

        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the container and its visible child controls
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                foreach (Control control in controls)
                {
                    if (control.Visible)
                    {
                        control.Draw(spriteBatch);
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a child control by the control's name
        /// </summary>
        /// <param name="_name">The name of the control to find</param>
        /// <returns>The named control or null if none is found</returns>
        public IControl FindControlByName(string _name)
        {
            foreach (IControl ctrl in controls)
            {
                if (ctrl.Name == _name)
                {
                    return ctrl;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a child control by the control's name or its childs
        /// </summary>
        /// <param name="_name">The name of the control to find</param>
        /// <returns>The named control or null if none is found</returns>
        public IControl FindControlByNameRecursive(string _name)
        {
            foreach (IControl ctrl in controls)
            {
                if (ctrl.Name == _name)
                {
                    return ctrl;
                }

                if (ctrl.GetType() == typeof(Container))
                {
                    IControl control = ((Container)ctrl).FindControlByNameRecursive(_name);
                    if (control != null)
                        return control;
                }
            }

            return null;
        }

        #endregion
    }
}
