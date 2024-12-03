using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagementSample.Models.GUI
{
    public class HealthGUI : GUIObserver
    {
        int healthPoints;

        Color color = Color.Green;
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
            else
                color = Color.Green;

        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 position)
        {
            spriteBatch.DrawString(spriteFont, "HP   " + healthPoints.ToString(), position, color);
        }
    }
}
