using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void StopAllAudio()
    {
        // Find all AudioSource components without sorting for best performance
        AudioSource[] sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource source in sources)
        {
            source.Stop();
        }
    }
}
