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
        public Level Location { get; set; }
        public float Speed { get; set; } = 5;

        public void LoadContent(ContentManager cm)
        {
            Sprite = cm.Load<Texture2D>(Name);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Sprite, Position, Color.White);
        }

        public virtual void Update(GameTime gt)
        {
            
        }
    }
}