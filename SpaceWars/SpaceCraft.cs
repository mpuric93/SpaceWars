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
        }

       
        protected override void Initialize()
        {
           
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

       
        protected override void UnloadContent()
        {
            
        }

      
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.End();
        }
    }
}
