namespace Timeline.Data.Model
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

        public override int GetHashCode() { return Ticks.GetHashCode(); }
        public override bool Equals(object obj) { return Ticks.Equals(obj); }

    }
}
