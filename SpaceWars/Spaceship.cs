using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
   public class Spaceship
    {
        public Vector2 Location;   
        public Vector2 Velocity;    
        private Texture2D image;
        public Rectangle Bounds;
        public int BulletCount;

        public Spaceship(Vector2 location, Texture2D image)
        {
            this.Location = location;
            this.image = image;
            this.Bounds = new Rectangle(0, 0, 68, 42);
            this.BulletCount = 5;
        }

        public void Update(float elapsed)
        {
            this.Location += this.Velocity * elapsed;
            this.Bounds.X = (int)Location.X;
            this.Bounds.Y = (int)Location.Y;

            if (this.Bounds.Top < 0)
            {
                this.Location.Y = 0;
            }
            if(this.Bounds.Bottom>720)
            {
                this.Location.Y = 720 - this.Bounds.Height;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.image, this.Location);
        }
    }
}
