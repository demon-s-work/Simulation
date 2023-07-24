using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Simulation.Entities
{
    public class Level
    {
        private List<Entity> _entities = new List<Entity>();
        public ImmutableList<Entity> Entities => _entities.ToImmutableList();

        public void LoadContent(ContentManager cm)
        {
            _entities.ForEach(e => e.LoadContent(cm));
        }

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