using Microsoft.Xna.Framework;
namespace Simulation.Client
{
    public class CameraController : EntityController
    {
        public Camera2D Camera { get; set; }
        public override void Update(GameTime gt)
        {
            if (Entity is not null)
            {
                var cX = -Entity.Position.X + Simulation.GameWidth / 2;
                var cY = -Entity.Position.Y + Simulation.GameHeight / 2;
                Camera.Position = new Vector2(cX, cY);
            }
        }
    }
}