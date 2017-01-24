using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearnMonoGame.Tools
{
    public enum AnimationKey
    {
        IdleLeft,
        IdleRight,
        IdleDown,
        IdleUp,
        WalkLeft,
        WalkRight,
        WalkDown,
        WalkUp,
        ThrowLeft,
        ThrowRight,
        DuckLeft,
        DuckRight,
        JumpLeft,
        JumpRight,
        Dieing,

        //Spells
        heal,
        burn,
        tornado

    }
    public class AnimatedSprite
    {

        #region Field Region

        Dictionary<AnimationKey, Animation> animations;
        AnimationKey currentAnimation;
        bool isAnimating; //returns if the animation should be played or not.

        Texture2D texture;
        public Vector2 Position;
        Vector2 velocity; // Velocity is a normalized vector that holds the direction the sprite is travelling
        float speed = 200.0f; // Speed holds how far the sprite travels in that direction

        #endregion

        #region Property Region

        public bool IsActive { get; set; } //if the sprite is active or not.

        public AnimationKey CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public int Width
        {
            get { return animations[currentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return animations[currentAnimation].FrameHeight; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 400.0f); }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        #endregion

        #region Constructor Region

        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation)
        {
            texture = sprite;
            animations = new Dictionary<AnimationKey, Animation>();
            foreach (AnimationKey key in animation.Keys)
                animations.Add(key, (Animation)animation[key].Clone());
        }
        #endregion

        #region Method Region

        public void ResetAnimation()
        {
            animations[currentAnimation].Reset();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (isAnimating)
                animations[currentAnimation].Update(gameTime);
        }

        public virtual void Draw( SpriteBatch spriteBatch) //vGameTime gameTime
        {

            spriteBatch.Draw(texture, Position, animations[currentAnimation].CurrentFrameRect, Color.White);
        }

        public void LockToMap(Point mapSize)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, mapSize.X - Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, mapSize.Y - Height);
        }

        #endregion
    }
}
