using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Models.Items;

public class Key : Item
{
    public Key(string itemName, Texture2D itemTexture, Entity itemOwner, Vector2 position, Engine engine) : base(itemName, itemTexture, itemOwner, engine)
    {
        this.Position = position;
    }

    public override void use()
    {
        if (ItemOwner is Player player)
        {
            player.HasKey = true;
        }
    }

    public override void DrawItem(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture: ItemTexture,
            position: Position,
            sourceRectangle: null,
            color: Color.White,
            rotation: 0f,
            origin: new Vector2(ItemTexture.Width / 2, ItemTexture.Height / 2),
            scale: 1f,
            effects: SpriteEffects.None,
            layerDepth: 0f);
    }
}