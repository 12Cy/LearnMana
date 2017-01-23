using LearnMonoGame.Particle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Manager
{
    class ParticleManager
    {
        public List<SimpleParticle> particles = new List<SimpleParticle>();


        static ParticleManager instance;
        public static ParticleManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ParticleManager();

                return instance;
            }
        }

    }
}
