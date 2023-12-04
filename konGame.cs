using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using konUI;
using konSceneMan;

namespace _4koneko
{
    public class konGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D note;
        private Texture2D lane;
        private Texture2D laneActive;
        private SpriteFont sFont;
        private SceneManager sceneMan;
        
        public static UI ui;

        public konGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.ApplyChanges();
            Window.AllowUserResizing = false; 
            Window.Position = new Point(
            (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - _graphics.PreferredBackBufferWidth) / 2,
            (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - _graphics.PreferredBackBufferHeight) / 2
        );
        }

        protected override void Initialize()
        {
            base.Initialize();
            ui = new();
            sceneMan = new();
            sceneMan.SetScene(Scene.Main);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            note = Content.Load<Texture2D>("note");
            lane = Content.Load<Texture2D>("lane");
            laneActive = Content.Load<Texture2D>("laneActive");
            sFont = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            sceneMan.Update();
            ui.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            ui.Draw(_spriteBatch, sFont);
            base.Draw(gameTime);
        }
    }
}
