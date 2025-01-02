using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Models.Items;

public class SpeedPotion : Item
{
    private float movmentSpeedBoost;
    private float secondsDuration;

    public float MovmentSpeedBoost { get => movmentSpeedBoost;}
    public float SecondsDuration { get => secondsDuration; }

    public SpeedPotion(string itemName, Texture2D itemTexture, Entity itemOwner, Vector2 position, float movmentSpeedBoost, float secondsDuration) : base(itemName, itemTexture, itemOwner)
    {
        this.Position = position;
        this.movmentSpeedBoost = movmentSpeedBoost;
        this.secondsDuration = secondsDuration;
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

    public override void use()
    {
        ItemOwner.UseSpeedPotion(this);
    }
}