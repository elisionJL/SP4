using UnityEngine;

[System.Serializable]
public class PlayerSettings
{
    public string username;
    public int masterVolume;
    public int sfxVolume;
    public int bgmVolume;
    public static PlayerSettings CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerSettings>(jsonString);
    }
}
