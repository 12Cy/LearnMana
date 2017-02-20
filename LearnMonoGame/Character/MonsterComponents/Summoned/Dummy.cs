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
    class Dummy : Character
    {
        static int ID = 0;
        public Dummy(Vector2 _position) : base(SummonedsInformation.Instance.characterInformation["Dummy"])
        {
            id = "DM" + ID++;
            pos = _position;

            creatureTexture = _CM.GetTexture(_CM.TextureName.skelett);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            selectedTexture = _CM.GetTexture(_CM.TextureName.selected);

            characterTyp = ECharacterTyp.summoned;
            element = EElement.none;

            Initialize();
            Console.WriteLine("Dummy has beend created");
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
            
            MouseState aMouse = Mouse.GetState();

            if (IsSelect && aMouse.RightButton == ButtonState.Pressed)
            {
                
                moveDestination = new Vector2((int)posDestination.X - width/2, (int)posDestination.Y - height);
                isRunning = true;
                moveDestinationAnimation.IsAnimating = true;
                moveDestinationAnimation.Position = new Vector2(moveDestination.X + 16, moveDestination.Y + 48);

            }

            Vector2 dif = moveDestination - pos; //VerbindungsVektor
            Move(gameTime, dif);
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

