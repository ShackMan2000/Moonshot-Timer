using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Music : MonoBehaviour
{
    [SerializeField] Slider volumeSlider, maxVolumeSlider;

    AudioSource audioSource;

    float maxVolume;

    bool isIncreasingVolume;


    [SerializeField] AudioClip focusMusic;
    [SerializeField] AudioClip rocketMusic;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;

        volumeSlider.onValueChanged.AddListener(delegate { VolumeToSlider(volumeSlider.value); });
        volumeSlider.value = 0;


        maxVolumeSlider.onValueChanged.AddListener(delegate { ChangeMaxVolume(maxVolumeSlider.value); });
        maxVolumeSlider.value = 0.2f;


        SmoothlyIncreaseVolume();
    }


    void OnEnable()
    {
        Dude.OnMineGotStarted += MuteMusic;
        Dude.OnMineGotStopped += SmoothlyIncreaseVolume;


    }

    void SwitchToFocusMusic()
    {
        audioSource.clip = focusMusic;
        audioSource.Play();
    }

    void SwitchToRocketMusic()
    {
        audioSource.clip = rocketMusic;
        audioSource.Play();
        SmoothlyIncreaseVolume();
    }

    void VolumeToSlider(float value)
    {
        audioSource.volume = value;
    }



    [ContextMenu("IncreaseVolumeTillMax")]
    public void SmoothlyIncreaseVolume()
    {
        if (!isIncreasingVolume)
            StartCoroutine(SmoothlyIncreaseVolumeRoutine());
    }


    IEnumerator SmoothlyIncreaseVolumeRoutine()
    {
        isIncreasingVolume = true;
        float lastSetVolume = 0f;

        while (volumeSlider.value < maxVolume)
        {
            volumeSlider.value += 0.05f;
            lastSetVolume = volumeSlider.value;
            yield return new WaitForSeconds(0.3f);
            if (volumeSlider.value != lastSetVolume)
                break;
        }

        isIncreasingVolume=false;
    }


    public void ChangeVolume(float newVolume)
    {
        volumeSlider.value = newVolume;
    }


    public void ChangeMaxVolume(float newMaxVolume)
    {
        //  maxVolumeSlider.value = newMaxVolume;
        maxVolume = newMaxVolume;

    }
    public void MuteMusic() => ChangeVolume(0f);
  










}
