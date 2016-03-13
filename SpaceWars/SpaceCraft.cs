using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Timers;

namespace SpaceWars
{
   
    public class SpaceCraft : Game
    {
        const int GAME_HEIGHT = 720;
        const int GAME_WIDTH = 1280;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Spaceship spaceship;
        Background background1;
        Background background2;
        List<List<Meteor>> meteors;
        Timer gameTimer;
        int meteorVelocity;
        int holeLength;
        Random rng;
       

        public SpaceCraft()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
        }

       
        protected override void Initialize()
        {
            meteors = new List<List<Meteor>>();
            rng = new Random();
            gameTimer = new Timer(1500);
            gameTimer.Elapsed += delegate { SpawnMeteorWall(); };
            gameTimer.Enabled = true;
            meteorVelocity = 250;
            holeLength = 8;
            base.Initialize();
            
        }

      

        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceship = new Spaceship(new Vector2(20, GAME_HEIGHT / 2), Content.Load<Texture2D>("ship"));
            background1 = new Background(new Vector2(0, -90), Content.Load<Texture2D>("space"));
            background2 = new Background(new Vector2(1920, -90), Content.Load<Texture2D>("spaceRev"));

        }

       
     /*   protected override void UnloadContent()
        {
            
        }*/

      
        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spaceship.Velocity = new Vector2(0, 0);         // if we dont press button anymore
            background1.Velocity = new Vector2(-50, 0);
            background2.Velocity = new Vector2(-50, 0);
            GameOverCollision();
            KeyHandler();
            UpdateEntities(elapsed);
            base.Update(gameTime);
        }

        private void GameOverCollision()
        {
         
            foreach (var wall in meteors)
            {
                foreach (var meteor in wall)
                {
                    if (meteor.Bounds.Intersects(spaceship.Bounds))
                    {

                        try
                        {
                           Exit();
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e);
                        }
                    }
                }
            }
        }

        private void UpdateEntities(float elapsed)
        {
            spaceship.Update(elapsed);
            background1.Update(elapsed);
            background2.Update(elapsed);
            foreach (var wall in meteors)
            {
                foreach (var meteor in wall)
                {
                    meteor.Update(elapsed);
                }
            }
        }

        private void SpawnMeteorWall()
        {
            List<Meteor> currentWall = new List<Meteor>();
            int holePosition = rng.Next(0,25 - holeLength);
            for (int i = 0; i < 25; i++)
            {
                if(i<=holePosition || i>= holePosition+holeLength)
                {
                    currentWall.Add(new Meteor(new Vector2(GAME_WIDTH, i * 28 + 10), Content.Load<Texture2D>("meteor"), meteorVelocity));
                }
                }
            meteors.Add(currentWall);

        }

        private void KeyHandler()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                spaceship.Velocity.Y = 500;
                background1.Velocity.Y = -20;
                background2.Velocity.Y = -20;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                spaceship.Velocity.Y = -500;
                background1.Velocity.Y = 20;
                background2.Velocity.Y = 20;
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
            spriteBatch.Begin();
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            spaceship.Draw(spriteBatch);
            foreach (var wall in meteors)
            {
                foreach (var meteor in wall)
                {
                    meteor.Draw(spriteBatch);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
