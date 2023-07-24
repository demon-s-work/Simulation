using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simulation.Entities;
using Simulation.Entities.Impl;

namespace Simulation;

public class Simulation : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Level _currentLevel;
    private PlayerControlService _inputService;

    public Simulation()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _currentLevel = new Level();
        var player = new Box
        {
            Position = Vector2.Zero
        };
        var fl1 = new Flag
        {
            Position = new Vector2(100, 100)
        };
        var fl2 = new Flag
        {
            Position = new Vector2(200, 200)
        };

        _currentLevel.AddEntity(player);
        _currentLevel.AddEntitiesRange( fl1, fl2 );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _currentLevel.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _currentLevel.Update(gameTime);
    
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _currentLevel.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}