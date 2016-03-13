using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
   public class Meteor
    {
        public Vector2 Location;
        public Vector2 Velocity;
        private Texture2D image;
        public Rectangle Bounds;
        public bool IsVisible;

        public Meteor(Vector2 location, Texture2D image, int velocity)
        {
            this.Location = location;
            this.image = image;
            this.IsVisible = true;
            this.Bounds = new Rectangle(0, 0, 29, 29);
            this.Velocity = new Vector2(-velocity, 0);

        }

        public void Update(float elapsed)
        {
            this.Location += this.Velocity * elapsed;
            this.Bounds.X = (int)Location.X;
            this.Bounds.Y = (int)Location.Y;

            if (this.Location.X < -20)
            {
                this.IsVisible = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (this.IsVisible)
            {
                sb.Draw(this.image, this.Location);
            }
            
        }
    }
}
