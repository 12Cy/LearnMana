using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Spells;

namespace LearnMonoGame.Manager
{
    class BulletManager
    {
        public List<Bullet> bullets = new List<Bullet>();


        static BulletManager instance;
        public static BulletManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new BulletManager();

                return instance;
            }
        }
    }
}
