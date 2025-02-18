using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameDatabaseItem : ScriptableObject
{
    public string IDGameDatabase => iDGameDatabase;
    [SerializeField][DatabaseIDField] protected string iDGameDatabase;
}
