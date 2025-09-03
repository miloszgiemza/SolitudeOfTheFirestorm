using System.Collections;
using System.Collections.Generic;
using System;

public static class GenerateUniqueID
{
    public static string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
