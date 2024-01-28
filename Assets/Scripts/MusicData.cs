using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum SE
{
    Button,
    Ghost,
    Lose,
    ScaryLaugh,
    Slap,
    Spring,
    Win,
    Arrow,
    Click1,
    Click2,
    Click3,
    GetPoint,
}
    
public enum Bgm
{
    Take1,
    Take2,
    Take4
}
    
[CreateAssetMenu(fileName = "Music Data", menuName = "Music Data")]
public class MusicData : SerializedScriptableObject
{
        
    public Dictionary<SE, AudioClip> seDict;

    [Title("BGM")]
    public AudioClip titleBGM;
    public AudioClip[] gameBGMs;

}