using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools
{
    public static class BoolClass
    {
        //Gibt an, ob der Mauszeiger im Window des Spiels ist.
        public static bool MouseInsideWindow { get; set; }

        //Gibt an, ob der Spieler sich gerade in einem Level befindet.
        public static bool InGameLevel { get; set; }


        public static void Init()
        {
            MouseInsideWindow = false;
            InGameLevel = false;
        }

        public static string StrBool()
        {
            return  nameof(MouseInsideWindow) + "\t\t" + MouseInsideWindow.ToString() + "\n" +
                    nameof(InGameLevel) + "\t\t\t" + InGameLevel.ToString();
        }
    }
}
