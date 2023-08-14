using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace BobboNet
{

    public class JsonData<Generic> where Generic : new()
    {
        public string pathToData = "";
        private Generic genericData;


        public JsonData(string _pathToData)
        {
            pathToData = _pathToData;
        }


        public void LoadData()
        {
            if (!File.Exists(pathToData))
            {
                CreateNewData();
                return;
            }

            TextReader tr = File.OpenText(pathToData);
            string dataJSON = tr.ReadToEnd();
            tr.Close();

            genericData = JsonUtility.FromJson<Generic>(dataJSON);

            // if the settings file was corrupted...
            if (genericData == null)
            {
                CreateNewData();
            }
        }

        public void SaveData()
        {
            string dataJSON = JsonUtility.ToJson(genericData, true);

            TextWriter tw = File.CreateText(pathToData);
            tw.Write(dataJSON);
            tw.Close();
        }

        public void CreateNewData()
        {
            genericData = new Generic();
            SaveData();
        }

        public Generic GetData()
        {
            return genericData;
        }

        public Generic LoadAndGetData()
        {
            LoadData();
            return genericData;
        }
    }
}