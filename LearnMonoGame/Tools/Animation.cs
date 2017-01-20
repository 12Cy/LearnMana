using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LearnMonoGame.Tools
{
    public class Animation
    {

        #region Field Region

        Rectangle[] frames;
        int framesPerSecond; // determines how many frames will be displayed each second and determines if the animation is slow or fast.

        TimeSpan frameLength; // holds how long to display each frame
        TimeSpan frameTimer; // holds how much time has passed since the last frame change.

        int currentFrame; // current frame displayed in the animation
        int frameWidth;
        int frameHeight;

        #endregion

        #region Property Region

        public int FramesPerSecond
        {
            get { return framesPerSecond; }
            set
            {
                if (value < 1)
                    framesPerSecond = 1;
                else if (value > 60)
                    framesPerSecond = 60;
                else
                    framesPerSecond = value;
                frameLength = TimeSpan.FromSeconds(1 / (double)framesPerSecond);
            }
        }

        public Rectangle CurrentFrameRect //returns what the current rectangle for the animation is
        {
            get { return frames[currentFrame]; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = (int)MathHelper.Clamp(value, 0, frames.Length - 1); //  number of frames minus 1 because arrays are zero based
            }
        }

        public int FrameWidth
        {
            get { return frameWidth; }
        }

        public int FrameHeight
        {
            get { return frameHeight; }
        }

        #endregion

        #region Constructor Region

        //xOffset and yOffset are used in generating the rectangles for each frame.

        public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int
        yOffset)
        {
            frames = new Rectangle[frameCount];
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = new Rectangle(xOffset + (frameWidth * i), yOffset, frameWidth, frameHeight);
            }
            FramesPerSecond = 5; //values can be changes (individuell)
            Reset();
        }

        private Animation(Animation animation)
        {
            this.frames = animation.frames;
            FramesPerSecond = 5;
        }

        #endregion

        #region Method Region

        public void Update(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime;
            if (frameTimer >= frameLength)
            {
                frameTimer = TimeSpan.Zero;
                currentFrame = (currentFrame + 1) % frames.Length;
            }
        }
        public void Reset()
        {
            currentFrame = 0;
            frameTimer = TimeSpan.Zero;
        }
        public bool IsAnimationEnd()
        {
            if (currentFrame == 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Interface Method Region

        public object Clone()
        {
            Animation animationClone = new Animation(this);
            animationClone.frameWidth = this.frameWidth;
            animationClone.frameHeight = this.frameHeight;
            animationClone.Reset();

            return animationClone;
        }

        #endregion

    }
}