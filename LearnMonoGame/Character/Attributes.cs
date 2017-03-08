using LearnMonoGame.Manager;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    public class Attributes
    {
        float speed = 180;
        int maxHealth = 100;
        float defense = 1;
        float maxMana = 100;
        float attackSpeed = 0;
        int attackDamage = 0;

        float currentHealth;
        float currentMana;

        float realSpeed;
        float realAttackSpeed;
        int realAttackDamage;
        float realDefensiv;

        EAlignment alignment; //Beeiflusst die Waffe, welche Ziele sie angreift.

        //TODO: Richtige CharacterGrößen anpassen
        int width = 32;
        int height = 64;


        public float Speed { get { return speed; } set { speed = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        public float Defense { get { return defense; } set { defense = value; } }
        public float MaxMana { get { return maxMana; } set { maxMana = value; } }
        public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
        public float CurrentMana { get { return currentMana; } set { currentMana = value; } }
        public int AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

        public float RealSpeed { get { return realSpeed; } set { realSpeed = value; } }
        public float RealAttackSpeed { get { return realAttackSpeed; } set { realAttackSpeed = value; } }
        public int RealAttackDamage { get { return realAttackDamage; } set { realAttackDamage = value; } }
        public float RealDefensiv { get { return realDefensiv; } set { realDefensiv = value; } }

        public EAlignment Alignment { get { return alignment; } set { alignment = value; } }
    

        public Attributes(float _speed = 180, int _maxHealth = 100,float _attackSpeed = 0, float _defense = 1, float _maxMana = 100, Point _size = new Point(), EAlignment _alignment = EAlignment.All)
        {
            alignment = _alignment;
            width = _size.X;
            height = _size.Y;
            speed = _speed;
            maxHealth = _maxHealth;
            defense = _defense;
            maxMana = _maxMana;


            currentMana = maxMana;
            currentHealth = maxHealth;
        }

        public Attributes(Attributes attr)
            : this(attr.speed, attr.maxHealth,attr.attackSpeed, attr.defense, attr.maxMana, new Point(attr.width,attr.height))
        {

        }

        public void ResetRealValues()
        {

            RealAttackDamage = attackDamage;
            RealAttackSpeed = attackSpeed;
            RealDefensiv = Defense;
            RealSpeed = speed;
        }
    }
}
