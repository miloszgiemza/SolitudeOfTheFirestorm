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

    private int compartmentsSize;
    private string[] compartmentBigScrolls;
    private string[] compartmentMixtures;
    private string[] compartementSmallScrolls;

    public PlayerInventoryState(int compartmentsSize, string[] compartmentBigScrolls, string[] compartmentMixtures, string[] compartementSmallScrolls)
    {
        this.compartmentsSize = compartmentsSize;

        this.compartmentBigScrolls = (string[]) compartmentBigScrolls.Clone();
        this.compartmentMixtures = (string[]) compartmentMixtures.Clone();
        this.compartementSmallScrolls = (string[])compartementSmallScrolls.Clone(); 
    }

    public PlayerInventoryState(int compartmentsSize, BaseItem[] currentBigScrolls, BaseItem[] currentMixtures, BaseItem[] currentSmallScrolls)
    {
        this.compartmentsSize = compartmentsSize;
        this.compartmentBigScrolls = LoadInventoryCompartmentFromInventoryControllerToSaveFormat(compartmentsSize, currentBigScrolls);
        this.compartmentMixtures = LoadInventoryCompartmentFromInventoryControllerToSaveFormat(compartmentsSize, currentMixtures);
        this.compartementSmallScrolls = LoadInventoryCompartmentFromInventoryControllerToSaveFormat(compartmentsSize, currentSmallScrolls);
    }

    /*
    public PlayerInventoryState UpdatePlayerInventoryState(int compartmentSize, BaseItem[] currentBigScrolls, BaseItem[] currentMixtures, BaseItem[] currentSmallScrolls)
    {
        return new PlayerInventoryState(compartmentSize, LoadInventoryCompartmentFromInventoryControllerToSaveFormat(compartmentSize, currentBigScrolls), 
            LoadInventoryCompartmentFromInventoryControllerToSaveFormat(compartmentSize, currentMixtures), LoadInventoryCompartmentFromInventoryControllerToSaveFormat(compartmentSize, currentSmallScrolls));
    }
    */

    private string[] LoadInventoryCompartmentFromInventoryControllerToSaveFormat(int compartmentSize, BaseItem[] currentInventoryCompartment)
    {
        string[] compartmentState = new string[compartmentSize];

        for(int i=0; i < compartmentSize; i++)
        {
            if(!ReferenceEquals(currentInventoryCompartment[i], null))compartmentState[i] = currentInventoryCompartment[i].IDGameDatabase;
        }

        return compartmentState;
    }
}
