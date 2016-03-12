using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceWars
{
   
    public class SpaceCraft : Game
    {
        const int GAME_HEIGHT = 720;
        const int GAME_WIDTH = 1280;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Spaceship spaceship;

        public SpaceCraft()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
        }

       
        protected override void Initialize()
        {
           
            base.Initialize();
        }

       
        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceship = new Spaceship(new Vector2(20, GAME_HEIGHT / 2), Content.Load<Texture2D>("ship"));
           // spaceship = new Spaceship(new Vector2(20, GAME_WIDTH / 2)), Content.Load<Texture2D>("ship");
        }

       
        protected override void UnloadContent()
        {
            
        }

      
        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spaceship.Velocity = new Vector2(0, 0);         // if we dont press button anymore
            KeyHandler();
            UpdateEntities(elapsed);
            base.Update(gameTime);
        }

        private void UpdateEntities(float elapsed)
        {
            spaceship.Update(elapsed);
        }

        private void KeyHandler()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                spaceship.Velocity.Y = 10;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                spaceship.Velocity.Y = -10;
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
            spriteBatch.Begin();
            spaceship.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
