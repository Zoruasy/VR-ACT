using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButtonSimple : MonoBehaviour
{
    public XRSimpleInteractable interactable;
    public AudioSource audioSource;

    [Header("Alarm fixed stop time")]
    public float alarmStopAtSeconds = 257f;

    [Header("VFX (light/slash)")]
    public ParticleSystem slashVfx;
    public float startVfxAfterSeconds = 151f;

    private Coroutine stopRoutine;
    private Coroutine vfxStartRoutine;

    private Renderer buttonRenderer;
    private Color originalColor;

    private static VRButtonSimple activeButton;

    void Awake()
    {
        buttonRenderer = GetComponent<Renderer>();

        if (buttonRenderer != null)
            originalColor = buttonRenderer.material.color;
    }

    void Start()
    {
        if (interactable == null)
            interactable = GetComponent<XRSimpleInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
            interactable.enabled = false; // eerst niet klikbaar
        }

        if (slashVfx != null)
            slashVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        if (slashVfx != null)
            vfxStartRoutine = StartCoroutine(StartVfxAfterDelay());
    }

    private IEnumerator StartVfxAfterDelay()
    {
        yield return new WaitForSeconds(startVfxAfterSeconds);

        if (slashVfx != null)
            slashVfx.Play(true);

        if (interactable != null)
            interactable.enabled = true; // nu pas klikbaar

        vfxStartRoutine = null;
    }

    public void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Na 1 keer drukken niet meer opnieuw klikbaar
        if (interactable != null)
            interactable.enabled = false;

        if (activeButton != null && activeButton != this && activeButton.audioSource != null)
        {
            activeButton.audioSource.Stop();

            if (activeButton.stopRoutine != null)
                activeButton.StopCoroutine(activeButton.stopRoutine);

            activeButton.stopRoutine = null;
            activeButton.ResetColor();
        }

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = true;

            float remainingTime = alarmStopAtSeconds - Time.timeSinceLevelLoad;

            if (remainingTime > 0f)
            {
                audioSource.Play();

                if (stopRoutine != null)
                    StopCoroutine(stopRoutine);

                stopRoutine = StartCoroutine(StopLoopAfterTime(remainingTime));
            }
            else
            {
                audioSource.Stop();
            }
        }

        if (slashVfx != null)
        {
            if (vfxStartRoutine != null)
            {
                StopCoroutine(vfxStartRoutine);
                vfxStartRoutine = null;
            }

            slashVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        SetButtonColor(Color.red);
        activeButton = this;
    }

    private IEnumerator StopLoopAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (audioSource != null)
            audioSource.Stop();

        stopRoutine = null;
    }

    private void SetButtonColor(Color color)
    {
        if (buttonRenderer != null)
            buttonRenderer.material.color = color;
    }

    private void ResetColor()
    {
        if (buttonRenderer != null)
            buttonRenderer.material.color = originalColor;
    }
}