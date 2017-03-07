using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame
{
    public static class Events
    {

        public delegate void DebugUpdate();

        static DebugUpdate debugUpdate;

        public static void SetEvent(DebugUpdate updt)
        {
            debugUpdate = updt;
        }

        public static void DoDebug()
        {
            List<int> myFactory = new List<int>();
            for(int i = 0; i <= myFactory.Count; i++)
                Console.Error.WriteLine((myFactory[i]));
            debugUpdate();

        }

       


    }
}
