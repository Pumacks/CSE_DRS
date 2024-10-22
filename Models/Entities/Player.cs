using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GameStateManagementSample.Models.Entities
{
    public class Player : Entity
    {
        public Player() { }
        public Player(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, List<Item> items)
        : base(healthPoints, movementSpeed, playerPosition, texture, items)
        {
        }

        public override void Move()
        {
            Console.WriteLine("´Player is moving");
        }
        public override void Atack()
        {
            Console.WriteLine("Atack: " + ActiveWeapon);
        }
    }
}
