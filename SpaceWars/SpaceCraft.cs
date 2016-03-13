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
        

        List<Bullet> bullets;
        KeyboardState prevState;

        List<Powerup> powerups;

        SpriteFont basicFont;
        int score;
        string difficulty;


       

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
            bullets = new List<Bullet>();
            powerups = new List<Powerup>();
            rng = new Random();
            gameTimer = new Timer(1500);
            gameTimer.Elapsed += delegate
            {
                SpawnMeteorWall();
                SpawnPowerups();
            };
            gameTimer.Enabled = true;
            meteorVelocity = 200;
            holeLength = 8;
            prevState = Keyboard.GetState();
            score = 0;
            difficulty = "Easy";
            base.Initialize();
            
        }

       

        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceship = new Spaceship(new Vector2(20, GAME_HEIGHT / 2), Content.Load<Texture2D>("ship"));
            background1 = new Background(new Vector2(0, -90), Content.Load<Texture2D>("space"));
            background2 = new Background(new Vector2(1920, -90), Content.Load<Texture2D>("spaceRev"));
            basicFont = Content.Load<SpriteFont>("font");
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
            CheckMeteorBulletCollision();
            ChechPowerupCollision();
            RemoveNotVisible();
            SetDifficulity();
            KeyHandler();
            UpdateEntities(elapsed);
            base.Update(gameTime);
        }

        private void SetDifficulity()
        {
            if (score>=1500 && difficulty == "Easy")
            {
                difficulty = "Medium";
                meteorVelocity = 300;
                ChangeMeteorSpeed();
                ChangePoweupSpeed();
                holeLength -= 2;
            }

            if (score>=3000 && difficulty=="Medium")
            {
                difficulty = "Hard";
                meteorVelocity = 350;
                ChangeMeteorSpeed();
                ChangePoweupSpeed();
                holeLength -= 2;

            }
        }

        private void ChangePoweupSpeed()
        {
            foreach (var powerup in powerups)
            {
                powerup.Velocity = new Vector2(-meteorVelocity,0);
            }
        }

        private void ChangeMeteorSpeed()
        {
            foreach (var wall in meteors)
            {
                foreach (var meteor in wall)
                {
                    meteor.Velocity = new Vector2(-meteorVelocity,0);
                }
            } 
        }

        private void ChechPowerupCollision()
        {
            foreach (var powerup in powerups)
            {
                if (powerup.Bounds.Intersects(spaceship.Bounds))
                {
                    powerup.IsVisible = false;
                    spaceship.BulletCount++;
                }
            }
        }

        private void RemoveNotVisible()
        {
            for (int i = 0; i < meteors.Count; i++)
            {
                var currentWall = meteors[i];
                for (int j = 0; j < currentWall.Count; j++)
                {
                    if (!currentWall[j].IsVisible)
                    {
                        currentWall.RemoveAt(j);
                    }
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].IsVisible)
                {
                    bullets.RemoveAt(i);
                }
            }
            for (int i = 0; i < powerups.Count; i++)
            {
                if(!powerups[i].IsVisible)
                {
                    powerups.RemoveAt(i);
                }
            }
        }

        private void CheckMeteorBulletCollision()
        {
            foreach (var wall in meteors)
            {
                foreach (var meteor in wall)
                {
                    foreach (var bullet in bullets)
                    {
                        if (meteor.Bounds.Intersects(bullet.Bounds))
                        {
                            score -= 100;
                            bullet.IsVisible = false;
                            foreach (var meteorToDestroy in wall)
                            {
                                meteorToDestroy.IsVisible = false;
                            }
                        }
                    }
                }
            }
        }

        private void GameOverCollision()
        {
            
                foreach (var wall in meteors)
                    foreach (var meteor in wall)
                    {
                        if (meteor.Bounds.Intersects(spaceship.Bounds))
                        {
                        try
                        {
                            Exit();
                        }
                        catch (Exception x)
                        {

                            Console.WriteLine(x);
                        }
                                  
                        
                        }
                    }
            }
           
            
        

        private void UpdateEntities(float elapsed)
        {
            spaceship.Update(elapsed);
            background1.Update(elapsed);
            background2.Update(elapsed);
            score++;
            foreach (var wall in meteors)
            {
                foreach (var meteor in wall)
                {
                    try
                    {
                        meteor.Update(elapsed);
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e);
                    }
                   
                }

                foreach (var bullet in bullets)
                {
                    bullet.Update(elapsed);
                }

                
            }
            foreach (var powerup in powerups)
            {
                powerup.Update(elapsed);
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
                    try
                    {
                        currentWall.Add(new Meteor(new Vector2(GAME_WIDTH, i * 28 + 10), Content.Load<Texture2D>("meteor"), meteorVelocity));

                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e);
                    }
                }
                }
            meteors.Add(currentWall);

        }

        private void SpawnPowerups()
        {
            
            if (rng.Next(0,3)==0)
            {
                powerups.Add(new Powerup(new Vector2(rng.Next(1720, 2000), rng.Next(0, GAME_HEIGHT)),
               Content.Load<Texture2D>("target"), meteorVelocity));
            }
           
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

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space)&& spaceship.BulletCount>0)
            {
                spaceship.BulletCount--;
                bullets.Add(new Bullet(new Vector2(spaceship.Location.X, spaceship.Location.Y), Content.Load<Texture2D>("rocket")));
                
            }
            prevState = Keyboard.GetState();
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
            foreach (var bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            foreach (var powerup in powerups)
            {
                powerup.Draw(spriteBatch);
            }

            spriteBatch.DrawString(basicFont, "Bullets: " + spaceship.BulletCount, new Vector2(0, 0), Color.Red);
            spriteBatch.DrawString(basicFont, "Score " + score, new Vector2(0, 20), Color.Orange);
            spriteBatch.DrawString(basicFont, "Difficulty: " + difficulty, new Vector2(0, 40), Color.Orange);
            spriteBatch.DrawString(basicFont, "Developed by Mihailo Puric", new Vector2(1080, 0), Color.Orange);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
