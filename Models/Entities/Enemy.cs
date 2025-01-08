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

        /*         public virtual void FollowPlayer(Room room)
                {
                    Vector2 movingDirection = Vector2.Zero;
                    bool colNorth = false, colEast = false, colSouth = false, colWest = false;

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
                } */

        public virtual void FollowPlayer(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;


            //SouthEast
            if (isDistanceToPlayerBetweenAandBPlus(distanceXToPlayer, 120, 500) && isDistanceToPlayerBetweenAandBPlus(distanceYToPlayer, 120, 500))
            {
                moveSouthEast(room);
            }
            // SouthWest
            else if (isDistanceToPlayerBetweenAandBPlus(distanceYToPlayer, 120, 500) && isDistanceToPlayerBetweenAandBMinus(distanceXToPlayer, -120, -500))
            {
                moveSouthWest(room);
            }
            //NorthEast
            else if (isDistanceToPlayerBetweenAandBMinus(distanceYToPlayer, -120, -500) && isDistanceToPlayerBetweenAandBPlus(distanceXToPlayer, 120, 500))
            {
                moveNorthEast(room);
            }
            //NorthWest
            else if (isDistanceToPlayerBetweenAandBMinus(distanceYToPlayer, -120, -500) && isDistanceToPlayerBetweenAandBMinus(distanceXToPlayer, -120, -500))
            {
                moveNorthWest(room);
            }
            // East
            else if (isDistanceToPlayerBetweenAandBPlus(distanceXToPlayer, 120, 500))
            {
                movingDirection.X += MovementSpeed;
                if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    Move(movingDirection);
            }
            // West
            else if (isDistanceToPlayerBetweenAandBMinus(distanceXToPlayer, -120, -500))
            {
                movingDirection.X -= MovementSpeed;
                if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    Move(movingDirection);
            }
            // South
            else if (isDistanceToPlayerBetweenAandBPlus(distanceYToPlayer, 120, 500))
            {
                movingDirection.Y += MovementSpeed;
                if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    Move(movingDirection);
            }
            // North
            else if (isDistanceToPlayerBetweenAandBMinus(distanceYToPlayer, -120, -500))
            {
                movingDirection.Y -= MovementSpeed;
                if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
                    Move(movingDirection);
            }

        }

        private bool isDistanceToPlayerBetweenAandBPlus(float distance, float a, float b)
        {
            if (distance > a && distance < b)
                return true;
            else
                return false;
        }
        private bool isDistanceToPlayerBetweenAandBMinus(float distance, float a, float b)
        {
            if (distance < a && distance > b)
                return true;
            else
                return false;
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

        public void UpdateDistanceToHero(Vector2 heroPos)
        {
            distanceXToPlayer = -(position.X - heroPos.X);
            distanceYToPlayer = -(position.Y - heroPos.Y);
        }

    }
}
