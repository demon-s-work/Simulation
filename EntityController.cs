using Microsoft.Xna.Framework;
using Simulation.Entities;
namespace Simulation
{
    public abstract class EntityController
    {
        public Entity Entity;
        public virtual void Update(GameTime gt)
        {
            
        }
    }
}