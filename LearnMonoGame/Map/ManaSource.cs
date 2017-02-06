using LearnMonoGame.Manager;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Map
{
    class ManaSource
    {
        Vector2 pos;
        int width;
        int height;
        Rectangle bounds;
        Player player;
        bool giveMana = false;
        Rectangle help = new Rectangle(1, 1, 1, 1);
        AnimatedSprite animatedSprite;
        Texture2D texture;
        public ManaSource(Vector2 _pos)
        {
            player = PlayerManager.Instance.MyPlayer;
            texture = _CM.GetTexture(_CM.TextureName.manaSourceAnimation);



            pos = _pos;
            width = 64;
            height = 74;
            bounds = new Rectangle((int)pos.X + width/2 , (int)pos.Y + height/3, width/6, height/8);

            animatedSprite = new AnimatedSprite(_CM.GetTexture(_CM.TextureName.manaSourceAnimation), _AnimationManager.GetAnimation(_AnimationManager.AnimationName.effects));
            animatedSprite.CurrentAnimation = AnimationKey.manaSource;
            animatedSprite.Position = new Vector2(pos.X  - 20, pos.Y - texture.Height + 36);

        }
        public void Update(GameTime gameTime)
        {
            if (player == null)
                player = PlayerManager.Instance.MyPlayer;

            if (giveMana)
            {
                player.CalculateMana(1);
                animatedSprite.IsAnimating = true;
                animatedSprite.Update(gameTime);
                
            }

            else
            {
                animatedSprite.IsAnimating = false;
                animatedSprite.ResetAnimation();
            }
                

        }
        public void CheckCollisionWithManaSource(Character c)
        {
            if (c.CharacterTyp == ECharacterTyp.enemy)
                return;

            Rectangle a = new Rectangle(c.Bounds.X + c.Bounds.Width/2, c.Bounds.Y + c.Bounds.Height, c.Bounds.Width/4 , c.Bounds.Height/4);
            help = a;


            if (bounds.Intersects(a)){
                giveMana = true;
                
            }
            else 
                giveMana = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if(giveMana)
                animatedSprite.Draw(spriteBatch);
        }

    }
}
