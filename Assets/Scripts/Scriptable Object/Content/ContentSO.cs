using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Content", menuName = "Scriptable Object/Content")]
public class ContentSO : ScriptableObject
{
    public int contentID;
    public string contentTitle;
    public string[] videoURL;

    [ReorderableList] public List<Sprite> contentImages;

    [TextArea]
    public string[] contentText;
}
