using UnityEngine;

[CreateAssetMenu(fileName = "List Of Food", menuName = "MPHT/ListOfAudioClips", order = 1)]
public class ListOfAudioClips : ScriptableObject
{
    public AudioClip ButtonClick;
    public AudioClip FoodClick;
    public AudioClip FoodAppear;
    public AudioClip FoodOnGrab;
    public AudioClip FoodOnRelease;
    public AudioClip OnLose;
}
