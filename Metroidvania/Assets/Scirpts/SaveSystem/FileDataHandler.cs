using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class FileDataHandler 
{
    private string fullPath;

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        fullPath = Path.Combine(dataDirPath, dataFileName);
    }

    public void SaveData(GameData gameData)
    {
        try
        {
            //��/����Ŀ¼
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //��gamadataת��Ϊjson
            string dataToSave = JsonUtility.ToJson(gameData, true);

            //��/�����ļ�
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                //��jsonд���ļ�
                using (StreamWriter write = new StreamWriter(stream))
                {
                    write.Write(dataToSave);
                }
            }
        }

        catch (Exception e)
        {
            Debug.Log("ERROR: "+e);
        }
    }


    public GameData LoadData()
    {
        GameData loadData=null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }

            catch (Exception e)
            {
                Debug.Log("ERROR " + e);
            }
        }

        return loadData;
    }

    public void Delete()
    {
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
