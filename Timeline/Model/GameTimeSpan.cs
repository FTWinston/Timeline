using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public struct GameTimeSpan
    {
        public long Ticks;
        
        public static GameTimeSpan operator +(GameTimeSpan t1, GameTimeSpan t2) { return new GameTimeSpan() { Ticks = t1.Ticks + t2.Ticks }; }
        public static GameTimeSpan operator -(GameTimeSpan t1, GameTimeSpan t2) { return new GameTimeSpan() { Ticks = t1.Ticks - t2.Ticks }; }

        public static bool operator ==(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks == t2.Ticks; }
        public static bool operator !=(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks != t2.Ticks; }
        public static bool operator <(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks < t2.Ticks; }
        public static bool operator >(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks > t2.Ticks; }
        public static bool operator <=(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks <= t2.Ticks; }
        public static bool operator >=(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks >= t2.Ticks; }
    }
}
