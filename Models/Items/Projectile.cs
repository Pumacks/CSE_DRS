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
        #endregion

        public Projectile(String itemName, Texture2D itemTexture, Entity itemOwner, Vector2 pos, Vector2 target, int projectileSpeed, float weaponRange) : base(itemName, itemTexture, itemOwner)
        {
            this.ItemName = "Projectile Nr. " + ++projectileNumber;
            this.currentProjectilePosition = pos;
            this.targetProjectilePosition = target;
            this.velocity = projectileSpeed;
            this.projectileRotationFloatValue = calculateRotation(pos,target);
            this.speedVector = calculateSpeedVector(pos,target,projectileSpeed);
            // this.projectileTimeToLive = (int)(weaponRange * 1000 / projectileSpeed);
            this.projectileRange = (int) weaponRange;
            this.distanceCovered = 0;
        }

        private float calculateRotation(Vector2 start, Vector2 end)
        {
            if (start != null && end != null && start!=end)
            {
                float deltaX = end.X - start.X;
                float deltaY = end.Y - start.Y;

                float rotation = (float)Math.PI/2+(float)Math.Atan2(deltaY, deltaX);

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
            if (start != null && end != null && start!=end)
            {
                Vector2 dirV = new Vector2(end.X - start.X, end.Y - start.Y); //direction vector
                float absoluteOfDirectionVector = (float) Math.Sqrt(dirV.X * dirV.X + dirV.Y * dirV.Y);
                Vector2 unitV = new Vector2(dirV.X/absoluteOfDirectionVector, dirV.Y/absoluteOfDirectionVector);
                Vector2 speedVector = new Vector2((int)pixelsPerSecond*unitV.X,(int)pixelsPerSecond*unitV.Y);
                return speedVector;
            }
            else
            {
                Console.WriteLine("FEHLER! Für die Funktion calculateSpeedVector waren entweder der Startvektor oder der Zielvektor null, oder die beiden Vektoren waren gleich.");
                return new Vector2(0,0);
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
