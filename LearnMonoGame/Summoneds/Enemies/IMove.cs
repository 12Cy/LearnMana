using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies
{
    public enum ETarget
    {
        Self, Enemy
    }

    public enum EMoveType 
    {
        Attack, Heal, Buff, Debuff, Status
    }
    public enum EStatus //welchen Status habe ich nach meinem Move
    {
        Normal, Sleep, Poison, Paralysis
    }
    public enum EMoveElement
    {
        None, Dark, Earth, Fire, Light, Water, Wind
    }


    public interface IMove
    {
        ETarget Target { get; }
        EMoveType MoveType { get; }
        EMoveElement MoveElement { get; }
        EStatus Status { get; }
        string Name { get; }
        int Duration { get; set; }
        int Attack { get; }
        int Defense { get; }
        int Speed { get; }
        int Health { get; }
        object Clone();
    }
}
