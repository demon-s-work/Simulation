using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Simulation.Entities;
namespace Simulation
{
    public class PlayerControlService
    {
        public Entity PlayerEntity { get; init; }
        
        public PlayerControlService(Entity playerEntity)
        {
            PlayerEntity = playerEntity;
        }

        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var resultV = Vector2.Zero;
            
            if (state.IsKeyDown(Keys.Right))
            {
                resultV.X = 10f;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                resultV.X = -10f;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                resultV.Y = -10f;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                resultV.Y = 10f;
            }
            
            PlayerEntity.Position = Vector2.Add(PlayerEntity.Position, resultV);
        }
    }
}