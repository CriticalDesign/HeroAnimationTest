using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace HeroAnimationTest
{
    internal class Hero
    {

        enum HeroState
        {
            Idle,
            Move,
            Nap,
            Fly1,
            Fly2,
            Jump,
            Attack
        }


        private Texture2D _heroSpriteStrip;
        private int _heroHealth;
        private string _heroName;
        private float _heroSpeed, _heroScale;
        private float _heroX, _heroY;
        private int _animationDelay, _animationTimer, _animationIndex;
        private HeroState _animationAction;
        private int _numRows, _numCols;
        private int _frameWidth , _frameHeight;
        private SpriteEffects _heroLook;
        private SpriteFont _heroFont;
        private Vector2 _heroCenter;

        public Hero(float heroX, float heroY, float heroSpeed, int heroHealth, string heroName, Texture2D heroSpriteStrip, SpriteFont heroFont)
        {
            _heroX = heroX;
            _heroY = heroY;
            _heroSpeed = heroSpeed;
            _heroHealth = heroHealth;
            _heroName = heroName;
            _heroSpriteStrip = heroSpriteStrip;
            _heroFont = heroFont;

            Random rng = new Random();
            _animationIndex = rng.Next(0,11);

            _numRows = 7;
            _numCols = 12;

            _animationAction = (HeroState) rng.Next(0, 7);

            _frameWidth = heroSpriteStrip.Width / _numCols;
            _frameHeight = heroSpriteStrip.Height / _numRows;

            _animationDelay = 6;
            _animationTimer = 0;

            _heroLook = SpriteEffects.None;

            _heroScale = 3f;

            _heroCenter = GetCenter();
        }

        private int GetHealth() {  return _heroHealth; }
        private string GetName() { return _heroName; }

        private Vector2 GetCenter()
        {
            if (_heroLook == SpriteEffects.None)
                return new Vector2(25, 18);
            else
                return new Vector2(_frameWidth - 25, 18);
        }

        public void Update()
        {
            _animationAction = HeroState.Idle;

            KeyboardState currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                _heroY -= _heroSpeed;
                _animationAction = HeroState.Move;
            }
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                _heroX -= _heroSpeed;
                _animationAction = HeroState.Move;
                _heroLook = SpriteEffects.FlipHorizontally;
                _heroCenter = GetCenter();
            }
            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                _heroY += _heroSpeed;
                _animationAction = HeroState.Move;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                _heroX += _heroSpeed;
                _animationAction = HeroState.Move;
                _heroLook = SpriteEffects.None;
                _heroCenter = GetCenter();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Space))
                _animationAction = HeroState.Attack;

            if (currentKeyboardState.IsKeyDown(Keys.N))
                _animationAction = HeroState.Nap;

            if (currentKeyboardState.IsKeyDown(Keys.F))
                _animationAction = HeroState.Fly1;

            if (currentKeyboardState.IsKeyDown(Keys.J))
                _animationAction = HeroState.Jump;


            _animationTimer++;

            if (_animationTimer >= _animationDelay)
            {
                //reset timer
                _animationTimer = 0;

                //increase index and check if out of bounds
                _animationIndex++;

                if (_animationAction == HeroState.Idle && _animationIndex >= 4)
                    _animationIndex = 0;
                if (_animationAction == HeroState.Move && _animationIndex >= 4)
                    _animationIndex = 0;
                if (_animationAction == HeroState.Nap && _animationIndex >= 6)
                    _animationIndex = 0;
                if (_animationAction == HeroState.Fly1 && _animationIndex >= 3)
                    _animationIndex = 0;
                if (_animationAction == HeroState.Fly2 && _animationIndex >= 3)
                    _animationIndex = 0;
                if (_animationAction == HeroState.Jump && _animationIndex >= 6)
                    _animationIndex = 0;
                if (_animationAction == HeroState.Attack && _animationIndex >= 12)
                    _animationIndex = 0;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(
                _heroSpriteStrip,               //this is the sprite strip
                new Vector2(_heroX, _heroY),    //this is the sprite location on the scene
                new Rectangle(_animationIndex * _frameWidth, (int)_animationAction * _frameHeight, _frameWidth, _frameHeight),     //subsection of the texture to draw 
                Color.White,                    //color overlay
                0f,                             //rotation
                _heroCenter,               //center of rotation
                _heroScale,                             //scale
                _heroLook,             //effect
                0                               //depth
                );
            spriteBatch.DrawString(_heroFont, "" + _animationAction, new Vector2(_heroX - 25, _heroY - 65), Color.White);
            spriteBatch.End();
        }
    }

}
