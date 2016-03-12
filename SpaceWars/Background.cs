using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    class Background
    {
        public Vector2 Location;
        public Vector2 Velocity;
        private Texture2D image;
        public Rectangle Bounds;
      

        public Background(Vector2 location, Texture2D image)
        {
            this.Location = location;
            this.image = image;
            this.Bounds = new Rectangle(0, 0, 128, 128);
            
        }

        public void Update(float elapsed)
        {
            this.Location += this.Velocity;
            this.Bounds.X = (int)Location.X;
            this.Bounds.Y = (int)Location.Y;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.image, this.Location);
        }
    }
}
