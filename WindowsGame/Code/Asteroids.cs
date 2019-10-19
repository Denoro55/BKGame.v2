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
    class Asteroids
    {
        public static int Width, Height;
        public static Random rand = new Random();

        public static SpriteBatch SpriteBatch { get; set; }
        static Star[] stars;

        public static Player Player { get; set; }
        public static float playerReload = 0;
        public static Target Target { get; set; }
        public static Enemy Enemy { get; set; }

        public static List<Bullet> bullets = new List<Bullet>();

        public static Texture2D healthTexture;
        public static int bossMaxHealth = 800;
        public static int bossHealth = bossMaxHealth;

        public static int getRandInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static void Init(SpriteBatch SpriteBatch, int Width, int Height)
        {
            Asteroids.Width = Width;
            Asteroids.Height = Height;
            Asteroids.SpriteBatch = SpriteBatch;
            stars = new Star[70];
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star(new Vector2(-rand.Next(1, 10), 0));
            }
            Player = new Player(new Vector2(100, 100));
            Target = new Target(new Vector2(100, 100));

            Enemy = new Enemy(new Vector2(200, 200));
        }
        public static void CreateBullet()
        {
            bullets.Add(new Bullet(Player.getPosPlayer(), getAngle(Player.getPosPlayer())));
        }
        static double getAngle(Vector2 Pos)
        {
            MouseState currentMouseState = Mouse.GetState();
            float xDiff = currentMouseState.X - Pos.X;
            float yDiff = currentMouseState.Y - Pos.Y;
            return Math.Atan2(yDiff, xDiff);
        }
        public float clamp(int x, float min, float max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }
        public static void Draw()
        {
            foreach (Star star in stars)
            {
                star.Draw();
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }

            Player.Draw();
            Enemy.Draw();

            Target.Draw();

            SpriteBatch.Draw(healthTexture, new Vector2(0, 0), new Rectangle(0, 0, Width * bossHealth / bossMaxHealth, healthTexture.Height), Color.White);
        }
        public static void Update()
        {
            foreach (Star star in stars)
            {
                star.Update();
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();

                // player bullets -> enemy

                float enemyX = Enemy.Pos.X;
                float enemyY = Enemy.Pos.Y;

                float x = (bullets[i].Pos.X + 4) - (enemyX + 16);
                float y = (bullets[i].Pos.Y + 4) - (enemyY + 16);

                float distance = x * x + y * y;
                float intersect_distance = (4 + 16) * (4 + 16);

                if (distance <= intersect_distance) // collision
                {
                    bullets[i].isAlive = false;
                    bossHealth--;
                    // bullets.RemoveAt(i);
                    // --;
                    // if (i < 0) break;
                }

                if (bullets[i].Hidden)
                {
                    bullets[i].isAlive = false;
                    // bullets.RemoveAt(i);
                    // if (i > 0) i--;
                }
            }
            bullets.RemoveAll(e => !e.isAlive);
            Target.Update();
        }
    }

    class Target
    {
        public static Texture2D Texture2D { get; set; }
        public Vector2 Pos;
        Color color = Color.White;
        public Target(Vector2 Pos)
        {
            this.Pos = Pos;
        }
        public void Update()
        {
            MouseState currentmousestate = Mouse.GetState();
            this.Pos.X = currentmousestate.X;
            this.Pos.Y = currentmousestate.Y;
        }
        public void Draw()
        {
            Asteroids.SpriteBatch.Draw(Texture2D, Pos, color);
        }
    }

    class Enemy
    {
        public Vector2 Pos;
        Color color = Color.White;
        public static Texture2D Texture2D { get; set; }
        public Enemy(Vector2 Pos)
        {
            this.Pos = Pos;
        }
        public void Draw()
        {
            Asteroids.SpriteBatch.Draw(Texture2D, Pos, color);
        }
    }
  
}
