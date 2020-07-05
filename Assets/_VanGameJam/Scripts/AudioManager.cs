using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;

    [SerializeField] private ListOfAudioClips _clips;

    public static AudioManager Instance => _instance;

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private AudioSource _audioManagerAudioSource = null;
    private AudioSource AudioManagerAudioSource
    {
        get
        {
            if (_audioManagerAudioSource == null)
            {
                _audioManagerAudioSource = GetComponent<AudioSource>();
            }

            return _audioManagerAudioSource;
        }
    }

    public void PlayGameLose()
    {
        PlayClip(_clips.OnLose);
    }

    public void PlayButtonClick()
    {
        PlayClip(_clips.ButtonClick);
    }

    public void PlayFillContainerWithFood()
    {
        PlayClip(_clips.FoodAppear);
    }

    public void PlayGrabbedFood()
    {
        PlayClip(_clips.FoodOnGrab);
    }

    public void PlayReleaseFood()
    {
        PlayClip(_clips.FoodOnRelease);
    }

    public void PlayContainerClicked()
    {
        PlayClip(_clips.FoodClick);
    }

    private void PlayClip(AudioClip clip)
    {
        AudioManagerAudioSource.clip = clip;
        AudioManagerAudioSource.Play();
    }
}
