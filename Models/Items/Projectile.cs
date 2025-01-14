using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.GameLogic;
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
        private Rectangle projectileHitBox;
        public Rectangle ProjectileHitBox
        {
            get
            {
                return this.projectileHitBox;
            }
            set
            {
                this.projectileHitBox = value;
            }
        }
        private Vector2 currentProjectilePosition;
        public Vector2 CurrentProjectilePosition
        {
            get
            {
                return this.currentProjectilePosition;
            }
            set
            {
                //BoundingBox = new Rectangle((int)value.X - ItemTexture.Width / 2, (int)value.Y - ItemTexture.Height / 2, ItemTexture.Width, ItemTexture.Height);
                this.currentProjectilePosition = value;
                // Update hitbox here too?
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
        private float projectileRotationFloatValue; //The value, determined by source and target coordinates, that influences how the arrow is rotated.
        public float ProjectileRotationFloatValue
        {
            get
            {
                return this.projectileRotationFloatValue;
            }
            set
            {
                this.projectileRotationFloatValue = value;
            }
        }
        private Vector2 speedVector; // The vector that is applied for every second of ingame time. Therefore at 60 FPS, each time the update method is called, a 60th of this is added to every projectile's current vector.
        public Vector2 SpeedVector
        {
            get
            {
                return this.speedVector;
            }
            set
            {
                this.speedVector = value;
            }
        }
        // private int projectileTimeToLive; // The time that the projectile has until it disappears. This effectively simulates the weapon range (instead of calculating distance) by taking into account the flight speed of the projectile and the range it can reach.
        // public int ProjectileTimeToLive
        // {
        //     get
        //     {
        //         return this.projectileTimeToLive;
        //     }
        //     set
        //     {
        //         this.projectileTimeToLive = value;
        //     }
        // }
        private int projectileRange;
        public int ProjectileRange
        {
            get
            {
                return this.projectileRange;
            }
            set
            {
                this.projectileRange = value;
            }
        }
        private int distanceCovered;
        public int DistanceCovered
        {
            get
            {
                return this.distanceCovered;
            }
            set
            {
                this.distanceCovered = value;
            }
        }
        private Texture2D projectileTexture;
        public Texture2D ProjectileTexture
        {
            get
            {
                return this.projectileTexture;
            }
            set
            {
                this.projectileTexture = value;
            }
        }
        private int projectileDamage;
        public int ProjectileDamage
        {
            get
            {
                return this.projectileDamage;
            }
            set
            {
                this.projectileDamage = value;
            }
        }
        private bool isStuck = false;
        public bool IsStuck
        {
            get
            {
                return this.isStuck;
            }
            set
            {
                this.isStuck = value;
            }
        }
        #endregion

        public Projectile(String itemName, Texture2D itemTexture, Entity itemOwner, Vector2 pos, Vector2 target, int projectileSpeed, float weaponRange, float weaponDamage, Engine engine) : base(itemName, itemTexture, itemOwner, engine)
        {
            this.ItemName = "Projectile Nr. " + ++projectileNumber;
            this.currentProjectilePosition = pos;
            this.targetProjectilePosition = target;

            this.projectileRotationFloatValue = calculateRotation(pos, target);
            this.speedVector = calculateSpeedVector(pos, target, projectileSpeed);
            // this.projectileTimeToLive = (int)(weaponRange * 1000 / projectileSpeed);

            this.velocity = projectileSpeed;
            this.projectileRange = (int)weaponRange;
            this.projectileDamage = (int) weaponDamage;
            this.distanceCovered = 0;

            this.projectileTexture = itemTexture;

            this.projectileHitBox = new Rectangle( // The hitbox shall only be a small rectangle at the tip of the arrow.
                (int) currentProjectilePosition.X - projectileTexture.Width / 2,
                (int) currentProjectilePosition.Y - projectileTexture.Height / 2,
                projectileTexture.Width,
                projectileTexture.Width);
        }

        private float calculateRotation(Vector2 start, Vector2 end)
        {
            if (start != end)
            {
                float deltaX = end.X - start.X;
                float deltaY = end.Y - start.Y;

                float rotation = (float)Math.PI / 2 + (float)Math.Atan2(deltaY, deltaX);

                return rotation;
            }
            else
            {
                Console.WriteLine("FEHLER! Für die Funktion calculateRotation waren entweder der Startvektor oder der Zielvektor null, oder die beiden Vektoren waren gleich.");
                return 0;
            }
        }

        private Vector2 calculateSpeedVector(Vector2 start, Vector2 end, int pixelsPerSecond)
        {
            if (start != end)
            {
                Vector2 dirV = new Vector2(end.X - start.X, end.Y - start.Y); //direction vector
                float absoluteOfDirectionVector = (float)Math.Sqrt(dirV.X * dirV.X + dirV.Y * dirV.Y);
                Vector2 unitV = new Vector2(dirV.X / absoluteOfDirectionVector, dirV.Y / absoluteOfDirectionVector);
                Vector2 speedVector = new Vector2((int)pixelsPerSecond * unitV.X, (int)pixelsPerSecond * unitV.Y);
                return speedVector;
            }
            else
            {
                Console.WriteLine("FEHLER! Für die Funktion calculateSpeedVector waren entweder der Startvektor oder der Zielvektor null, oder die beiden Vektoren waren gleich.");
                return new Vector2(0, 0);
            }
        }

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        public override void DrawItem(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
            //DrawProjectile(spriteBatch);
        }

        // Leaving this here as a dummy to put the method for drawing projectiles. Alternatively, it will be found in the Draw-method of GameplayScreen.
        /*
        public void DrawProjectile(SpriteBatch spriteBatch) {
            
        }*/
    }
}
