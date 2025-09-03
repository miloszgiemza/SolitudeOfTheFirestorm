using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRandomElement<T> 
{
    public static T ReturnRandomElement(List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
