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
        public Animation walk;
        public Animation attack;
        public Animation idle;
        public Animation death;

        public AnimationManager(float MovmentSpeed)
        {
            movmentSpeed = MovmentSpeed;
            walk = new Animation(movmentSpeed);
 
            // attack speed 10
            attack = new Animation(5);

            idle = new Animation(4);

            death = new Animation(4, false);
        }


        public void loadTextures(ContentManager content)
        {
 

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

        public Texture2D DeathAnimation()
        {
            return death.GetCurrentFrame();
        }

        public bool DeathAnimationFinished()
        {
            return death.GetCurrentFrame() == death.Textures.Last();
        }
    }

}
