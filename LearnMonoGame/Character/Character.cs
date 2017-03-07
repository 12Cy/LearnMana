using LearnMonoGame.Components;
using LearnMonoGame.Manager;
using LearnMonoGame.Map;
using LearnMonoGame.Particle;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Spells;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Tools;
using LearnMonoGame.Tools.Collider;
using LearnMonoGame.Weapons;
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
    public abstract class Character
    {

        #region Variable
        protected Texture2D creatureTexture;
        protected AnimatedSprite animatedSprite;

        //TODO: CleanUp, Überlegen ob benötigt.
        protected Texture2D moveAnimation;
        protected AnimatedSprite moveDestinationAnimation;

        
        protected Rectangle hitBox;

        //selected
        protected Texture2D selectedTexture;
        protected TimeSpan hitInformationTImer;
        //TODO: Nochmal über den Sinn nachdenken!
        protected Texture2D damageselectedTexture;

        //TODO: Status-Klasse für alle Booooooleans
        public CharacterBool statusClass;

        //Movement
        protected Vector2 pos;
        protected Vector2 moveDestination; //Move richtung
        protected Vector2 posDestination; // position des Ziels!


        //Life
        //TODO: Current-Attribute Klassen
        protected Texture2D lifeTexture;

        //Offset
        //TODO: Nochmal drüber Nachdenken (float,int) Offset
        //TODO: Umbenennen
        protected float healthBarOffset = 0.5f;
        protected int offsetHeight = 10; //Gibt die Height der LB Texture an!

        
        protected List<TimerMove> effects;
        protected List<TimerMove> delayEffects;

        //Modifier

        //TODO: In Attributes auslagern
        protected Attributes attributes;

        //Weapon,Spell
        protected Spellbook spellBook;
        protected Weapon weapon;
        //TODO: Eventuell in Weapon auslagern
        protected EWeaponStatus weaponStatus;

        Rectangle collider = new Rectangle();
        protected string id;

        #endregion

        #region properties

        public EAlignment CharacterTyp { get { return attributes.Alignment; } }
        public List<TimerMove> Effects { get { return effects; } }
        public Vector2 Pos { get { return pos; } }
        public bool IsSelect { get { return statusClass.isSelected; } set { statusClass.isSelected = value; } }
        public int Width { get { return attributes.Width; } }
        public int Height { get { return attributes.Height; } }
        public bool IsAlive { get { return statusClass.isAlive; } set { statusClass.isAlive = value; } }
        public Rectangle HitBox { get { return hitBox; } }
        public float RealSpeed { get { return attributes.RealSpeed; } }
        public float RealDefensiv { get { return attributes.RealDefensiv; } }
        public int RealAttackDamage { get { return attributes.RealAttackDamage; } }
        public float RealAttackSpeed { get { return attributes.RealAttackSpeed; } }
        public Rectangle Collider { get { return collider; } set { collider = value; } }
        public Vector2 PosDestination { get { return posDestination; } set { posDestination = value; } }

        public string GetID { get { return id; } }

        public float CurrMana { get { return attributes.CurrentMana; } }



        #endregion

        #region Constructor

        public Character(Attributes info)
        {
            // --- Attributes --- (Hole mir alle Informationen von SummonedsInformation für das jeweilige Monster)

            attributes = new Attributes(info);

            statusClass = new CharacterBool();

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, attributes.Width, attributes.Height);
            collider = new Rectangle((int)pos.X, (int)pos.Y + attributes.Height /3 * 2, attributes.Width, attributes.Height /3);
            effects = new List<TimerMove>();
            delayEffects = new List<TimerMove>();

            // --- Life ---
            attributes.CurrentHealth = attributes.MaxHealth;
            statusClass.hit = false;
            statusClass.isAlive = true;
            statusClass.isSelected = false;
            hitInformationTImer = TimeSpan.Zero;

            statusClass.isRunning = false;
            moveAnimation = _CM.GetTexture(_CM.TextureName.animationClick);

            moveDestinationAnimation = new AnimatedSprite(moveAnimation, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.move));
            moveDestinationAnimation.CurrentAnimation = AnimationKey.moveClick;

            //Weapon,Spell
            spellBook = new Spellbook();

        }
        #endregion

        #region Methoden

        protected virtual void Initialize() { }

        public virtual void UnloadContent() { }

        //TODO: In Math-Klasse
        float CalculateRandomValue(int[] ary)
        {
            int diff = ary[1] - ary[0];

            return SpellManager.Instance.rnd.Next(diff + 1) + ary[0];
        }

        float CalculateCritValue(SAbility effect)
        {
            int diff = (int)effect.crit[1] * 100 - (int)effect.crit[0] * 100;

            float value = SpellManager.Instance.rnd.Next(diff + 1) + effect.crit[0] * 100;

            if (SpellManager.Instance.rnd.Next(101) < effect.critChance)
                return value / 100f;
            else
                return 1;

        }

        public virtual void Update(GameTime gameTime)
        {
            spellBook.Update(gameTime);
            moveDestinationAnimation.Update(gameTime);
            collider = new Rectangle((int)pos.X + 10, (int)pos.Y + attributes.Height / 3 * 2, attributes.Width -20, attributes.Height / 3);
            UpdateEffects(gameTime);

            if (weapon != null)
                WeaponUpdate(gameTime, attributes.Alignment);

            //TODO: Hit Implementieren
            if (statusClass.hit)
            {//Wenn der Spieler getroffen wurde, wird der LB angezeigt (für 1 Sekunde)
                hitInformationTImer += gameTime.ElapsedGameTime;

                if (hitInformationTImer > TimeSpan.FromSeconds(1))
                {//1 Sekunde ist um, LB verschwindet

                    hitInformationTImer = TimeSpan.Zero;
                    statusClass.hit = false;
                }


            }




            //TODO: Collider überarbeiten (CHRIS!)
            hitBox = new Rectangle(pos.ToPoint(), new Point(attributes.Width, attributes.Height));


        }

        void UpdateEffects(GameTime gameTime)
        {
            for (int i = 0; i < delayEffects.Count; ++i)
            {
                if (delayEffects[i].Update(gameTime))
                {
                    ApplyEffectWithoutDelay(delayEffects[i].effect);
                    delayEffects.RemoveAt(i--);
                }
            }


            //Update EffectList
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].Trigger(gameTime))
                {
                    CalculateHealth(CalculateRandomValue(effects[i].effect.health) * CalculateCritValue(effects[i].effect));
                    CalculateHealth(-CalculateRandomValue(effects[i].effect.damage) * CalculateCritValue(effects[i].effect));
                    CalculateMana(CalculateRandomValue(effects[i].effect.mana) * CalculateCritValue(effects[i].effect));
                }


                if (effects[i].Update(gameTime))
                {
                    ReRollEffect(effects[i].effect);
                    effects.RemoveAt(i--);
                }
            }
        }

        protected virtual void WeaponUpdate(GameTime gameTime, EAlignment alignment)
        {
            switch (weaponStatus)
            {
                case EWeaponStatus.NoTarget:
                    if (weapon.CheckAttack(this, alignment))
                        weaponStatus = EWeaponStatus.TargetFound;
                    break;
                case EWeaponStatus.TargetFound:
                    if (!weapon.CheckAttack(this, alignment))
                        weaponStatus = EWeaponStatus.NoTarget;
                    break;
                case EWeaponStatus.Channel:
                    if (weapon.Channel(this, gameTime, alignment))
                    {
                        weaponStatus = EWeaponStatus.NoTarget;
                    }
                    break;
                default:
                    break;
            }
        }

        protected void Move(GameTime gameTime, Vector2 dif)
        {

            if (dif.Length() < 3f)
            {//Ziel angekommen?

                moveDestination = pos;
                dif = Vector2.Zero;
                statusClass.isRunning = false;
                return;
            }

            Vector2 motion = Vector2.Normalize(dif);

            if (motion != Vector2.Zero)
            {//Soll ich mich bewegen!

                if (motion.X > 0)
                    animatedSprite.CurrentAnimation = AnimationKey.WalkRight;

                else
                    animatedSprite.CurrentAnimation = AnimationKey.WalkLeft;

                if (Math.Abs(motion.X / motion.Y) < healthBarOffset)
                {
                    if (motion.Y > 0)
                        animatedSprite.CurrentAnimation = AnimationKey.WalkDown;
                    else
                        animatedSprite.CurrentAnimation = AnimationKey.WalkUp;
                }
                //motion.Normalize();
                motion *= (attributes.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                Vector2 newPosition = animatedSprite.Position + motion; // the position we are moving to is valid?
         



                //TODO: Map-Collider (Matthis)
                if (_MapStuff.Instance.map.Walkable(newPosition)
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(attributes.Width, 0))
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(0, attributes.Height))
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(attributes.Width, attributes.Height)))
                {//Ist dort keine Collision?
                    //TODO: Matthis! Überarbeiten, Auslagern Collision-Check Interface auslagern!
                    if(attributes.Alignment == EAlignment.Player)
                    {
                        foreach(Character b in MonsterManager.Instance.enemyList)
                        {
                            if (SAT.AreColliding(new Rectangle((int)collider.X + (int)motion.X, (int)collider.Y + (int)motion.Y, this.collider.Width, this.collider.Height), b.collider))
                                return;
                        }
                        foreach(Character c in MonsterManager.Instance.mySummoned)
                        {
                            if (SAT.AreColliding(new Rectangle((int)collider.X + (int)motion.X, (int)collider.Y + (int)motion.Y, this.collider.Width, this.collider.Height), c.collider))
                                return;
                        }
                    }
                    if(attributes.Alignment == EAlignment.Enemy)
                    {
                        foreach (Character b in MonsterManager.Instance.enemyList)
                        {
                            foreach (Character c in MonsterManager.Instance.mySummoned)
                            {
                                //ToDO Summoned einstellen
                            }

                            //ToDo Enemys blocken Enemys

                            if (SAT.AreColliding(b.hitBox, PlayerManager.Instance.MyPlayer.collider))
                                return;
                        }
                    }
                    if(attributes.Alignment == EAlignment.Summoned)
                    {
                        foreach(Character c in MonsterManager.Instance.mySummoned)
                        {
                            foreach(Character b in MonsterManager.Instance.enemyList)
                            {
                                //TODO: EnemyList einstellen
                            }

                            //ToDo Summoned blockt Summoned

                            if (SAT.AreColliding(new Rectangle((int)collider.X + (int)motion.X, (int)collider.Y + (int)motion.Y, c.collider.Width, c.collider.Height), PlayerManager.Instance.MyPlayer.collider))
                                return;

                        }
                    }
                        

                    foreach (ManaSource a in _MapStuff.Instance.manaSourceList)
                    {
                        a.CheckCollisionWithManaSource(this);
                    }


                    animatedSprite.Position = newPosition;
                    pos = newPosition;
                    animatedSprite.IsAnimating = true;
                }
                else
                {//Collision vorhanden

                    animatedSprite.ResetAnimation();
                    animatedSprite.IsAnimating = false;
                }

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
            Console.WriteLine(collider.Width + "-" + collider.Height);
            Texture2D rectangle = new Texture2D(_MapStuff.Instance.graphics, collider.Width, collider.Height);
            Color[] data = new Color[collider.Width * collider.Height];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Chocolate;
            rectangle.SetData(data);
            spriteBatch.Draw(rectangle, new Vector2((int)collider.X, (int)collider.Y), Color.Red);

            animatedSprite.Draw(spriteBatch);
            if(statusClass.isRunning && statusClass.isSelected)
                moveDestinationAnimation.Draw(spriteBatch);

            //LB
            if (statusClass.isSelected)
            {
                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - attributes.Height / 4 - 5, attributes.Width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);

                if (attributes.Alignment == EAlignment.Summoned || attributes.Alignment == EAlignment.Player)
                    spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - attributes.Height / 4 - 5, (int)(attributes.Width * ((float)attributes.CurrentHealth / attributes.MaxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Aquamarine);
                else
                    spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - attributes.Height / 4 - 5, (int)(attributes.Width * ((float)attributes.CurrentHealth / attributes.MaxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Red);

                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - attributes.Height / 4 - 5, attributes.Width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);


                //if(isSelected)
                //    spriteBatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);
                if (statusClass.hit)
                    spriteBatch.Draw(damageselectedTexture, new Rectangle((int)pos.X, (int)pos.Y, attributes.Width, attributes.Height), Color.White);
                else
                    spriteBatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, attributes.Width, attributes.Height), Color.White);

            }

        }

        //TODO: Auslagern in Attribute(z.B.)
        public virtual void CalculateHealth(float value)
        {
            value = (int)value;
            if (value > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, value.ToString(), Color.GreenYellow));
            else if (value < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, value.ToString(), Color.Red));
            attributes.CurrentHealth += value;
            if (attributes.CurrentHealth > attributes.MaxHealth)
                attributes.CurrentHealth = attributes.MaxHealth;

            if (attributes.CurrentHealth < 0)
            {
                attributes.CurrentHealth = 0;
                statusClass.isAlive = false;
            }


        }

        public void CalculateMana(float value)
        {
            value = (int)value;
            if (value > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, value.ToString(), Color.DarkBlue));
            else if (value < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, value.ToString(), Color.LightBlue));
            attributes.CurrentMana += value;
            if (attributes.CurrentMana > attributes.MaxMana)
                attributes.CurrentMana = attributes.MaxMana;

            if (attributes.CurrentMana < 0)
            {
                attributes.CurrentMana = 0;
            }


        }

        void ApplyEffectWithoutDelay(SAbility iMove)
        {
            if (SpellManager.Instance.rnd.Next(101) < iMove.spellChance)
                ApplyEffect(SpellManager.Instance.attackInformation[iMove.spell]);

            if (iMove.moveType == EMoveType.Attack)
            {
                CalculateHealth(-CalculateRandomValue(iMove.damage) * CalculateCritValue(iMove));
                CalculateMana(CalculateRandomValue(iMove.mana));
            }

            if (iMove.moveType == EMoveType.Heal)
                CalculateHealth(CalculateRandomValue(iMove.health) * CalculateCritValue(iMove));
            if (iMove.moveType == EMoveType.Effect)
                effects.Add(new TimerMove(iMove, iMove.duration));



            if (iMove.attackDamage[0] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.attackDamage[0].ToString(), Color.OrangeRed));
            else if (iMove.attackDamage[0] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.attackDamage[0].ToString(), Color.Orchid));

            if (iMove.defense[0] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.defense[0].ToString(), Color.Gray));
            else if (iMove.defense[0] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.defense[0].ToString(), Color.DarkSeaGreen));

            if (iMove.speed[0] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.speed[0].ToString(), Color.Yellow));
            else if (iMove.speed[0] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.speed[0].ToString(), Color.BlueViolet));

            if (iMove.attackSpeed[0] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.attackSpeed[0].ToString(), Color.BlanchedAlmond));
            else if (iMove.attackSpeed[0] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, iMove.attackSpeed[0].ToString(), Color.AliceBlue));


            //TODO: Random-Values hinzufügen
            attributes.AttackDamage += iMove.attackDamage[0];
            attributes.Defense += iMove.defense[0];
            attributes.Speed += iMove.speed[0];
            attributes.AttackSpeed += iMove.attackSpeed[0];
        }

        void ReRollEffect(SAbility iMove)
        {
            attributes.AttackDamage -= iMove.attackDamage[0];
            attributes.Defense -= iMove.defense[0];
            attributes.Speed -= iMove.speed[0];
            attributes.AttackSpeed -= iMove.attackSpeed[0];
        }

        public void ApplyEffect(SAbility iMove)
        {
            if (iMove.delay == 0)
            {
                ApplyEffectWithoutDelay(iMove);
            }
            else
            {
                delayEffects.Add(new TimerMove(iMove, iMove.delay));
            }
        }

        #endregion
    }
}