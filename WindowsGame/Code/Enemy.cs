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
    class Enemy
    {
        public Vector2 Pos;

        Color color = Color.White;

        int hiddenTimer = 150;
        int length = 0;
        int maxLength = 180;
        int shootingTime = 30;
        int shootingTimer = 30;
        float coefRadius = 0.4f;

        float alphaBlending = 0;
        float phase = 0;
        float angleRotationSpeed = 0.6f;

        double currentAngle = 0;

        bool isSegmentized = true;

        public static Texture2D Texture2D { get; set; }
        public Enemy(Vector2 Pos)
        {
            this.Pos = Pos;
        }
        public Vector2 getPos()
        {
            return new Vector2(Pos.X + length * (float)Math.Cos((currentAngle / 180 * Math.PI) + coefRadius * Math.Sin(phase)),
                Pos.Y + length * (float)Math.Sin((currentAngle / 180 * Math.PI) + coefRadius * Math.Sin(phase)));
        }
        public double getAngle(int randomize = 32)
        {
            return ((currentAngle + Asteroids.getRandInt(-randomize, randomize)) / 180 * Math.PI) + coefRadius * Math.Sin(phase);
        }
        public void Update()
        {
            phase += 0.04f;

            if (this.hiddenTimer > 0)
            {
                this.hiddenTimer--;
            } else if (alphaBlending < 1)
            {
                this.alphaBlending += 0.02f;
            }
            if (this.hiddenTimer <= 0)
            {
                // shooting
                if (shootingTime > 0)
                {
                    shootingTime--;
                }
                else
                {
                    Asteroids.CreateEnemyBullet();
                    shootingTime = shootingTimer;
                }
                // rotation
                if (isSegmentized && length < maxLength)
                {
                    length += 2;
                }
                double targetAngle = Math.Atan2(Asteroids.Player.Pos.Y - Pos.Y, Asteroids.Player.Pos.X - Pos.X) * 180.0 / Math.PI;
                if (currentAngle > 180)
                {
                    currentAngle = -180;
                }
                if (currentAngle < -180)
                {
                    currentAngle = 180;
                }
                if (Math.Abs(currentAngle - targetAngle) > 5)
                {
                    if (Math.Abs(currentAngle - targetAngle) < 180)
                    {
                        // Rotate current directly towards target.
                        if (currentAngle < targetAngle) currentAngle += angleRotationSpeed;
                        else currentAngle -= angleRotationSpeed;
                    }
                    else
                    {
                        // Rotate the other direction towards target.
                        if (currentAngle < targetAngle) currentAngle -= angleRotationSpeed;
                        else currentAngle += angleRotationSpeed;
                    }
                    
                }
            }
        }
        public void Draw()
        {
            double angle2 = (currentAngle / 180 * Math.PI) + coefRadius * Math.Sin(phase);
            if (this.hiddenTimer <= 0)
                Asteroids.SpriteBatch.Draw(Texture2D, Pos, color * alphaBlending);
                Asteroids.SpriteBatch.Draw(Texture2D, new Vector2(Pos.X + length * (float)Math.Cos(angle2),
                    Pos.Y + length * (float)Math.Sin(angle2)), color * alphaBlending);
                Asteroids.SpriteBatch.Draw(Texture2D, new Vector2(Pos.X + (length * 0.25f) * (float)Math.Cos(angle2),
                    Pos.Y + ( length * 0.25f ) * (float)Math.Sin(angle2)), color * alphaBlending);
                Asteroids.SpriteBatch.Draw(Texture2D, new Vector2(Pos.X + (length * 0.5f) * (float)Math.Cos(angle2),
                    Pos.Y + (length * 0.5f) * (float)Math.Sin(angle2)), color * alphaBlending);
                Asteroids.SpriteBatch.Draw(Texture2D, new Vector2(Pos.X + (length * 0.75f) * (float)Math.Cos(angle2),
                    Pos.Y + (length * 0.75f) * (float)Math.Sin(angle2)), color * alphaBlending);
        }
    }
}
