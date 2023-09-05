using Microsoft.Xna.Framework;
using Simulation.Client.Entities;
namespace Simulation.Client
{
    public abstract class EntityController
    {
        public Entity Entity;
        public virtual void Update(GameTime gt)
        {
            
        }
    }
}