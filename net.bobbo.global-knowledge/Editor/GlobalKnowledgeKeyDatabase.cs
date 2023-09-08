using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using BobboNet;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    public static class GlobalKnowledgeKeyDatabase
    {
        [System.Serializable]
        public class Database
        {
            public List<string> keys;


            public Database()
            {
                keys = new List<string>();
            }




            public void AddKey(string key)
            {
                keys.Add(key);
                keys.Sort();
            }

            public void RemoveKey(string key)
            {
                keys.Remove(key);
            }

            public void RemoveAt(int index)
            {
                keys.RemoveAt(index);
            }



            public string[] GetKeys()
            {
                return keys.ToArray();
            }

            public bool ContainsKey(string key)
            {
                return keys.Contains(key);
            }
        }


        private static JsonData<Database> databaseJSON;
        private static Database database;


        static GlobalKnowledgeKeyDatabase()
        {
            databaseJSON = new JsonData<Database>("global_knowledge_key_database.json");
            LoadDatabase();
        }

        private static void LoadDatabase()
        {
            database = databaseJSON.LoadAndGetData();

        }

        private static void SaveDatabase()
        {
            databaseJSON.SaveData();
        }




        public static string[] GetKeys()
        {
            return database.GetKeys();
        }

        public static void AddKey(string keyToAdd)
        {
            if (string.IsNullOrWhiteSpace(keyToAdd))
            {
                return;
            }

            if (database.ContainsKey(keyToAdd))
            {
                return;
            }

            database.AddKey(keyToAdd);
            SaveDatabase();
        }

        public static void RemoveKey(string keyToRemove)
        {
            if (!database.ContainsKey(keyToRemove))
            {
                return;
            }

            database.RemoveKey(keyToRemove);
            SaveDatabase();
        }

        public static void RemoveAt(int indexToRemove)
        {
            database.RemoveAt(indexToRemove);
            SaveDatabase();
        }
    }
}