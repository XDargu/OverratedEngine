using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using OverratedEngine.Base;
using Microsoft.Xna.Framework.Graphics;

namespace OverratedEngine.Util
{
    public class Layer
    {
        private readonly Camera2D camera;
        public Vector2 Parallax { get; set; }
        public List<GameElement> Elements { get; private set; }
        public string Tag;
        private BlendState blendState;
        private SamplerState samplerState;
        private DepthStencilState depthPencilState;
        private RasterizerState rasterizerState;
        private Effect effect;

        public Layer(Camera2D camera)
        {
            this.camera = camera;
            Parallax = Vector2.One;
            Elements = new List<GameElement>();
            this.blendState = null;
            this.samplerState = null;
            this.depthPencilState = null;
            this.rasterizerState = null;
            this.effect = null;
        }

        public Layer(Camera2D camera, BlendState blendState, SamplerState samplerState, DepthStencilState depthPencilState, RasterizerState rasterizerState, Effect effect)
        {
            this.camera = camera;
            Parallax = Vector2.One;
            Elements = new List<GameElement>();
            this.blendState = blendState;
            this.samplerState = samplerState;
            this.depthPencilState = depthPencilState;
            this.rasterizerState = rasterizerState;
            this.effect = effect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, blendState, samplerState, depthPencilState, rasterizerState, effect, camera.GetViewMatrix(Parallax));
            foreach (GameElement sprite in Elements)
                sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, camera.GetViewMatrix(Parallax));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(camera.GetViewMatrix(Parallax)));
        }
    }
}
