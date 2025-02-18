using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayRandomElement<T>
{
    public static T ReturnRandomElement(T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
}
