using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2=Microsoft.Xna.Framework.Vector2;
namespace Simulation.Client.UI
{
    public class Button
    {
        public Vector2 Position;
        public Texture2D Sprite;
        public event EventHandler OnPressed;

        public void Update(GameTime gt)
        {
            if (MouseEntered() && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                OnPressed?.Invoke(this, EventArgs.Empty);
            }
        }
        private bool MouseEntered()
        {
            var mousePos = Mouse.GetState().Position;
            
            if (mousePos.X < Position.X + Sprite.Width &&
                 mousePos.X > Position.X &&
                mousePos.Y < Position.Y + Sprite.Height &&
                mousePos.Y > Position.Y)
            {
                return true;
            }
            return false;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}