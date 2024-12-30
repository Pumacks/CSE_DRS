using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Entities.States;


namespace GameStateManagementSample.Models.Entities
{
    public abstract class Enemy : Entity
    {
        IEnemyState currentState;

        public Enemy() { }

        public Enemy(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
        : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items)
        {
        }

        public override void Move(Vector2 movment)
        {
            Console.WriteLine("AI Move");
        }
        public override void Atack()
        {
            Console.WriteLine("AI Atack:" + ActiveWeapon);
        }

    }
}
