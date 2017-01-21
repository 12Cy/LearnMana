using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    enum ITyp
    {
        dummy,
    }

    class DummyInformation : Attributes
    {
        #region properties
        public DummyInformation()
        {

        }

        #endregion
    }
    class SummonedsInformation
    {
        public DummyInformation dummyInformation = new DummyInformation();


        static SummonedsInformation instance;
        public static SummonedsInformation Instance
        {
            get
            {
                if (instance == null)
                    instance = new SummonedsInformation();

                return instance;
            }
        }
    }
}
