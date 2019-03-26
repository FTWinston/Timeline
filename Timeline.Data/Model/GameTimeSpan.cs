namespace Timeline.Data.Model
{
    public struct GameTimeSpan
    {
        public GameTimeSpan(long ticks)
        {
            Ticks = ticks;
        }

        public readonly long Ticks;
        
        public static GameTimeSpan operator +(GameTimeSpan t1, GameTimeSpan t2) { return new GameTimeSpan(t1.Ticks + t2.Ticks); }
        public static GameTimeSpan operator -(GameTimeSpan t1, GameTimeSpan t2) { return new GameTimeSpan(t1.Ticks - t2.Ticks); }

        public static bool operator ==(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks == t2.Ticks; }
        public static bool operator !=(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks != t2.Ticks; }
        public static bool operator <(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks < t2.Ticks; }
        public static bool operator >(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks > t2.Ticks; }
        public static bool operator <=(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks <= t2.Ticks; }
        public static bool operator >=(GameTimeSpan t1, GameTimeSpan t2) { return t1.Ticks >= t2.Ticks; }

        public override int GetHashCode() { return Ticks.GetHashCode(); }
        public override bool Equals(object obj) { return Ticks.Equals(obj); }
    }
}
