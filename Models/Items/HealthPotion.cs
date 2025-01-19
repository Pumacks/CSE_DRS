using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameStateManagementSample.Models.Items
{
    public class HealthPotion : Item
    {

        #region attributes, fields and properties
        private int healingAmount;
        public int HealingAmount
        {
            get { return healingAmount; }
            set { healingAmount = value; }
        }

        public HealthPotion(string itemName, Texture2D itemTexture, Entity itemOwner, Vector2 position, int healingAmount, Engine engine) : base(itemName, itemTexture, itemOwner, engine)
        {
            Position = position;
            HealingAmount = healingAmount;
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

        /*
Möglicherweise noch andere Effekte wie temporär erhöhter ausgeteilter Schaden, oder reduzierter erhaltener Schaden?
Nach Implementierung besagter Mechaniken.
*/
        #endregion

        public override void use()
        {
            gameEngine.potionSound1.Play();
            gameEngine.burpSound.Play();
            ItemOwner.HealthPoints += healingAmount;
        }

    }
}
