
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AudioManager : MonoSingleton<AudioManager>, ILogger
{
    public string Prefix => "<AudioManager>";
    
    [InlineEditor]
    [SerializeField] private MusicData _musicData;
    
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource pitchSoundEffect;
    
    #region Play BGM
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "_MainScene")
        {
            PlayBGM(_musicData.titleBGM);
        }
        else if (scene.name == "_Result")
        {
            StartCoroutine(PlayResultBgm());
        }else
        {
            int randomIndex = Random.Range(0, _musicData.gameBGMs.Length);
            PlayBGM(_musicData.gameBGMs[randomIndex]);
        }
    }

    private IEnumerator PlayResultBgm()
    {
        yield return new WaitForSeconds(1f);
        
        int randomIndex = Random.Range(0, _musicData.gameBGMs.Length);
        PlayBGM(_musicData.gameBGMs[randomIndex]);
    }
    

    #endregion
    
    public void PlayRandomSE(SE[] ses)
    {
        int randomIndex = Random.Range(0, _musicData.gameBGMs.Length);
        PlaySE(ses[randomIndex]);
    }

    [Button("播放音效")]
    public void PlaySE(SE se, float pitch)
    {
        var audioClip = _musicData.seDict[se];

        pitchSoundEffect.pitch = pitch;
        pitchSoundEffect.clip = audioClip;
        pitchSoundEffect.PlayOneShot(audioClip);
        Logger.Log(this,$"播放 SE {se} 以 pitch {pitch}");
    }
    
    [Button("播放音效")]
    public void PlaySE(SE se)
    {
        var audioClip = _musicData.seDict[se];

        soundEffect.clip = audioClip;
        soundEffect.PlayOneShot(audioClip);
        Logger.Log(this,$"播放 SE {se}");
    }
    
    [Button("播放音樂")]
    public void PlayBGM(AudioClip clip)
    {
        backgroundMusic.clip = clip;
        backgroundMusic.Play();
        Logger.Log(this,$"播放 Bgm {clip.name}");
        
    }

    
}