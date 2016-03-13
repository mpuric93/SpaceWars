using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    public class Bullet
    {
        public Vector2 Location;
        public Vector2 Velocity;
        private Texture2D image;
        public Rectangle Bounds;
        public bool IsVisible;

        public Bullet(Vector2 location, Texture2D image)
        {
            this.Location = location;
            this.image = image;
            this.IsVisible = true;
            this.Bounds = new Rectangle(0,0,40,30);
            this.Velocity = new Vector2(200, 0);

        }

        public void Update(float elapsed)
        {
            this.Location += this.Velocity * elapsed;
            this.Bounds.X = (int)Location.X;
            this.Bounds.Y = (int)Location.Y;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.image, this.Location);
        }
    }
}

