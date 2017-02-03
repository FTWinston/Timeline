using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public struct GameTime
    {
        public long Ticks;

        public static GameTime operator +(GameTime t, GameTimeSpan ts) { return new GameTime() { Ticks = t.Ticks + ts.Ticks }; }
        public static GameTimeSpan operator -(GameTime t1, GameTime t2) { return new GameTimeSpan() { Ticks = t1.Ticks - t2.Ticks }; }
        public static GameTime operator -(GameTime t, GameTimeSpan ts) { return new GameTime() { Ticks = t.Ticks - ts.Ticks }; }
        
        public static bool operator ==(GameTime t1, GameTime t2) { return t1.Ticks == t2.Ticks; }
        public static bool operator !=(GameTime t1, GameTime t2) { return t1.Ticks != t2.Ticks; }
        public static bool operator <(GameTime t1, GameTime t2) { return t1.Ticks < t2.Ticks; }
        public static bool operator >(GameTime t1, GameTime t2) { return t1.Ticks > t2.Ticks; }
        public static bool operator <=(GameTime t1, GameTime t2) { return t1.Ticks <= t2.Ticks; }
        public static bool operator >=(GameTime t1, GameTime t2) { return t1.Ticks >= t2.Ticks; }
    }
}
