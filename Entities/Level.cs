using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Simulation.Entities
{
    public class Level
    {
        public List<Entity> Entities = new List<Entity>();
        
        public void LoadContent(ContentManager cm)
        {
            Entities.ForEach(e => e.LoadContent(cm));
        }

        public void Draw(SpriteBatch sb)
        {
            Entities.ForEach(e => e.Draw(sb));
        }
    }
}