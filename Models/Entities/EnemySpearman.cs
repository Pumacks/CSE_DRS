using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Models.Helpers;
using GameStateManagementSample.Models.Map;


namespace GameStateManagementSample.Models.Entities
{
    public class EnemySpearman : Enemy
    {

        public EnemySpearman()
        {
        }


        public EnemySpearman(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
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
            animManager.walk.ChangeAnimationDuration(3);
            animManager.attack.ChangeAnimationDuration(2);
            animManager.idle.ChangeAnimationDuration(2);
            animManager.death.ChangeAnimationDuration(15);
            Texture = animManager.IdleAnimation();
            Position += Vector2.Zero;
        }

        // public override void FollowPlayer2(Room room)
        // {
        //     Vector2 movingDirection = Vector2.Zero;
        //     bool colNorth = false, colEast = false, colSouth = false, colWest = false;

        //     /*
        //     #region StructureCollision

        //     if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //         colNorth = true;
        //     if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //         colSouth = true;
        //     if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //         colWest = true;
        //     if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //         colEast = true;

        //     #endregion
        //     */

        //     // Attacking:
        //     if (Math.Sqrt(distanceXToPlayer * distanceXToPlayer + distanceYToPlayer * distanceYToPlayer) <= this.ActiveWeapon.WeaponRange)
        //     {

        //         ActiveWeapon.weaponAttack(this);
        //     }
        //     else
        //     {


        //         if (distanceXToPlayer > 120 && distanceXToPlayer < 500)
        //         {
        //             movingDirection.X += MovementSpeed;
        //             if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //                 colEast = true;
        //             if (!colEast)
        //                 Move(movingDirection);
        //         }
        //         if (distanceXToPlayer < -120 && distanceXToPlayer > -500)
        //         {
        //             movingDirection.X -= MovementSpeed;
        //             if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //                 colWest = true;
        //             if (!colWest)
        //                 Move(movingDirection);
        //         }
        //         if (distanceYToPlayer > 120 && distanceYToPlayer < 500)
        //         {
        //             movingDirection.Y += MovementSpeed;
        //             if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //                 colSouth = true;
        //             if (!colSouth)
        //                 Move(movingDirection);
        //         }
        //         if (distanceYToPlayer < -120 && distanceYToPlayer > -500)
        //         {
        //             movingDirection.Y -= MovementSpeed;
        //             if (CollisionDetector.HasStructureCollision(room, this, movingDirection))
        //                 colNorth = true;
        //             if (!colNorth)
        //                 Move(movingDirection);
        //         }

        //     }


        // }
    }
}
