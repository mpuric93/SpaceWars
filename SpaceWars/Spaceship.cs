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
            this.Bounds = new Rectangle(0, 0, 128, 128);
            this.BulletCount = 5;
        }
    }
}
