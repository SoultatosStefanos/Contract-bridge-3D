using System;
using ContractBridge.Core;

namespace Extensions
{
    public static class LevelExtensions
    {
        public static string ToNumeralString(this Level level)
        {
            return level switch
            {
                Level.One => "1",
                Level.Two => "2",
                Level.Three => "3",
                Level.Four => "4",
                Level.Five => "5",
                Level.Six => "6",
                Level.Seven => "7",
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };
        }
    }
}