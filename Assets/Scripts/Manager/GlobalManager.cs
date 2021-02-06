using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    public PlayerData playerData;
    public List<string> allChapterUnlocked;
    public List<string> allSubchapterUnlocked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        GetComponentInParent<GameManager>().GetResourcesData();
        Load();
    }
    

    private void Update()
    {
#if UNITY_EDITOR
        allChapterUnlocked = playerData.chapterUnlocked.Keys.ToList();
        allSubchapterUnlocked = playerData.subchapterUnlocked.Keys.ToList();
#endif
    }

    [Button("Load Player Data")]
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "Playerdata.dat"))
        {

        }

        else
        {
            //player default data
            playerData.currentChapter = GameManager.Instance.allChapterList[0].chapterName;
            playerData.currentSubchapter = GameManager.Instance.allSubchapterList[0].subchapterName;
            playerData.chapterUnlocked.Add(playerData.currentChapter, true);
            string subchapterUnlocked = GameManager.Instance.allChapterList[0].chapterName + "|" + GameManager.Instance.allSubchapterList[0].subchapterName;
            playerData.subchapterUnlocked.Add(subchapterUnlocked, true);
        }
    }

    [Serializable]
    public class PlayerData
    {
        public string currentChapter;
        public string currentSubchapter;

        public Dictionary<string, bool> chapterUnlocked = new Dictionary<string, bool>();
        public Dictionary<string, bool> subchapterUnlocked = new Dictionary<string, bool>();
    }


    [Serializable]
    public class ChapterData
    {
        public int chapterID;
        public string chapterName;
        private bool chapterUnlocked = false;
        public Dictionary<string, SubchapterData> subchapterData = new Dictionary<string, SubchapterData>();

        public bool ChapterUnlocked
        { get => chapterUnlocked; set => chapterUnlocked = value;}

        public ChapterData(int _chapterID, string _chapterName, bool _chapterUnlocked)
        {
            chapterID = _chapterID;
            chapterName = _chapterName;
            chapterUnlocked = _chapterUnlocked;
        }
    }

    [Serializable]
    public class SubchapterData
    {
        public int subchapterID;
        public string subchapterName;
        private bool subchapterUnlocked = false;

        public bool SubchapterUnlocked { get => subchapterUnlocked; set => subchapterUnlocked = value; }

        public SubchapterData(int _subchapterID, string _subchapterName, bool _subchapterUnlocked)
        {
            subchapterID = _subchapterID;
            subchapterName = _subchapterName;
            subchapterUnlocked = _subchapterUnlocked;
        }
    }
}
