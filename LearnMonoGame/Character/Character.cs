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
    //TODO: In eine extra Klassen-Datei
    public class TimerMove
    {
        public SAbility effect;
        double timerDuration;
        double timerTrigger;

        public TimerMove(SAbility _effect, float duration)
        {
            timerTrigger = 0;
            effect = _effect;
            timerDuration = duration;
        }

        public bool Trigger(GameTime gTime)
        {
            timerTrigger += gTime.ElapsedGameTime.TotalSeconds;
            if (timerTrigger >= effect.trigger)
            {
                timerTrigger = 0;
                return true;
            }
            return false;
        }

        public bool Update(GameTime gTime)
        {
            timerDuration -= gTime.ElapsedGameTime.TotalSeconds;
            if (timerDuration > 0) return false;
            return true;
        }
    }

    //TODO: Ab In die Waffen-Klasse
    public enum EWeaponStatus
    {
        NoTarget,
        TargetFound,
        Channel,
        Attack
    }
    public enum ECharacterTyp
    {
        player,
        enemy,
        summoned
    }
    //TODO: Wie in SABility, Element in ein Array
    public enum EElement
    {
        Dark, Earth, Fire, Light, Water, Wind, none
    }
    public abstract class Character
    {

        #region Variable
        //TODO: Bitte Löschen ... Direkt .GetTexture nutzen
        protected Texture2D creatureTexture;
        protected AnimatedSprite animatedSprite;

        //TODO: CleanUp, Überlegen ob benötigt.
        protected Texture2D moveAnimation;
        protected AnimatedSprite moveDestinationAnimation;

        
        protected Rectangle hitBox;

        //selected
        protected Texture2D selectedTexture;
        protected bool isSelected;
        protected TimeSpan hitInformationTImer;
        //TODO: Nochmal über den Sinn nachdenken!
        protected Texture2D damageselectedTexture;

        //TODO: Status-Klasse für alle Booooooleans

        //Movement
        protected Vector2 pos;
        protected Vector2 moveDestination; //Move richtung
        protected Vector2 posDestination; // position des Ziels!
        protected bool isRunning; 

        //Life
        //TODO: Current-Attribute Klassen
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
        //TODO: Auf späteres Meeting verschoben
        protected int width;
        protected int height;
        protected int attackDamage;
        protected float defense;

        protected EElement element;
        protected ECharacterTyp characterTyp;

        //Offset
        //TODO: Nochmal drüber Nachdenken (float,int) Offset
        //TODO: Umbenennen
        protected float healthBarOffset = 0.5f;
        protected int offsetHeight = 10; //Gibt die Height der LB Texture an!

        
        protected List<TimerMove> effects;
        protected List<TimerMove> delayEffects;

        //Modifier

        //TODO: In Attributes auslagern
        protected float realSpeed;
        protected float realAttackSpeed;
        protected int realAttackDamage;
        protected float realDefensiv;

        //Weapon,Spell
        protected Spellbook spellBook;
        protected Weapon weapon;
        //TODO: Eventuell mit ECharacterType mergen
        protected EAlignment alignment; //Beeiflusst die Waffe, welche Ziele sie angreift.
        //TODO: Eventuell in Weapon auslagern
        protected EWeaponStatus weaponStatus;


        protected string id;

        #endregion

        #region properties

        public ECharacterTyp CharacterTyp { get { return characterTyp; } }
        public List<TimerMove> Effects { get { return effects; } }
        public Vector2 Pos { get { return pos; } }
        public bool IsSelect { get { return isSelected; } set { isSelected = value; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        public Rectangle HitBox { get { return hitBox; } }
        public float RealSpeed { get { return realSpeed; } }
        public float RealDefensiv { get { return realDefensiv; } }
        public int RealAttackDamage { get { return realAttackDamage; } }
        public float RealAttackSpeed { get { return realAttackSpeed; } }
        public Vector2 PosDestination { get { return posDestination; } set { posDestination = value; } }

        public string GetID { get { return id; } }

        public float CurrMana { get { return currentMana; } }



        #endregion

        #region Constructor

        public Character(Attributes info)
        {
            // --- Attributes --- (Hole mir alle Informationen von SummonedsInformation für das jeweilige Monster)

            speed = info.Speed;
            width = info.Width;
            height = info.Height;
            maxHealth = info.MaxHealth;
            defense = info.Defense;
            maxMana = info.MaxMana;

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            effects = new List<TimerMove>();
            delayEffects = new List<TimerMove>();

            // --- Life ---
            currentHealth = maxHealth;
            hit = false;
            isAlive = true;
            isSelected = false;
            hitInformationTImer = TimeSpan.Zero;

            isRunning = false;
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
            UpdateEffects(gameTime);

            if (weapon != null)
                WeaponUpdate(gameTime, alignment);

            //TODO: Hit Implementieren
            if (hit)
            {//Wenn der Spieler getroffen wurde, wird der LB angezeigt (für 1 Sekunde)
                hitInformationTImer += gameTime.ElapsedGameTime;

                if (hitInformationTImer > TimeSpan.FromSeconds(1))
                {//1 Sekunde ist um, LB verschwindet

                    hitInformationTImer = TimeSpan.Zero;
                    hit = false;
                }


            }

            

            realAttackDamage = attackDamage;
            realDefensiv = defense;
            realSpeed = speed;
            realAttackSpeed = attackSpeed;

            //TODO: Collider überarbeiten (CHRIS!)
            hitBox = new Rectangle(pos.ToPoint(), new Point(width, height));


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
                isRunning = false;
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
                motion *= (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                Vector2 newPosition = animatedSprite.Position + motion; // the position we are moving to is valid?


                //TODO: Map-Collider (Matthis)
                if (_MapStuff.Instance.map.Walkable(newPosition)
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(width, 0))
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(0, height))
                  && _MapStuff.Instance.map.Walkable(newPosition + new Vector2(width, height)))
                {//Ist dort keine Collision?
                    //TODO: Matthis! Überarbeiten, Auslagern Collision-Check
                    if(characterTyp == ECharacterTyp.player)
                    {
                        foreach(Character b in MonsterManager.Instance.enemyList)
                        {
                            if (SAT.AreColliding(new Rectangle((int)newPosition.X, (int)newPosition.Y, this.hitBox.Width, this.hitBox.Height), b.hitBox))
                                return;
                        }
                        foreach(Character c in MonsterManager.Instance.mySummoned)
                        {
                            if (SAT.AreColliding(new Rectangle((int)newPosition.X, (int)newPosition.Y, this.hitBox.Width, this.hitBox.Height), c.hitBox))
                                return;
                        }
                    }
                    if(characterTyp == ECharacterTyp.enemy)
                    {
                        foreach (Character b in MonsterManager.Instance.enemyList)
                        {
                            foreach (Character c in MonsterManager.Instance.mySummoned)
                            {
                                //ToDO Summoned einstellen
                            }

                            //ToDo Enemys blocken Enemys

                            if (SAT.AreColliding(b.hitBox, PlayerManager.Instance.MyPlayer.hitBox))
                                return;
                        }
                    }
                    if(characterTyp == ECharacterTyp.summoned)
                    {
                        foreach(Character c in MonsterManager.Instance.mySummoned)
                        {
                            foreach(Character b in MonsterManager.Instance.enemyList)
                            {
                                //TODO: EnemyList einstellen
                            }

                            //ToDo Summoned blockt Summoned

                            if (SAT.AreColliding(new Rectangle((int)newPosition.X, (int)newPosition.Y, c.hitBox.Width, c.hitBox.Height), PlayerManager.Instance.MyPlayer.hitBox))
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
            animatedSprite.Draw(spriteBatch);
            if(isRunning && isSelected)
                moveDestinationAnimation.Draw(spriteBatch);

            //LB
            if (isSelected)
            {
                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);

                if (characterTyp == ECharacterTyp.summoned || characterTyp == ECharacterTyp.player)
                    spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Aquamarine);
                else
                    spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Red);

                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);


                //if(isSelected)
                //    spriteBatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);
                if (hit)
                    spriteBatch.Draw(damageselectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);
                else
                    spriteBatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);

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
            currentHealth += value;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            if (currentHealth < 0)
            {
                currentHealth = 0;
                isAlive = false;
            }


        }

        public void CalculateMana(float value)
        {
            value = (int)value;
            if (value > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, value.ToString(), Color.DarkBlue));
            else if (value < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(pos + new Vector2(animatedSprite.Width / 2, animatedSprite.Height / 2) - new Vector2(10, 20), 2f, value.ToString(), Color.LightBlue));
            currentMana += value;
            if (currentMana > maxMana)
                currentMana = maxMana;

            if (currentMana < 0)
            {
                currentMana = 0;
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
            attackDamage += iMove.attackDamage[0];
            defense += iMove.defense[0];
            speed += iMove.speed[0];
            attackSpeed += iMove.attackSpeed[0];
        }

        void ReRollEffect(SAbility iMove)
        {
            attackDamage -= iMove.attackDamage[0];
            defense -= iMove.defense[0];
            speed -= iMove.speed[0];
            attackSpeed -= iMove.attackSpeed[0];
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