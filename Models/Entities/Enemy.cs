using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Entities.States;
using GameStateManagementSample.Models.Helpers;
using GameStateManagementSample.Models.Map;
using System.Runtime.CompilerServices;

namespace GameStateManagementSample.Models.Entities
{
    public abstract class Enemy : Entity
    {
        IEnemyState currentState;
        CollisionDetector collisionDetector;

        float distanceXToPlayer;
        float distanceYToPlayer;

        float reductionDistance = 120;
        float recognitionDistance = 1100;

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

        public virtual void FollowPlayer(Room room)
        {
            if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance))
            {
                moveSouthEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance))
            {
                moveSouthWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance))
            {
                moveNorthEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance))
            {
                moveNorthWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance) && distanceYToPlayer > -recognitionDistance && distanceYToPlayer < recognitionDistance)
            {
                moveEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance) && distanceYToPlayer > -recognitionDistance && distanceYToPlayer < recognitionDistance)
            {
                moveWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance) && distanceXToPlayer > -recognitionDistance && distanceXToPlayer < recognitionDistance)
            {
                moveSouth(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && distanceXToPlayer > -recognitionDistance && distanceXToPlayer < recognitionDistance)
            {
                moveNorth(room);
            }

        }

        private bool isDistanceToPlayerinRecognitionDistance(float distance, float a, float b)
        {
            if (a < 0 && b < 0)
            {
                if (distance < a && distance > b)
                    return true;
                else
                    return false;
            }
            else
            {
                if (distance > a && distance < b)
                    return true;
                else
                    return false;
            }
        }

        private void moveSouthEast(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X += MovementSpeed / 2;
            movingDirection.Y += MovementSpeed / 2;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
        }
        private void moveSouthWest(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X -= MovementSpeed / 2;
            movingDirection.Y += MovementSpeed / 2;
            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
        }
        private void moveNorthEast(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y -= MovementSpeed / 2;
            movingDirection.X += MovementSpeed / 2;
            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
        }
        private void moveNorthWest(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y -= MovementSpeed / 2;
            movingDirection.X -= MovementSpeed / 2;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
        }
        private void moveNorth(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y -= MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
        }
        private void moveEast(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X += MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
        }
        private void moveSouth(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y += MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
        }
        private void moveWest(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X -= MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
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
