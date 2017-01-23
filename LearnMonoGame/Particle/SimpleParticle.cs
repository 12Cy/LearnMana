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

        public SimpleParticle(Texture2D _sprite, Vector2 _position, float duration)
        {
            timer = 0;
            maxTimer = duration;
            sprite = _sprite;
            position = _position - (_sprite.Bounds.Size.ToVector2() / 2);
        }

        public void Update(GameTime gTime)
        {
            timer += (float) gTime.ElapsedGameTime.TotalSeconds;

            if (timer > maxTimer)
                alive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,position,Color.White);
        }
    }
}
