using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Manager;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework.Input;
using LearnMonoGame.Components;

namespace LearnMonoGame.Summoneds
{
    class Dummy : Summoned
    {

        public Dummy(Vector2 _position) : base(SummonedsInformation.Instance.dummyInformation)
        {

            pos = _position;

            creatureTexture = _CM.GetTexture(_CM.TextureName.dummy);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            selectedTexture = _CM.GetTexture(_CM.TextureName.selected);
            Initialize();
            Console.WriteLine("created Dummy");
        }
        protected override void Initialize()
        {
            moveDestination = pos;

            // --- Animation ---
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.dummy));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;

        }

        public override void UnloadContent() { }

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

