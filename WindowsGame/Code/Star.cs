using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame
{
    class Star
    {
        Vector2 Pos;
        Vector2 Dir;
        Color color;
        public static Texture2D Texture2D { get; set; }

        public Star(Vector2 Pos, Vector2 Dir)
        {
            this.Pos = Pos;
            this.Dir = Dir;
        }
        public Star(Vector2 Dir)
        {
            this.Dir = Dir;
        }
        public void Update()
        {
            Pos += Dir;
            //  Console.WriteLine(Pos);
            if (Pos.X < 0)
            {
                RandomSet();
            }
        }
        public void RandomSet()
        {
            Pos = new Vector2(Asteroids.getRandInt(Asteroids.Width, Asteroids.Width + 300),
                Asteroids.getRandInt(0, Asteroids.Height));
            color = Color.FromNonPremultiplied(Asteroids.getRandInt(0, 256), Asteroids.getRandInt(0, 256), Asteroids.getRandInt(0, 256), 255);
        }
        public void Draw()
        {
            Asteroids.SpriteBatch.Draw(Texture2D, Pos, color);
        }
    }
}
