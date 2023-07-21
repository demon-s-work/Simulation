using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Simulation.Entities
{
    public abstract class Entity
    {
        public virtual string Name { get; }
        public Texture2D Sprite { get; private set; }
        public Vector2 Position { get; set; }

        public void LoadContent(ContentManager cm)
        {
            Sprite = cm.Load<Texture2D>(Name);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Sprite, Position, Color.White);
        }
    }
}