using UnityEngine;

[System.Serializable]
public class Speaker
{
    public string name;
    public Sprite avatar;
    [TextArea(3, 10)]
    public string[] sentences;

    public Speaker() { }
    public Speaker(string name)
    {
        this.name = name;
    }
}
