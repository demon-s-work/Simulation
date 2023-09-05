using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Simulation.Client.Screens
{
    public class Screen : IGameObject
    {
        public List<IGameObject> UI;
        
        public void Update(GameTime gt)
        {
        }
        
        public void Draw(SpriteBatch sp)
        {
        }
    }
}