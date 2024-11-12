using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameStateManagementSample.Models.Entities
{
    public abstract class Entity
    {
        #region atributes

        private Weapon activeWeapon;
        private Texture2D texture;
        protected Vector2 position;
        protected Rectangle boundingBox;
        protected bool flipTexture = false;
        protected GameTime gameTime;
      
        // Weapon + Healthpots
        List<Item> items = new List<Item>();
        #endregion

        #region Properties
        public int HealthPoints { get; set; }
        public float MovementSpeed { get; set; }
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        public Weapon ActiveWeapon { get { return activeWeapon; } set { activeWeapon = value; } }
        public Rectangle BoundingBox { get { return boundingBox; } }
      
        public Vector2 Position { get { return this.position;} private set {this.position = value;} } // Hinzugefügt am 09.11.2024 von Stylianos, um es möglich zu machen, den Abstand von Gegnern zum Spieler für Waffenangriffe zu berechnen.

        #endregion

        protected AnimationManager animationManager;
        public Entity() { }

        public Entity(int healthPoints, float movmentSpeed, Vector2 playerPosition, Texture2D texture, List<Item> items)
        {
            this.HealthPoints = healthPoints;
            this.MovementSpeed = movmentSpeed;
            this.position = playerPosition;
            this.texture = texture;
            this.items = items;

            this.boundingBox = new Rectangle((int)position.X - texture.Width / 2,
                                             (int)position.Y - texture.Height / 2,
                                             texture.Width,
                                             texture.Height);
          
            animationManager = new AnimationManager(MovementSpeed);
        }


        public abstract void Move();
        public abstract void Atack();

        public abstract void LoadContent(ContentManager content);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void SetGameTime(GameTime gameTime)
        {
            if (gameTime != null)
                this.gameTime = gameTime;
        }
    }
}
