using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class BGMManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource wavePrep;
    public AudioSource waveBattle;
    public AudioSource FinalPrep;
    public AudioSource FinalBattle;
    public enum BGM
    {
        WAVEPREP = 0,
        WAVEBATTLE,
        FINALPREP,
        FINALBATTLE,
        END
    }
    public void ChangeBGM(BGM bgm)
    {
        wavePrep.Stop();
        waveBattle.Stop();
        FinalPrep.Stop();
        FinalBattle.Stop();
        switch (bgm)
        {
            case BGM.WAVEPREP:
                wavePrep.Play();
                break;
            case BGM.WAVEBATTLE:
                waveBattle.Play();
                break;
            case BGM.FINALPREP:
                FinalPrep.Play();
                break;
            case BGM.FINALBATTLE:
                FinalBattle.Play();
                break;
            default:
                break;
        }
    }
}
