using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simulation.Client
{
    public interface IGameObject
    {
        public void Update(GameTime gt);
        public void Draw(SpriteBatch sp);
    }
}