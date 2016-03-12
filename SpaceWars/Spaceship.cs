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
    }
}
