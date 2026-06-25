using System.Collections;
using UnityEngine;

public class BeepForSeconds : MonoBehaviour
{
    public AudioSource buttonAudio;
    public float playForSeconds = 5f;

    private Coroutine routine;

    private void Awake()
    {
        if (!buttonAudio) buttonAudio = GetComponent<AudioSource>();
    }

    // Koppel deze aan je button press / Activated event
    public void PlayBeepForSeconds()
    {
        // Start (opnieuw) de loop
        buttonAudio.Stop();
        buttonAudio.Play();

        // Reset timer als je opnieuw drukt
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(StopAfter());
    }

    private IEnumerator StopAfter()
    {
        yield return new WaitForSeconds(playForSeconds);
        buttonAudio.Stop();
        routine = null;
    }
}
