using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

[RequireComponent(typeof(AudioSource))]
public class AudioPoolObject : PoolAbleObject
{
    [SerializeField] private AudioSource source;
    //private float stdPitch;
    public override void Init_Pop()
    {
        //DoNothing
    }

    public override void Init_Push()
    {
        source.Stop();
    }
    /// <summary>
    /// ����� ���
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void Play(AudioClip clip, float pitch = 1f, float volume = 1f, AudioMixerGroup mixerGroup = null)
    {
        source.Stop();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.outputAudioMixerGroup = mixerGroup;
        source.Play();

        if(pitch < 0)
        {
            source.time = clip.length * 0.99f;
        }
        else
        {
            source.time = 0;
        }

        StartCoroutine(WaitForPush((source.clip.length / Mathf.Abs(source.pitch)) * 1.05f));
    }
    public void Update()
    {
        //source.pitch = stdPitch * (1 + (Time.timeScale - 1) * 0.5f);
    }
    IEnumerator WaitForPush(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        PoolManager.Push(PoolType, gameObject);
    }
}
