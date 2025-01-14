using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Models.GUI;

public class KeyGUI : GUIObserver
{
    public Texture2D KeyTexture { get; set; }
    private Vector2 KeyTexturePosition = new Vector2(220, 55);
    private bool hasKey = false;
    public KeyGUI(Entity player) : base(player)
    {
    }

    public override void Update()
    {
        hasKey = player is Player { HasKey: true };
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
    {
        if (KeyTexture != null && hasKey)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture: KeyTexture,
                position: KeyTexturePosition,
                sourceRectangle: null,
                color: Color.White,
                rotation: 0f,
                origin: new Vector2(KeyTexture.Width / 2, KeyTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();
        }
    }
}