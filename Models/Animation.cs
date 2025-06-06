﻿using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameStateManagementSample.Models
{
    public class Animation
    {
        private List<Texture2D> textures = new List<Texture2D>();
        private float animationSpeed;
        private float timeCounter;
        private int currentFrame;
        private int iterationCounter;
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
            iterationCounter = currentFrame;
            if (currentFrame >= textures.Count - 1)
            {
                currentFrame = 0;
                return true;
            }
            return false;
        }

        public void ChangeAnimationDuration(float duration)
        {
            if (textures.Count > 0 && duration != 0)
                animationSpeed = textures.Count / duration;
        }

    }
}
