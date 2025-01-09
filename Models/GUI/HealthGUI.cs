using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagementSample.Models.GUI
{
    public class HealthGUI : GUIObserver
    {
        int healthPoints;
        public Texture2D Texture { get; set; }
        public Texture2D KeyTexture { get; set; }
        private Vector2 textPosition = new Vector2(95, 35);
        private Vector2 TexturePosition = new Vector2(50, 50);
        private Vector2 KeyTexturePosition = new Vector2(220, 65);
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
            if (Texture != null)
            {
                spriteBatch.Draw(texture: Texture,
                    position: TexturePosition,
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: 0f,
                    origin: new Vector2(Texture.Width / 2, Texture.Height / 2),
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
            }

            if (KeyTexture != null && player is Player { HasKey: true })
            {
                spriteBatch.Draw(texture: KeyTexture,
                    position: KeyTexturePosition,
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: 0f,
                    origin: new Vector2(Texture.Width / 2, Texture.Height / 2),
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
            }
            spriteBatch.DrawString(spriteFont, healthPoints.ToString(), textPosition, color);
            spriteBatch.End();
        }
    }
}
