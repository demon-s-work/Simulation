using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simulation.Client.Entities;
using Simulation.Client.Entities.Impl;
using Simulation.Client.UI;

namespace Simulation.Client;

public class Simulation : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Level _currentLevel;
    private Button bt = null;
    
    private Camera2D Camera;
    private ResourceManager rm;
    
    public static int GameWidth = 1366;
    public static int GameHeight = 768;
    
    public Simulation()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _currentLevel = new Level();
        Camera = new Camera2D();
        _graphics.PreferredBackBufferWidth = GameWidth;
        _graphics.PreferredBackBufferHeight = GameHeight;
        _graphics.ApplyChanges();
        rm = ResourceManager.Instance;
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        rm.LoadContent(Content);

        bt = new Button()
        {
            Position = new Vector2(100,100),
            Sprite = rm.Flag
        };

        bt.OnPressed += (sender, args) => {
            bt = null;
        };
        
        var pl = new Player()
        {
            Position = Vector2.Zero,
            Name = "Envy",
            Sprite = rm.Player
        };
        pl.Controller = new KeyboardControl(pl);
        
        Camera.Controller = new CameraController
        {
            Entity = pl,
            Camera = Camera
        };
        _currentLevel.AddEntity(pl);

        _currentLevel.AddEntity(new Flag
        {
            Name = "Flag",
            Sprite = rm.Flag,
            Position = new Vector2(10,10),
        });
        _currentLevel.AddEntity(new Flag
        {
            Name = "Flag1",
            Sprite = rm.Flag,
            Position = new Vector2(30,30),
        });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _currentLevel.Update(gameTime);
        bt?.Update(gameTime);
        Camera.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        var viewMatrix = Camera.GetTransform();
        _spriteBatch.Begin();
        _spriteBatch.DrawString(rm.DefaultFont, $"{Camera.Position.X}|{Camera.Position.Y}", Vector2.One, Color.Black);
        
        bt?.Draw(_spriteBatch);
        
        _spriteBatch.End();
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
            null, null, null, null, viewMatrix);
        _currentLevel?.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}