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
            //打开/创建目录
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //将gamadata转换为json
            string dataToSave = JsonUtility.ToJson(gameData, true);

            //打开/创建文件
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                //将json写入文件
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
