using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace GameStateManagementSample.Models.Entities
{
    public abstract class Entity
    {
        #region atributes
        private int healthPoints;
        public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }

        private float movementSpeed;
        public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }

        private Weapon activeWeapon;
        public Weapon ActiveWeapon { get { return activeWeapon; } set { activeWeapon = value; } }

        private Texture2D texture;
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }
        // Weapon + Healthpots
        List<Item> items = new List<Item>();
        #endregion


        public Entity() { }

        public Entity(int healthPoints, float movmentSpeed, Vector2 playerPosition, Texture2D texture , List<Item> items)
        {
            this.healthPoints = healthPoints;
            this.movementSpeed = movmentSpeed;
            this.position = playerPosition;
            this.texture = texture;
            this.items = items;
        }


        public abstract void Move();
        public abstract void Atack();
    }
}
