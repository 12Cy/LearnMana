using LearnMonoGame.Manager;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies.Monster
{
    class Skelett : Enemy
    {
        
            

        public Skelett(Vector2 _pos) : base(SummonedsInformation.Instance.skelettInformation)
        {
            pos = _pos;
            creatureTexture = _CM.GetTexture(_CM.TextureName.dummy);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            Initialize();
        }
        protected override void Initialize()
        {
            moveDestination = pos;

            // --- Animation ---
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.dummy));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void CalculateHealth(float value)
        {
            base.CalculateHealth(value);
        }



    }
}
