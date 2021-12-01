using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DIALOGUE_UTILITY : MonoBehaviour
{
    public bool WriteOnPlay;
    public List<Phrase> list;
    public string filename;


    private void Start()
    {
        if (WriteOnPlay)
        {
            WriteJson();
        }
    }


    public void WriteJson()
    {
        DialogueList ListObj;

        ListObj = new DialogueList(list);

        string d0 = JsonUtility.ToJson(ListObj, true);
        Debug.Log(d0);
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/" + filename, d0);
    }

    public static DialogueList ReadJson(string filePath)
    {
        string data = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/" + filePath);

        var DialogueObj = JsonUtility.FromJson<DialogueList>(data);
        return DialogueObj;
    }
}




[System.Serializable]
public enum Emotion
{
    normal,
    worried,
    yelling,
    thinking,
    question,
    init
}

[System.Serializable]
public class Phrase
{
    public int index;
    public string name;
    public Emotion emotion;
    public string message;
    public int next;

}

[System.Serializable]
public class DialogueList
{
    public List<Phrase> list;

    public DialogueList(List<Phrase> p)
    {
        list = new List<Phrase>(p);
    }
}
