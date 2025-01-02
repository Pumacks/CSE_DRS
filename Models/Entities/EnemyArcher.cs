using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagementSample.Models.Entities
{
    public class EnemyArcher : Enemy
    {

        public EnemyArcher(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
            : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i <= 7; i++)
                animManager.walk.addFrame(content.Load<Texture2D>("Skeleton_Archer/walk_frames/walk" + i.ToString()));
            for (int i = 0; i <= 14; i++)
                animManager.attack.addFrame(content.Load<Texture2D>("Skeleton_Archer/shot_frames/shot" + i.ToString()));
            for (int i = 0; i <= 6; i++)
                animManager.idle.addFrame(content.Load<Texture2D>("Skeleton_Archer/idle_frames/idle" + i.ToString()));
            for (int i = 0; i <= 4; i++)
                animManager.death.addFrame(content.Load<Texture2D>("Skeleton_Archer/death_frames/death" + i.ToString()));
            animManager.walk.ChangeAnimationDuration(3);
            animManager.attack.ChangeAnimationDuration(2);
            animManager.idle.ChangeAnimationDuration(2);
            animManager.death.ChangeAnimationDuration(15);
            Texture = animManager.IdleAnimation();
            Position += Vector2.Zero;
        }
    }
}
