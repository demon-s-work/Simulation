using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simulation.Client.Entities
{
    public class Level
    {
        private List<Entity> _entities = new List<Entity>();
        
        public void Draw(SpriteBatch sb)
        {
            _entities.ForEach(e => e.Draw(sb));
        }

        public void Update(GameTime gt)
        {
            _entities.ForEach(e => e.Update(gt));
        }

        public void AddEntity(Entity e)
        {
            e.Location = this;
            _entities.Add(e);
        }

        public void AddEntitiesRange(params Entity[] e)
        {
            foreach (var entity in e)
            {
                entity.Location = this;
            }
            
            _entities.AddRange(e);
        }

        public void RemoveEntity(Entity e)
        {
            _entities.Remove(e);
        }
    }
}