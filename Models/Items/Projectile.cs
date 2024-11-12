using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagementSample.Models.Items
{
    public class Projectile : Item
    {

        #region attributes, fields and properties
        public static int projectileNumber = 0;
        private Vector2 currentProjectilePosition;
        public Vector2 CurrentProjectilePosition
        {
            get
            {
                return this.currentProjectilePosition;
            }
            set
            {
                this.currentProjectilePosition = value;
            }
        }
        private Vector2 targetProjectilePosition;
        public Vector2 TargetProjectilePosition
        {
            get
            {
                return this.targetProjectilePosition;
            }
            set
            {
                this.targetProjectilePosition = value;
            }
        }
        private int velocity;
        public int Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }
        #endregion

        public Projectile (String itemName, Texture2D itemTexture, Entity itemOwner, Vector2 pos, Vector2 target, int projectileSpeed) : base (itemName, itemTexture, itemOwner) {
            this.ItemName = "Projectile Nr. " + ++projectileNumber;
            this.currentProjectilePosition = pos;
            this.targetProjectilePosition = target;
            this.velocity = projectileSpeed;
        }

        public override void use()
        {
            throw new System.NotImplementedException();
        }
    }
}
