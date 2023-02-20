using UnityEngine;

[System.Serializable]
public class TowerStats
{
    public int Tower1;
    public int Tower2;
    public int Tower3;
    public int Tower4;
    public int Tower5;

    public static TowerStats CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<TowerStats>(jsonString);
    }

    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.
}

//https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
