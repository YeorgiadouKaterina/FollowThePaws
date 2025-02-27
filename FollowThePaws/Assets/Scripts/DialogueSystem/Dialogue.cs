using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(2,4)]
    public string[] sentences;

    public string nameNPC;
    [TextArea(2,4)]
    public string[] sentencesNPC;
}
