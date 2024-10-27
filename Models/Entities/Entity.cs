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
        public Weapon ActiveWeapon { get { return activeWeapon; } set { activeWeapon = value; } }
        private Texture2D texture;
        protected Vector2 position;
        protected bool flipTexture = false;
        //protected Dictionary<string, Texture2D> animation;

        

        // Weapon + Healthpots
        List<Item> items = new List<Item>();
        #endregion

        #region Properties
        public int HealthPoints { get; set; }
        public float MovementSpeed { get; set; }
        public Texture2D Texture { get { return texture; } set { texture = value; } }
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
            
            animationManager = new AnimationManager(MovementSpeed);

        }


        public abstract void Move();
        public abstract void Atack();

        public abstract void LoadContent(ContentManager content);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
