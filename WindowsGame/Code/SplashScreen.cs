using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame
{
    static class SplashScreen
    {
        public static Texture2D Background { get; set; }
        static int counter = 0;
        static Color color;
        public static SpriteFont Font { get; set; }
        static Vector2 textPosition = new Vector2(20, 20);

        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.DrawString(Font, "Астероиды", textPosition, color);
        }
        static public void Update()
        {
            color = Color.FromNonPremultiplied(255, 255, 255, counter % 256);
            counter++;
        }
    }
}
