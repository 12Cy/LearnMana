using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies
{
    enum EElement
    {
        Dark, Earth, Fire, Light, Water, Wind
    }
    public abstract class Enemy
    {

#region Variable

        protected Texture2D creatureTexture;
        protected AnimatedSprite animatedSprite;
        protected Rectangle bounds;

        // --- Movement ---
        protected Vector2 pos;
        protected Vector2 moveDestination;

        // --- Life ---
        protected Texture2D lifeTexture;
        protected bool isAlive;
        protected bool hit;
        protected TimeSpan hitTimer;
        protected int currentHealth;

        // --- Attributes ---
        protected int maxHealth;
        protected float speed;
        protected int damage;
        protected int defense;
        protected float attackSpeed;
        protected int width;
        protected int height;

        protected int costToBuy;

        // --- Offset ---
        protected float offset = 0.5f;
        protected int offsetHeight = 10; //Gibt die Height der LB Texture an!

        // --- Fight ---
        protected float radius;


        // --- EP ---
        protected int level;
        protected float experience;

        EElement element;
        // --- Enum ---
        protected List<IMove> effects;
        protected Dictionary<string, IMove> knownMoves;
        TimeSpan ticks;

#endregion

#region properties

        public Vector2 Pos { get { return pos; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }


        public List<IMove> Effects
        {
            get { return effects; }
        }

        public Dictionary<string, IMove> KnownMoves
        {
            get { return knownMoves; }
        }
        public Rectangle Bounds { get { return bounds; } }

        #endregion
        #region Constructor

        public Enemy(Attributes info)
        {
            // --- Attributes --- (Hole mir alle Informationen von SummonedsInformation für das jeweilige Monster)

            speed = info.Speed;
            width = info.Width;
            height = info.Height;
            maxHealth = info.MaxHealth;
            defense = info.Defense;
            damage = info.Damage;

            bounds = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            level = 1;
            experience = 0;
            ticks = TimeSpan.Zero;


            knownMoves = new Dictionary<string, IMove>();
            effects = new List<IMove>();

            // --- Life ---
            currentHealth = maxHealth;
            hit = false;
            hitTimer = TimeSpan.Zero;
            isAlive = true;
            MoveManager.FillMoves();
        }

        #endregion


        protected virtual void Initialize() { }

        public virtual void Update(GameTime gameTime)
        {
            bounds = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            ticks += gameTime.ElapsedGameTime;

            if (ticks > TimeSpan.FromSeconds(2))
            {//1 Sekunde ist um, LB verschwindet
                ticks = TimeSpan.Zero;
                for (int i = 0; i < effects.Count; i++)
                {
                    effects[i].Duration--;
                    MoveManager.debugshitDuration = effects[i].Duration;
                    if (effects[i].Name == "Hot")
                        CalculateHealth(effects[i].Health);


                    if (effects[i].Duration < 1)
                    {
                        effects.RemoveAt(i);
                        i--;
                    }
                }
                
            }


            if (hit)
            {//Wenn der Spieler getroffen wurde, wird der LB angezeigt (für 1 Sekunde)
                hitTimer += gameTime.ElapsedGameTime;

                if (hitTimer > TimeSpan.FromSeconds(2))
                {//1 Sekunde ist um, LB verschwindet

                    hitTimer = TimeSpan.Zero;
                    hit = false;
                }
            }
        }
        public void ResolveMove(IMove move)//, Summoned target)
        {
            bool found = false;
            switch (move.Target)
            {
                case ETarget.Self:
                    if (move.MoveType == EMoveType.Buff)
                    {
                        found = false;
                        for (int i = 0; i < effects.Count; i++)
                        {
                            if (effects[i].Name == move.Name)
                            {
                                effects[i].Duration += move.Duration;
                                found = true;
                            }
                        }

                        if (!found)
                            effects.Add((IMove)move.Clone());
                    }
                    else if (move.MoveType == EMoveType.Heal)
                    {
                        CalculateHealth(move.Health);

                    }
                    else if (move.MoveType == EMoveType.Status)
                    {
                    }

                    break;
                //case ETarget.Enemy:
                //    if (move.MoveType == EMoveType.Debuff)
                //    {
                //        found = false;
                //        for (int i = 0; i < target.Effects.Count; i++)
                //        {
                //            if (target.Effects[i].Name == move.Name)
                //            {
                //                target.Effects[i].Duration += move.Duration;
                //                found = true;
                //            }
                //        }
                //
                //        if (!found)
                //            target.Effects.Add((IMove)move.Clone());
                //    }
                //    else if (move.MoveType == EMoveType.Attack)
                //    {
                //        //ToDo: modifiert berechnen (Schatten Licht etc)
                //
                //        float tDamage = CalculateDamage() + move.Health *  - target.CalculateDefensiv();
                //
                //        if (tDamage < 1f)
                //            tDamage = 1f;
                //
                //        target.CalculateHealth(-(int)tDamage);
                //    }
                //
                //  break;
            }
        }

        public int CalculateDamage()
        {
            int attackMod = 0;

            foreach (IMove move in effects)
            {
                if (move.MoveType == EMoveType.Buff)
                    attackMod += move.Attack;

                if (move.MoveType == EMoveType.Debuff)
                    attackMod -= move.Attack;
            }

            return damage + attackMod;
        }

        public int CalculateDefensiv()
        {
            int defenseMod = 0;

            foreach (IMove move in effects)
            {
                if (move.MoveType == EMoveType.Buff)
                    defenseMod += move.Defense;

                if (move.MoveType == EMoveType.Debuff)
                    defenseMod -= move.Defense;
            }

            return defense + defenseMod;
        }

        public float CalculateSpeed()
        {
            int speedMod = 0;

            foreach (IMove move in effects)
            {
                if (move.MoveType == EMoveType.Buff)
                    speedMod += move.Speed;
                if (move.MoveType == EMoveType.Debuff)
                    speedMod -= move.Speed;
            }

            return speed + speedMod;
        }

        public int CalculateHealth()
        {
            int healthMod = 0;

            foreach (IMove move in effects)
            {
                if (move.MoveType == EMoveType.Buff)
                    healthMod += move.Health;
                if (move.MoveType == EMoveType.Debuff)
                    healthMod += move.Health;
            }

            return currentHealth + healthMod;
        }

        public virtual void CalculateHealth(int value)
        {
            hit = true;
            currentHealth += value;
            if (currentHealth > 100)
                currentHealth = 100;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;
                
            }


        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch);

            spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
            spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Red);
            spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
        }











        }
}
