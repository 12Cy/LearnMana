using LearnMonoGame.Summoneds;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Particle
{
    class SimpleParticle
    {
        float timer;
        float maxTimer;
        Texture2D sprite;
        Vector2 position;
        public bool alive = true;
        Character character;
        AnimatedSprite animatedSprite;

        public SimpleParticle(Texture2D _sprite, Vector2 _position, float duration, Character _character, Dictionary<AnimationKey, Animation> dic)
        {

            timer = 0;
            maxTimer = duration;
            sprite = _sprite;
            //position = _position - (_sprite.Bounds.Size.ToVector2() / 2);

            character = _character;
            //position = new Vector2(character.Bounds.X, character.Bounds.Y);
            position = new Vector2(character.Bounds.X - character.Width / 2, character.Bounds.Y - character.Height/2 * 4 );
            animatedSprite = new AnimatedSprite(_sprite, dic);
            animatedSprite.CurrentAnimation = AnimationKey.effect;
            animatedSprite.Position = position;
        }

        public void Update(GameTime gTime)
        {
            //position = new Vector2(character.Bounds.X, character.Bounds.Y);
            animatedSprite.Position = new Vector2(character.Bounds.X - character.Width / 2, character.Bounds.Y - character.Height/2 *4);
            animatedSprite.IsAnimating = true;
            timer += (float) gTime.ElapsedGameTime.TotalSeconds;

            if (timer > maxTimer)
            {
                alive = false;
                animatedSprite.IsAnimating = false;
            }
            animatedSprite.Update(gTime);
               
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch);
        }
    }
}
