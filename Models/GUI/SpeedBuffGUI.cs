using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace GameStateManagementSample.Models.GUI;

public class SpeedBuffGUI : GUIObserver
{
    public Texture2D Texture { get; set; }
    bool isEffectActive = false;
    private Vector2 Position = new Vector2(45, 120);
    private Vector2 textPosition = new Vector2(95, 105);
    public SpeedBuffGUI(Entity player) : base(player)
    {
    }


    public override void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
    {
        if (isEffectActive)
        {
            if (Texture != null)
            {

                spriteBatch.Begin();
                spriteBatch.Draw(texture: Texture,
                    position: Position,
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: 0f,
                    origin: new Vector2(Texture.Width / 2, Texture.Height / 2),
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);

            }
            spriteBatch.DrawString(spriteFont, ((int)player.SpeedPotionDuration + 1).ToString(), textPosition, Color.White);
            spriteBatch.End();
        }
    }

    public override void Update()
    {
        if (player.SpeedPotionDuration > 0)
        {
            isEffectActive = true;
        }

        else
            isEffectActive = false;
    }
}