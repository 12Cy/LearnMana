using LearnMonoGame.Manager;
using LearnMonoGame.Spells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Particle
{
    class PopUpText : GameParticle
    {
        float timer, maxTimer;
        string text;
        Vector2 pos;
        float speed;
        Color color;
        float alpha;
        SpriteFont font;
        Vector2 sizeLerp, endSize;
        Vector2 textSize;

        public PopUpText(Vector2 _pos, float _duration, string _text, Color _color, float _speed = 10)
        {
            font = _CM.GetFont(_CM.FontName.DamagePopUp);
            alpha = 0;
            alive = true;
            timer = 0;
            maxTimer = _duration;
            text = _text;

            int offsetX = SpellManager.Instance.rnd.Next(-15, 15);
            int offsetY = SpellManager.Instance.rnd.Next(-15, 15);


            pos = _pos + new Vector2(offsetX,offsetY);
            endSize = new Vector2(1,1);
            sizeLerp = endSize * 5;
            textSize = font.MeasureString(text);
            color = _color;
            speed = _speed;

        }

        public override void Update(GameTime gTime)
        {
            timer += (float)gTime.ElapsedGameTime.TotalSeconds;

            pos.Y -= speed * (float)gTime.ElapsedGameTime.TotalSeconds;
            sizeLerp = Vector2.Lerp(sizeLerp, endSize, 0.15f);

            alpha = 1 - (timer / maxTimer);

            if (timer > maxTimer)
            {
                alive = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, pos, new Color(color,alpha),0,textSize / 2,sizeLerp,SpriteEffects.None,0);
        }
    }
}
