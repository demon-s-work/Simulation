using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Simulation.Client
{
    public sealed class ResourceManager
    {
        private ResourceManager() {}
        private static ResourceManager instance = null;
        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceManager();
                }
                return instance;
            }
        }

        public void LoadContent(ContentManager cm)
        {
            Player = cm.Load<Texture2D>("player");
            Flag = cm.Load<Texture2D>("flag");
            DefaultFont = cm.Load<SpriteFont>("DefaultFont");
        }

        public SpriteFont DefaultFont;
        public Texture2D Player { get; set; }
        public Texture2D Flag { get; set; }
    }
}