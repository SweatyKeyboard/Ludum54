using System.Collections.Generic;
using UnityEngine;

namespace UsefulScripts
{
    public static class GetRandomElementExtension
    {
        public static T GetRandomElement<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
        
        public static T GetRandomElement<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}