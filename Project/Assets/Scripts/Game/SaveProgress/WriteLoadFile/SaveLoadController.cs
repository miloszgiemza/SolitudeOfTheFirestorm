using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadController
{
    private static string saveFilePath = Application.persistentDataPath + "SaveGame.gd";

    public static void SaveGame(PlayerProgression playerProgression)
    {
        PlayerProgression newSave = new PlayerProgression(playerProgression.UnlockedSpells, playerProgression.PlayerInventoryState, playerProgression.UnlockedLevelsNumber);
        
        FileStream saveFile = File.Create(saveFilePath);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(saveFile, newSave);

        saveFile.Close();
    }

    public static PlayerProgression LoadGame()
    {
        FileStream saveFile = File.Open(saveFilePath, FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        PlayerProgression loadedSave = (PlayerProgression) binaryFormatter.Deserialize(saveFile);
        saveFile.Close();
        return loadedSave;
    }

    public static bool CheckIfSaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
}
