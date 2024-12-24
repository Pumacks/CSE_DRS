using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagementSample.Models.GUI
{
    public class HealthGUI : GUIObserver
    {
        int healthPoints;
        private Vector2 healthPositionGUI = new Vector2(20, 20);
        Color color = new Color(73, 255, 0); // init with green color
        public HealthGUI(Entity player) : base(player)
        {
            healthPoints = player.HealthPoints;
        }

        public override void Update()
        {
            healthPoints = player.HealthPoints;
            if (healthPoints < 30)
                color = Color.Red;
            else if (healthPoints < 60)
                color = Color.Yellow;
            else if (healthPoints < 150)
                color = new Color(73, 255, 0);
            else
                color = new Color(26, 25, 230);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "HP   " + healthPoints.ToString(), healthPositionGUI, color);
            spriteBatch.End();
        }
    }
}
