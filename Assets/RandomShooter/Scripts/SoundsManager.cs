using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
