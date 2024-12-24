using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameStateManagementSample.Models.Entities
{
    public abstract class Entity
    {
        #region atributes

        private Weapon activeWeapon;
        protected Item[] inventory;
        private Texture2D texture;
        protected Vector2 position;
        protected Rectangle boundingBox;
        protected bool flipTexture = false;
        protected GameTime gameTime;


        //Testing Purposes, required to give a Camera to an Entity so that the
        private GameStateManagementSample.Models.Camera.Camera camera;
        public GameStateManagementSample.Models.Camera.Camera CameraProperty { get { return camera; } set { camera = value; } }


        protected SpriteFont spriteFont;

        // Weapon + Healthpots
        List<Item> items = new List<Item>();
        #endregion

        #region Properties
        public int HealthPoints { get; set; }
        public float MovementSpeed { get; set; }
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        public Weapon ActiveWeapon { get { return activeWeapon; } set { activeWeapon = value; } }

        public Item[] Inventory { get { return this.inventory; } set { inventory = value; } }
        public GameTime GameTime { get { return gameTime; } set { gameTime = value; } }
        public Rectangle BoundingBox { get { return boundingBox; } }

        public Vector2 Position { 
            get { return position; }
            set
            {
                position = value;
                boundingBox = new Rectangle((int)position.X - Texture.Width / 2, (int)position.Y - Texture.Height / 2,
                    texture.Width, texture.Height);
            }
        } 


        #endregion

        protected AnimationManager animationManager;
        public Entity() { }

        public Entity(int healthPoints, float movmentSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
        {
            this.HealthPoints = healthPoints;
            this.MovementSpeed = movmentSpeed;
            this.position = playerPosition;
            this.texture = texture;
            this.items = items;
            this.spriteFont = spriteFont;
            this.inventory = new Item[7];

            this.boundingBox = new Rectangle((int)position.X - texture.Width / 2,
                                             (int)position.Y - texture.Height / 2,
                                             texture.Width,
                                             texture.Height);
            this.spriteFont = spriteFont;

            animationManager = new AnimationManager(MovementSpeed);
        }


        public abstract void Move(Vector2 movment);
        public abstract void Atack();

        public abstract void LoadContent(ContentManager content);

        public abstract void Draw(SpriteBatch spriteBatch);


        public abstract void TakeDamage(int damage);

        public void SetGameTime(GameTime gameTime)
        {
            if (gameTime != null)
                this.gameTime = gameTime;
        }
    }
}
