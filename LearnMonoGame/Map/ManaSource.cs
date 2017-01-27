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
            Rectangle a = new Rectangle(player.Bounds.X + player.Bounds.Width / 2, player.Bounds.Y + player.Bounds.Height, player.Bounds.Width / 4, player.Bounds.Height / 4);
            help = a;
            //DEBUG
            //Texture2D rectangle = new Texture2D(_MapStuff.Instance.graphics, bounds.Width, bounds.Height);
            //Color[] data = new Color[bounds.Width * bounds.Height];
            //for (int i = 0; i < data.Length; i++) data[i] = Color.Chocolate;
            //rectangle.SetData(data);
            //spriteBatch.Draw(rectangle, new Vector2(bounds.X, bounds.Y), Color.White);
            //
            //Texture2D rectangle1 = new Texture2D(_MapStuff.Instance.graphics, help.Width, help.Height);
            //Color[] data1 = new Color[help.Width * help.Height];
            //for (int i = 0; i < data1.Length; i++) data1[i] = Color.Red;
            //rectangle1.SetData(data1);
            //spriteBatch.Draw(rectangle1, new Vector2(help.X,help.Y), Color.White);
            if(giveMana)
                animatedSprite.Draw(spriteBatch);
        }

    }
}
