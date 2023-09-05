using Microsoft.Xna.Framework;
using Simulation.Client.Entities;
namespace Simulation.Client
{
    public class Camera2D
    {
        public Camera2D()
        {
            Zoom = 1;
            Position = Vector2.Zero;
            Rotation = 0;
            Position = Vector2.Zero;
        }

        public Vector2 Position { get; set; } = new Vector2();
        public float Zoom { get; set; }
        public float Rotation { get; set; }
        public EntityController Controller { get; set; }
        
        public Matrix GetTransform()
        {
            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));

            return translationMatrix * rotationMatrix * scaleMatrix;
        }

        public void Update(GameTime gt)
        {
            Controller?.Update(gt);
        }
    }
}