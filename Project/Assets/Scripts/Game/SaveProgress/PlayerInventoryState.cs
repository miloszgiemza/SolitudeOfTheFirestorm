using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInventoryState
{
    public int CompartmentsSize => compartmentsSize;
    public string[] CompartmentBigScrolls => compartmentBigScrolls;
    public string[] CompartmentMixtures => compartmentMixtures;
    public string[] CompartmentSmallScrolls => compartementSmallScrolls;

    [SerializeField] private int compartmentsSize;
    [SerializeField] private string[] compartmentBigScrolls;
    [SerializeField] private string[] compartmentMixtures;
    [SerializeField] private string[] compartementSmallScrolls;

    public PlayerInventoryState(int compartmentsSize, string[] compartmentBigScrolls, string[] compartmentMixtures, string[] compartementSmallScrolls)
    {
        this.compartmentsSize = compartmentsSize;

        this.compartmentBigScrolls = (string[]) compartmentBigScrolls.Clone();
        this.compartmentMixtures = (string[]) compartmentMixtures.Clone();
        this.compartementSmallScrolls = (string[])compartementSmallScrolls.Clone(); 
    }
}
