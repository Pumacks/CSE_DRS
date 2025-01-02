using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagementSample.Models
{
    public class Animation
    {
        private List<Texture2D> textures = new List<Texture2D>();
        private float animationSpeed;
        private float timeCounter;
        private int currentFrame;
        private bool isLooping = true;
        private float totalDuration;

        public List<Texture2D> Textures { get => textures; }
 

        public Animation(float totalDuration)
        {
            this.timeCounter = 0;
            this.currentFrame = 0;
            this.animationSpeed = 1;
            this.totalDuration = totalDuration;
        }

        public Animation(float animationSpeed, bool loop) : this(animationSpeed)
        {
            this.isLooping = loop;
        }


        public Texture2D GetCurrentFrame()
        {
            if (!(!isLooping && currentFrame == textures.Count - 1))
                ChangeToNextFrame();

            if (textures.Count > 0)
            {
                return textures[currentFrame];
            }
            return null;
        }


        public void addFrame(Texture2D texture)
        {
            textures.Add(texture);
            if (textures.Count > 0 && totalDuration != 0)
                animationSpeed = textures.Count / totalDuration;
        }

        public void ChangeToNextFrame()
        {
            timeCounter += 0.1f;
            if (timeCounter >= 1f / animationSpeed)
            {
                currentFrame++;
                timeCounter = 0f;
            }

            if (currentFrame >= textures.Count)
            {
                currentFrame = 0;
            }
        }

        public bool IterationFinished()
        {
            return currentFrame == textures.Count - 1;
        }

        public void ChangeAnimationDuration(float duration)
        {
            if (textures.Count > 0 && duration != 0)
                animationSpeed = textures.Count / duration;
        }

    }
}
