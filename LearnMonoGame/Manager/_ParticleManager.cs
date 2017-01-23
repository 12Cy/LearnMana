using LearnMonoGame.Particle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Manager
{
    class _ParticleManager
    {
        public List<SimpleParticle> particles = new List<SimpleParticle>();


        static _ParticleManager instance;
        public static _ParticleManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new _ParticleManager();

                return instance;
            }
        }

    }
}
