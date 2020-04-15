using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Riddlersoft.Graphics.Text
{
    public class TextChar
    {
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0f;
        public float RotationVelocity = 0f;
        public float LifeTime = 0f;
        public float Scale = 1f;
        public Color Colour = Color.White;
        public Texture2D Sprite;
        public string character = string.Empty;
        public float Opacity = 1f;
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 Center;

        public void SetUp(SpriteFont font)
        {
            Center = font.MeasureString(character) * .5f;
            Position += Center;
        }

        public TextChar(char c, Vector2 pos)
        {
            character = c.ToString();
            Position = pos;
        }

        public TextChar(Texture2D _sprite, Vector2 pos, float scale = 1)
        {
            Sprite = _sprite;
            Position = pos;
            Scale = scale;
        }

        public void Update(float dt)
        {
            Position += Velocity;
            Rotation += RotationVelocity;
            LifeTime += dt;
        }

    }
}
