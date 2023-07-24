using System.ComponentModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimulationClient.Entities;
using SimulationClient.Entities.Impl;

namespace SimulationClient;

public class Simulation : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Level _currentLevel;
    private Camera2D Camera;
    private Entity Player;
    
    public static SpriteFont DefaultFont;
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
        _graphics.PreferredBackBufferWidth = GameWidth;
        _graphics.PreferredBackBufferHeight = GameHeight;
        
        _graphics.ApplyChanges();
        
        _currentLevel = new Level();
        
        Player = new Box
        {
            Position = Vector2.Zero,
        };
        
        var fl1 = new Flag
        {
            Position = new Vector2(100, 100)
        };
        var fl2 = new Flag
        {
            Position = new Vector2(200, 200)
        };

        Camera = new Camera2D();

        Camera.Controller = new CameraController
        {
            Entity = Player,
            Camera = Camera
        };
        
        _currentLevel.AddEntity(Player);

        Player.Controller = new KeyboardControl(Player);
        
        _currentLevel.AddEntitiesRange( fl1, fl2 );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _currentLevel.LoadContent(Content);
        DefaultFont = Content.Load<SpriteFont>("DefaultFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _currentLevel.Update(gameTime);
        Camera.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        var viewMatrix = Camera.GetTransform();
        _spriteBatch.Begin();
        _spriteBatch.DrawString(DefaultFont, $"{Camera.Position.X}|{Camera.Position.Y}", Vector2.One, Color.Black);
        _spriteBatch.End();
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
            null, null, null, null, viewMatrix);
        _currentLevel.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}