using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HeroAnimationTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Hero _myHero1, _myHero2, _myHero3;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _myHero1 = new Hero(10, 10, 5, 100, "Aaron1", Content.Load<Texture2D>("GambrianDragon"), Content.Load<SpriteFont>("GameFont"));
            _myHero2 = new Hero(110, 110, 5, 100, "Aaron2", Content.Load<Texture2D>("GambrianDragon"), Content.Load<SpriteFont>("GameFont"));
            _myHero3 = new Hero(210, 210, 5, 100, "Aaron3", Content.Load<Texture2D>("GambrianDragon"), Content.Load<SpriteFont>("GameFont"));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _myHero1.Update();
            _myHero2.Update();
            _myHero3.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _myHero1.Draw(_spriteBatch);
            _myHero2.Draw(_spriteBatch);
            _myHero3.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
