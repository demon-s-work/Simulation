using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simulation.Client.Entities
{
    public abstract class Entity
    {
        public string Name { get; set; } = String.Empty;
        public Texture2D Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Level Location { get; set; }
        public EntityController Controller { get; set; }
        public float Speed { get; set; } = 10;

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Sprite, Position, Color.White);
            sb.DrawString(ResourceManager.Instance.DefaultFont, Name, new Vector2(Position.X, Position.Y - 24), Color.Black);
        }

        public void Update(GameTime gt)
        {
            Controller?.Update(gt);
        }
    }
}