using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagementSample.Models.Entities
{
    public class EnemySpearman : Enemy
    {

        public EnemySpearman()
        {
        }


        public EnemySpearman (int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
            : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i <= 6; i++)
                animManager.walk.addFrame(content.Load<Texture2D>("Skeleton_Spearman/walk_frames/walk" + i.ToString()));
            for (int i = 0; i <= 3; i++)
                animManager.attack.addFrame(content.Load<Texture2D>("Skeleton_Spearman/attack_frames/attack" + i.ToString()));
            for (int i = 0; i <= 6; i++)
                animManager.idle.addFrame(content.Load<Texture2D>("Skeleton_Spearman/idle_frames/idle" + i.ToString()));
            for (int i = 0; i <= 4; i++)
                animManager.death.addFrame(content.Load<Texture2D>("Skeleton_Spearman/death_frames/death" + i.ToString()));
            Texture = animManager.IdleAnimation();
            Position += Vector2.Zero;
        }
    }
}
