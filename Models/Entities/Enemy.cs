using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameStateManagementSample.Models.Entities
{
    public class Enemy : Entity
    {


        public Enemy() { }

        public Enemy(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, List<Item> items)
        : base(healthPoints, movementSpeed, playerPosition, texture, items)
        {
        }

        public override void Move()
        {
            Console.WriteLine("AI Move");
        }
        public override void Atack()
        {
            Console.WriteLine("AI Atack:" + ActiveWeapon);
        }
    }
}
