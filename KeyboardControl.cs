using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Simulation.Entities;
namespace Simulation
{
    public class KeyboardControl : EntityController
    {
        public KeyboardControl(Entity entity)
        {
            Entity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var resultV = Vector2.Zero;
            
            if (state.IsKeyDown(Keys.Right))
            {
                resultV.X = Entity.Speed;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                resultV.X = -Entity.Speed;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                resultV.Y = -Entity.Speed;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                resultV.Y = Entity.Speed;
            }
            
            Entity.Position = Vector2.Add(Entity.Position, resultV);
        }
    }
}