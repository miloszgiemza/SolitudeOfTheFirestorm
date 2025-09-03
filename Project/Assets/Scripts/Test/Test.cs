using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("To jest header")]
    [SerializeField] private Serializable2DArray<int> serialziable2DArray = new Serializable2DArray<int>(4, 4);
}
