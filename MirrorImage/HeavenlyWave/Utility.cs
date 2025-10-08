using System;
using MirrorImage;
namespace 
HeavenlyWave
{
    static class Utility
    {
        public static T[] Array<T>(int count)
            where T : new()
        {
            var result = new T[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new T();
            }
            return result;
        }
    }
}
