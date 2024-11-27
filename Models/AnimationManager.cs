using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagementSample.Models
{

    public class AnimationManager
    {
        float movmentSpeed;
        Animation walk;
        Animation attack;
        Animation idle;

        public AnimationManager(float MovmentSpeed)
        {
            movmentSpeed = MovmentSpeed;
            walk = new Animation(movmentSpeed);
 
            // attack speed 10
            attack = new Animation(5);

            idle = new Animation(4);
        }


        public void loadTextures(ContentManager content)
        {
            for (int i = 0; i <= 17; i++)
                walk.addFrame(content.Load<Texture2D>("Player/WalkRight/Golem_03_Walking_0" + i.ToString("D2")));

            for (int i = 0; i <= 11; i++)
                attack.addFrame(content.Load<Texture2D>("Player/Atack/Golem_03_Attacking_0" + i.ToString("D2")));

            for (int i = 0; i <= 11; i++)
                idle.addFrame(content.Load<Texture2D>("Player/Idle/Golem_03_Idle_0" + i.ToString("D2")));
        }

        public Texture2D AttackAnimation()
        {
            return attack.GetCurrentFrame();
        }

        public Texture2D WalkAnimation()
        {
            return walk.GetCurrentFrame();
        }

        public Texture2D IdleAnimation()
        {
            return idle.GetCurrentFrame();
        }
    }

}
