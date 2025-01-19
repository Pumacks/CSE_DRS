using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameStateManagementSample.Models.Items
{
    public abstract class Item
    {
        // camelCase, snake_case, kebab-case, PascalCase, sPonGEbObCaSe
        #region attributes, fields and properties
        private String itemName;
        public String ItemName
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        private Texture2D itemTexture;
        public Texture2D ItemTexture
        {
            get { return this.itemTexture; }
            set { this.itemTexture = value; }
        }

        private Entity itemOwner;
        public Entity ItemOwner
        {
            get { return this.itemOwner; }
            set { this.itemOwner = value; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }

            set
            {
                position = value;
                BoundingBox = new Rectangle((int)position.X - ItemTexture.Width / 2, (int)position.Y - ItemTexture.Height / 2, ItemTexture.Width, ItemTexture.Height);
            }
        }
        public Rectangle BoundingBox { get; set; }

        protected Engine gameEngine;
        //private Sound itemSounds
        // ItemType können wir einfach mittels Abfrage nach instanceOf weapon oder consumable oder meleeweapon, rangedweapon, etc. machen.
        #endregion

        public Item(String itemName, Texture2D itemTexture, Entity itemOwner, Engine engine)
        {
            this.itemName = itemName;
            this.itemTexture = itemTexture;
            this.itemOwner = itemOwner;
            this.gameEngine = engine;
           
        }

        #region abstract methods
        public abstract void use(); // Könnte z.B. je nach Item die Methoden consume() bzw. drink() bzw. eat() oder attack() aufrufen. Einfach als optionale Zusatzmethode gedacht.

        public abstract void DrawItem(SpriteBatch spriteBatch);


        #endregion


    }
}
