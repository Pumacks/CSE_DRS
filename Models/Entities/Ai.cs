using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Entities.States;
using System.ComponentModel;


namespace GameStateManagementSample.Models.Entities
{
    public class Ai
    {
        private Enemy enemy;
        float distanceXToPlayer;
        float distanceYToPlayer;


        public Ai() { }

        public void FollowPlayer(ref Enemy enemy)
        {
            Vector2 movingDirection = Vector2.Zero;

            if (distanceXToPlayer > 120 && distanceXToPlayer < 500)
            {
                movingDirection.X += enemy.MovementSpeed;
                enemy.Move(movingDirection);
            }
            if (distanceXToPlayer < -120 && distanceXToPlayer > -500)
            {
                movingDirection.X -= enemy.MovementSpeed;
                enemy.Move(movingDirection);
            }
            if (distanceYToPlayer > 120 && distanceYToPlayer  < 500)
            {
                movingDirection.Y += enemy.MovementSpeed;
                enemy.Move(movingDirection);
            }
            if (distanceYToPlayer < -120 && distanceYToPlayer > -500)
            {
                movingDirection.Y -= enemy.MovementSpeed;
                enemy.Move(movingDirection);
            }
        }

        /// <summary>
        /// Wird dauerhaft aufgerufen um die Heroposition in Realtime an die KI zu übermitteln
        /// </summary>
        /// <param name="heroPos"></param>
        public void UpdateDistanceToHero(Vector2 heroPos,  Vector2 enemyPos)
        {
            distanceXToPlayer = -(enemyPos.X - heroPos.X);
            distanceYToPlayer = -(enemyPos.Y - heroPos.Y);
        }
    }

}