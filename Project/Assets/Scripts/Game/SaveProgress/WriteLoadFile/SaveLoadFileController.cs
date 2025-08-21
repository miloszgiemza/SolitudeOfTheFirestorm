using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadFileController
{
    private static string saveFilePath = Application.persistentDataPath + "/SaveGame.gd";

    public static void SaveGame(PlayerProgression playerProgression)
    {
        //PlayerProgression newSave = new PlayerProgression(playerProgression.UnlockedLevelsNumber, playerProgression.UnlockedSpells, playerProgression.EquipedSpells, playerProgression.PlayerInventoryState);
        
        FileStream saveFile = File.Create(saveFilePath);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(saveFile, playerProgression);

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
