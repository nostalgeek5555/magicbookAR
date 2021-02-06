using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<ChapterSO> allChapterList;
    public List<SubchapterSO> allSubchapterList;
    public Dictionary<string, ChapterSO> allChapterData;
    public Dictionary<string, SubchapterSO> allSubchapterData;
    public List<string> allcurrentChapter;
    public List<string> allcurrentSubchapter;

    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Debug.Log("instance this");
            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Debug.Log("destroy duplicate instance");
            Destroy(gameObject);
            return;
        }
          
    }

    private void Update()
    {
#if UNITY_EDITOR
        allcurrentChapter = allChapterData.Keys.ToList();
        allcurrentSubchapter = allSubchapterData.Keys.ToList();

#endif
    }


    public void GetResourcesData()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        allChapterList = new List<ChapterSO>(Resources.LoadAll<ChapterSO>("Scriptable Object/Chapter"));
        allChapterData = new Dictionary<string, ChapterSO>();
        allSubchapterData = new Dictionary<string, SubchapterSO>();
        for (int i = 0; i < allChapterList.Count; i++)
        {
            Debug.Log(allChapterList[i].chapterName);
            allChapterData.Add(allChapterList[i].chapterName, allChapterList[i]);
            
            if (allChapterList[i].subchapterList.Count > 0)
            {
                allSubchapterList = new List<SubchapterSO>(allChapterList[i].subchapterList);
                for (int j = 0; j < allChapterList[i].subchapterList.Count; j++)
                {
                    string subchapterKey = allChapterList[i].chapterName + "|" + allChapterList[i].subchapterList[j].subchapterName;
                    allSubchapterData.Add(subchapterKey, allSubchapterList[j]);
                }
            }
            //Debug.Log("chapter name " + allChapterData[allChapterList[i].chapterName]);
        }
    }
}
