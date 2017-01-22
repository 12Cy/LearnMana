using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies
{
    class MoveManager
    {
#region Field Region

        private static Dictionary<string, IMove> allMoves = new Dictionary<string, IMove>();
        private static Random random = new Random();
        public static int debugshitDuration = 0;
        #endregion
        #region Property Region

        public static Random Random
        {
            get { return random; }
        }

#endregion
#region Constructor Region
#endregion

        #region Method Region
        public static void FillMoves()
        {
            AddMove(new Heal());
            AddMove(new Hot());
        }


        public static IMove GetMove(string name)
        {
            if (allMoves.ContainsKey(name))
                return (IMove)allMoves[name].Clone();

            return null;
        }

        public static void AddMove(IMove move)
        {
            if (!allMoves.ContainsKey(move.Name))
                allMoves.Add(move.Name, move);
        }
#endregion
    }
}