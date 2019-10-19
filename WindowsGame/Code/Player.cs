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
    class Player
    {
        public Vector2 Pos;
        Color color = Color.White;
        int Speed { get; set; } = 3;

        public static Texture2D Texture2D { get; set; }

        public Vector2 getPosPlayer()
        {
            return new Vector2(this.Pos.X + 8, this.Pos.Y + 8);
        }

        public Player(Vector2 Pos)
        {
            this.Pos = Pos;
        }
        public void Update()
        {

        }

        public void Right()
        {
            if (this.Pos.X < Asteroids.Width -Texture2D.Width)
            {
                this.Pos.X += this.Speed;
            }
        }
        public void Left()
        {
            if (this.Pos.X > 0)
            {
                this.Pos.X -= this.Speed;
            }
        }
        public void Down()
        {
            if (this.Pos.Y < Asteroids.Height - Texture2D.Height)
            {
                this.Pos.Y += this.Speed;
            }
        }
        public void Up()
        {
            if (this.Pos.Y > 10)
            {
                this.Pos.Y -= this.Speed;
            }
        }

        public void Draw()
        {
            Asteroids.SpriteBatch.Draw(Texture2D, Pos, color);
        }
    }
}
