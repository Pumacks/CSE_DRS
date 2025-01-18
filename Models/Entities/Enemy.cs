using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Entities.States;
using GameStateManagementSample.Models.Helpers;
using GameStateManagementSample.Models.Map;
using System.Runtime.CompilerServices;
using GameStateManagementSample.Models.GameLogic;

namespace GameStateManagementSample.Models.Entities
{
    public abstract class Enemy : Entity
    {
        protected float distanceXToPlayer;
        protected float distanceYToPlayer;
        protected float followPlayerMultiplier = 2;

        protected float reductionDistance = 120;
        protected float recognitionDistance = 800;
        protected float howLong = 100;
        protected float fleeAt = 60;

        protected float ReductionDistance
        {
            get => reductionDistance;
            set => reductionDistance = value;
        }

        public Enemy() { }

        public Enemy(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items, Engine engine)
        : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items, engine)
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


        public virtual void Idling(Room room)
        {

            if (howLong > 0)
            {
                moveWest(room);
                if (howLong == 1)
                    howLong = -100;
                howLong--;

            }
            if (howLong < 0)
            {
                moveEast(room);
                if (howLong == -1)
                    howLong = 100;
                howLong++;
            }

        }

        public virtual void Flee(Room room)
        {

        }

        public virtual void FollowPlayer(Room room)
        {
            if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance))
            {
                if (HealthPoints <= fleeAt)
                    moveNorthWest(room);
                else
                    moveSouthEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance))
            {
                if (HealthPoints <= fleeAt)
                    moveNorthEast(room);
                else
                    moveSouthWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance))
            {
                if (HealthPoints <= fleeAt)
                    moveSouthWest(room);
                else
                    moveNorthEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance))
            {
                if (HealthPoints <= fleeAt)
                    moveSouthEast(room);
                else
                    moveNorthWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance) && distanceYToPlayer > -recognitionDistance && distanceYToPlayer < recognitionDistance)
            {
                if (HealthPoints <= fleeAt)
                    moveWest(room);
                else
                    moveEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance) && distanceYToPlayer > -recognitionDistance && distanceYToPlayer < recognitionDistance)
            {
                if (HealthPoints <= fleeAt)
                    moveEast(room);
                else
                    moveWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance) && distanceXToPlayer > -recognitionDistance && distanceXToPlayer < recognitionDistance)
            {
                if (HealthPoints <= fleeAt)
                    moveNorth(room);
                else
                    moveSouth(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && distanceXToPlayer > -recognitionDistance && distanceXToPlayer < recognitionDistance)
            {
                if (HealthPoints <= fleeAt)
                    moveSouth(room);
                else
                    moveNorth(room);
            }
            else if (recognitionDistance < distanceXToPlayer && recognitionDistance < distanceYToPlayer || -recognitionDistance > distanceXToPlayer && -recognitionDistance < distanceYToPlayer)
            {
                Idling(room);
            }
        }

        protected bool isDistanceToPlayerinRecognitionDistance(float distance, float a, float b)
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
        protected bool isPlayerInReach()
        {
            if (Math.Abs(distanceXToPlayer) <= reductionDistance && Math.Abs(distanceYToPlayer) <= reductionDistance)
            {
                return true;
            }
            else
                return false;
        }

        protected void moveSouthEast(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X += MovementSpeed;
            movingDirection.Y += MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
                moveWest(room);
        }
        protected void moveSouthWest(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X -= MovementSpeed;
            movingDirection.Y += MovementSpeed;
            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
                moveEast(room);
        }
        protected void moveNorthEast(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y -= MovementSpeed;
            movingDirection.X += MovementSpeed;
            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
                moveWest(room);
        }
        protected void moveNorthWest(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y -= MovementSpeed;
            movingDirection.X -= MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
                moveEast(room);
        }
        protected void moveNorth(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y -= MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
            {
                moveEast(room);
            }
        }
        protected void moveEast(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X += MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
                moveSouth(room);
        }
        protected void moveSouth(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.Y += MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
                moveWest(room);
        }
        protected void moveWest(Room room)
        {
            Vector2 movingDirection = Vector2.Zero;
            movingDirection.X -= MovementSpeed;

            if (!CollisionDetector.HasStructureCollision(room, this, movingDirection))
            {
                Move(movingDirection);
            }
            else
                moveNorth(room);
        }

        public void UpdateDistanceToHero(Vector2 heroPos)
        {
            distanceXToPlayer = -(position.X - heroPos.X);
            distanceYToPlayer = -(position.Y - heroPos.Y);
        }

        public void setGameTime(GameTime gametime)
        {
            GameTime = gametime;
        }
    }
}
