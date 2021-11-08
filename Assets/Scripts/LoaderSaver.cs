using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoaderSaver : MonoBehaviour
{
    public static void savePlayerInventory(string savePath, Inventory inv) {

        // Blank the File
        wipeFile(savePath);

        using (StreamWriter w = File.AppendText(savePath)) { 
            for (int i = 0; i < inv.slots.Length; i++) {
                if(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().slots[i].GetComponentInChildren<Button>() != null) { 
                    w.WriteLine(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().slots[i].GetComponentInChildren<Button>().name);
                }
            }
        }
    }

    public static ArrayList loadPlayerInventory(string savePath) {

        StreamReader inputStream = new StreamReader(savePath);
        ArrayList lines = new ArrayList();

        while (!inputStream.EndOfStream) {
            string ln = inputStream.ReadLine();
            lines.Add(ln);
        }

        return lines; 
    }

    // Wipes the Save File
    public static void wipeFile(string savePath) {
        File.WriteAllText(savePath, string.Empty);
    }
}
