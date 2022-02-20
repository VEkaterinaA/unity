using UnityEngine;

public class MusicControl : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    internal void StopPlaying() => source.Stop();
}
