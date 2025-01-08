using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Entities.States;
using GameStateManagementSample.Models.Helpers;
using GameStateManagementSample.Models.Map;

namespace GameStateManagementSample.Models.Entities
{
    public abstract class Enemy : Entity
    {
        IEnemyState currentState;
        CollisionDetector collisionDetector;

        float distanceXToPlayer;
        float distanceYToPlayer;

        public Enemy() { }

        public Enemy(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
        : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items)
        {
        }

        public override void Move(Vector2 movement)
        {
            if (animState != AnimState.Death)
                Position += movement;
        }
        public override void Atack()
        {
            Console.WriteLine("AI Atack:" + ActiveWeapon);
        }
        public override void FlipTexture()
        {
            // Filip texture based on direction
            if (lastPosition.X < Position.X)
                flipTexture = false;
            else if (lastPosition.X > Position.X)
                flipTexture = true;
            //-------
        }


        // AI
        public virtual void FollowPlayer()
        {
            Vector2 movingDirection = Vector2.Zero;

            if (distanceXToPlayer > 120 && distanceXToPlayer < 500)
            {
                movingDirection.X += MovementSpeed;
                Move(movingDirection);
            }
            if (distanceXToPlayer < -120 && distanceXToPlayer > -500)
            {
                movingDirection.X -= MovementSpeed;
                Move(movingDirection);
            }
            if (distanceYToPlayer > 120 && distanceYToPlayer < 500)
            {
                movingDirection.Y += MovementSpeed;
                Move(movingDirection);
            }
            if (distanceYToPlayer < -120 && distanceYToPlayer > -500)
            {
                movingDirection.Y -= MovementSpeed;
                Move(movingDirection);
            }
        }

        public virtual void FollowPlayer2(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            bool colNorth = false, colEast = false, colSouth = false, colWest = false;

            /*
            #region StructureCollision

            if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                colNorth = true;
            if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                colSouth = true;
            if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                colWest = true;
            if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                colEast = true;

            #endregion
            */


            if (distanceXToPlayer > 120 && distanceXToPlayer < 500)
            {
                movingDirection.X += MovementSpeed;
                if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    colEast = true;
                if (!colEast)
                    Move(movingDirection);
            }
            if (distanceXToPlayer < -120 && distanceXToPlayer > -500)
            {
                movingDirection.X -= MovementSpeed;
                if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    colWest = true;
                if (!colWest)
                    Move(movingDirection);
            }
            if (distanceYToPlayer > 120 && distanceYToPlayer < 500)
            {
                movingDirection.Y += MovementSpeed;
                if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    colSouth = true;
                if (!colSouth)
                    Move(movingDirection);
            }
            if (distanceYToPlayer < -120 && distanceYToPlayer > -500)
            {
                movingDirection.Y -= MovementSpeed;
                if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    colNorth = true;
                if (!colNorth)
                    Move(movingDirection);
            }


        }

        public void UpdateDistanceToHero(Vector2 heroPos)
        {
            distanceXToPlayer = -(position.X - heroPos.X);
            distanceYToPlayer = -(position.Y - heroPos.Y);
        }

    }
}
