using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Models.Helpers;
using GameStateManagementSample.Models.Map;
using GameStateManagementSample.Models.GameLogic;

namespace GameStateManagementSample.Models.Entities
{
    internal class EnemyWarrior : Enemy
    {

        public EnemyWarrior(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items, Engine engine)
            : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items, engine)
        {
            ActiveWeapon = new MeleeWeapon("Spear",
            null,
            null,
            15,
            1000,
            ReductionDistance,
            engine.Enemies,
            engine);
        }
        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i <= 6; i++)
                animManager.walk.addFrame(content.Load<Texture2D>("Skeleton_Warrior/walk_frames/walk" + i.ToString()));
            for (int i = 0; i <= 4; i++)
                animManager.attack.addFrame(content.Load<Texture2D>("Skeleton_Warrior/attack_frames/attack" + i.ToString()));
            for (int i = 0; i <= 6; i++)
                animManager.idle.addFrame(content.Load<Texture2D>("Skeleton_Warrior/idle_frames/idle" + i.ToString()));
            for (int i = 0; i <= 3; i++)
                animManager.death.addFrame(content.Load<Texture2D>("Skeleton_Warrior/death_frames/death" + i.ToString()));
            animManager.walk.ChangeAnimationDuration(3);
            animManager.attack.ChangeAnimationDuration(2);
            animManager.idle.ChangeAnimationDuration(2);
            animManager.death.ChangeAnimationDuration(15);
            Texture = animManager.IdleAnimation();
            Position += Vector2.Zero;
        }

        public override void FollowPlayer(Room room)
        {
            if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance))
            {
                if(HealthPoints <= fleeAt)
                    moveNorthWest(room);
                else
                    moveSouthEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance))
            {
                if(HealthPoints <= fleeAt)
                    moveNorthEast(room);
                else
                    moveSouthWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance))
            {
                if(HealthPoints <= fleeAt)
                    moveSouthWest(room);
                else
                    moveNorthEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance))
            {
                if(HealthPoints <= fleeAt)
                    moveSouthEast(room);
                else
                    moveNorthWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, reductionDistance, recognitionDistance) && distanceYToPlayer > -recognitionDistance && distanceYToPlayer < recognitionDistance)
            {
                if(HealthPoints <= fleeAt)
                    moveWest(room);
                else
                    moveEast(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceXToPlayer, -reductionDistance, -recognitionDistance) && distanceYToPlayer > -recognitionDistance && distanceYToPlayer < recognitionDistance)
            {
                if(HealthPoints <= fleeAt)
                    moveEast(room);
                else
                    moveWest(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, reductionDistance, recognitionDistance) && distanceXToPlayer > -recognitionDistance && distanceXToPlayer < recognitionDistance)
            {
                if(HealthPoints <= fleeAt)
                    moveNorth(room);
                else
                    moveSouth(room);
            }
            else if (isDistanceToPlayerinRecognitionDistance(distanceYToPlayer, -reductionDistance, -recognitionDistance) && distanceXToPlayer > -recognitionDistance && distanceXToPlayer < recognitionDistance)
            {
                if(HealthPoints <= fleeAt)
                    moveSouth(room);
                else
                    moveNorth(room);
            }
            else if(!isPlayerInReach()){
                Idling(room);
            }
            else if(isPlayerInReach())
            {
                ActiveWeapon.weaponAttack(this);
            }
        }
    }
}
