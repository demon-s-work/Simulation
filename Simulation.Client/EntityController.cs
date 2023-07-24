using Microsoft.Xna.Framework;
using SimulationClient.Entities;
namespace SimulationClient
{
    public abstract class EntityController
    {
        public Entity Entity;
        public virtual void Update(GameTime gt)
        {
            
        }
    }
}