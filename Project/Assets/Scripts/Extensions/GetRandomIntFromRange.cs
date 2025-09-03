using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetRandomIntFromRange
{
    public static int Get(int minInclusive, int maxInclusive)
    {
        return UnityEngine.Random.Range(minInclusive, maxInclusive + 1);
    }
}
