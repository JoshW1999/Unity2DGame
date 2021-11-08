using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    // Save and Load Inventory Data 
    public Inventory inv = null;

    // Path the file is being saved to. 
    private string savePath;

    // Array which holds loaded items
    public ArrayList items; 

    private void Awake() {
        savePath = Application.persistentDataPath + "/inv.json";
    }
    [ContextMenu("Save")]
    public void SaveInventory() {
        Debug.Log(savePath);
        LoaderSaver.savePlayerInventory(savePath, inv);
    }

    [ContextMenu("Load")]
    public void LoadInventory() {
        items = LoaderSaver.loadPlayerInventory(Application.persistentDataPath + "/inv.json");
    }

    public void WipeSaveFile() {
        LoaderSaver.wipeFile(Application.persistentDataPath + "/inv.json");
    }
}
