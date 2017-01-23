using LearnMonoGame.Components;
using LearnMonoGame.Manager;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    public enum ECharacterTyp
    {
        player,
        enemy,
        summoned
    }
    public enum EElement
    {
        Dark, Earth, Fire, Light, Water, Wind, none
    }
    public abstract class Character
    {

#region Variable

        protected Texture2D creatureTexture;
        protected AnimatedSprite animatedSprite;
        protected Rectangle bounds;

        //selected
        protected Texture2D selectedTexture;
        protected bool isSelected;
        protected TimeSpan hitTimer;
        protected Texture2D damageselectedTexture;

        //Movement
        protected Vector2 pos;
        protected Vector2 moveDestination;

        //Life
        protected Texture2D lifeTexture;
        protected bool isAlive;
        protected bool hit;
        protected float currentHealth;
        protected float currentMana;
        protected float maxMana;

        //Attributes
        protected int maxHealth;
        protected float speed;
        protected float attackSpeed;
        protected int width;
        protected int height;
        protected int attackDamage;
        protected float defense;

        // --- Fight ---
        protected float radius;

        // --- EP ---
        protected int level;
        protected float experience;

        protected EElement element;
        protected ECharacterTyp characterTyp;

        //Offset
        protected float offset = 0.5f;
        protected int offsetHeight = 10; //Gibt die Height der LB Texture an!

        protected List<IMove> effects;

        //Modifier


        protected float realSpeed;
        protected float realAttackSpeed;
        protected int realAttackDamage;
        protected float realDefensiv;



        #endregion

        #region properties

        public ECharacterTyp CharacterTyp { get { return characterTyp; } }
        public List<IMove> Effects { get { return effects; } }
        public Vector2 Pos { get { return pos; } }
        public bool IsSelect { get { return isSelected; } set { isSelected = value; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        public Rectangle Bounds { get { return bounds; } }
        public float RealSpeed { get { return realSpeed; }}
        public float RealDefensiv {get { return realDefensiv; }}
        public int RealAttackDamage{get { return realAttackDamage; }}
        public float RealAttackSpeed{get { return realAttackSpeed; }}

        #endregion

        #region Constructor


        public Character(Attributes info)
        {
            // --- Attributes --- (Hole mir alle Informationen von SummonedsInformation für das jeweilige Monster)

            speed = info.Speed;
            width = info.Width;
            height = info.Height;
            maxHealth = info.MaxHealth;
            attackDamage = info.Damage;
            defense = info.Defense;

            bounds = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            level = 1;
            experience = 0;
            effects = new List<IMove>();

            // --- Life ---
            currentHealth = maxHealth;
            hit = false;
            isAlive = true;
            isSelected = false;
            hitTimer = TimeSpan.Zero;

        }
#endregion

#region Methoden

        protected virtual void Initialize() { }

        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime)
        {
            if(this.characterTyp == ECharacterTyp.summoned)
                Move(gameTime);

            if (hit)
            {//Wenn der Spieler getroffen wurde, wird der LB angezeigt (für 1 Sekunde)
                hitTimer += gameTime.ElapsedGameTime;

                if (hitTimer > TimeSpan.FromSeconds(1))
                {//1 Sekunde ist um, LB verschwindet

                    hitTimer = TimeSpan.Zero;
                    hit = false;
                }


            }

            realAttackDamage = attackDamage;
            realDefensiv = defense;
            realSpeed = speed;
            realAttackSpeed = attackSpeed;

            //Update EffectList
            for (int i = 0; i < effects.Count; i++)
            {

                effects[i].Update(gameTime);


                CalculateHealth(effects[i].damage);
                CalculateMana(effects[i].mana);

                attackDamage += effects[i].attackDamage;
                defense += effects[i].defense;
                speed += effects[i].speed;
                attackSpeed += effects[i].attackSpeed;





                if (!effects[i].isAlive)
                    effects.RemoveAt(i--);
            }


            bounds = new Rectangle(pos.ToPoint(), new Point(width, height));
        }
        protected void Move(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();

            if (isSelected && aMouse.RightButton == ButtonState.Pressed)
            {
                moveDestination = new Vector2((int)xIn.MousePosition.X, (int)xIn.MousePosition.Y);
            }
            Vector2 dif = moveDestination - pos; //VerbindungsVektor

            if (dif.Length() < 3f)
            {//Ziel angekommen?

                moveDestination = pos;
                dif = Vector2.Zero;
                return;
            }
            Vector2 motion = Vector2.Normalize(dif);

            if (motion != Vector2.Zero)
            {//Soll ich mich bewegen?

                if (motion.X > 0)
                    animatedSprite.CurrentAnimation = AnimationKey.WalkRight;

                else
                    animatedSprite.CurrentAnimation = AnimationKey.WalkLeft;

                if (Math.Abs(motion.X / motion.Y) < offset)
                {
                    if (motion.Y > 0)
                        animatedSprite.CurrentAnimation = AnimationKey.WalkDown;
                    else
                        animatedSprite.CurrentAnimation = AnimationKey.WalkUp;
                }
            }
            //Movement calculated
            if (motion != Vector2.Zero)
            {
                //motion.Normalize();
                motion *= (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                Vector2 newPosition = animatedSprite.Position + motion; // the position we are moving to is valid?

                if (_MapStuff.Instance.map.Walkable(newPosition)
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(width, 0))
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(0, height))
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(width, height)))
                {//Ist dort keine Collision?

                    animatedSprite.Position = newPosition;
                    pos = newPosition;
                    animatedSprite.IsAnimating = true;
                }
                else
                {//Collision vorhanden

                    animatedSprite.ResetAnimation();
                    animatedSprite.IsAnimating = false;
                }

                //ToDo: PATHFINDER

            }
            else
            {
                animatedSprite.ResetAnimation();
                animatedSprite.IsAnimating = false;
            }

            animatedSprite.Update(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch);
            //LB
            if (isSelected || hit)
            {
                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Gainsboro);
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
                if (isSelected)
                    spriteBatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);

            }
        }
        /// <CalculateHealth>
        /// need - or + Value
        /// </CalculateHealth>
        public virtual void CalculateHealth(float value)
        {
            currentHealth += value;
            if (currentHealth > 100)
                currentHealth = 100;

            if (currentHealth < 0)
                currentHealth = 0;

        }
        /// <CalculateMana>
        /// need - or + Value
        /// </CalculateMana>
        public void CalculateMana(float value)
        {
            currentMana += value;
            if (currentMana > 100)
                currentMana = 100;

            if (currentMana < 0)
                currentMana = 0;

        }

        public void ApplyEffect(IMove iMove)
        {
            Console.WriteLine("ApplyEffect");
            if (iMove.moveType == EMoveType.Attack)
                CalculateHealth(iMove.damage);
            if (iMove.moveType == EMoveType.Heal)
                CalculateHealth(-iMove.health);
            if (iMove.moveType == EMoveType.Effect)
                effects.Add(iMove);
        }

        #endregion
    }
}