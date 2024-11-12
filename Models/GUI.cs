using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagementSample.Models
{
    public class GUI
    {

        SpriteBatch spriteBatch;
        ContentManager contentManager;
        SpriteFont spriteFont;
        public GUI() { }

        public GUI(SpriteBatch spriteBatch, ContentManager contentManager, SpriteFont spriteFont)
        {
            this.spriteBatch = spriteBatch;
            this.contentManager = contentManager;
            this.spriteFont = spriteFont;
        }



        public void DrawGui(Player hero)
        {
            DrawHP(hero.HealthPoints);
        }



        public void DrawHP(int healthPoints)
        {
            spriteBatch.DrawString(spriteFont, "HP   " + healthPoints.ToString(), new Vector2(10, 10), Color.Red);
        }
    }
}
