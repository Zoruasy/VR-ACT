using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButtonSimple : MonoBehaviour
{
    public XRSimpleInteractable interactable;
    public AudioSource audioSource;

    [Header("Auto stop (loop)")]
    public float stopAfterSeconds = 262f; // 4:22
    public float startStopTimerAtSeconds = 150f; // 2:30

    [Header("VFX (light/slash)")]
    public ParticleSystem slashVfx;
    public float startVfxAfterSeconds = 150f; // 2:30

    private Coroutine stopRoutine;
    private Coroutine vfxStartRoutine;
    private Coroutine autoStopStartRoutine;

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
            interactable.enabled = false; // locked until slash starts
        }

        if (slashVfx != null)
            slashVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // Slash/VFX starts at 2:30
        if (slashVfx != null)
            vfxStartRoutine = StartCoroutine(StartVfxAfterDelay());

        // Stop timer also starts automatically at 2:30
        autoStopStartRoutine = StartCoroutine(StartAutoStopTimerAfterDelay());
    }

    private IEnumerator StartVfxAfterDelay()
    {
        yield return new WaitForSeconds(startVfxAfterSeconds);

        if (slashVfx != null)
            slashVfx.Play(true);

        if (interactable != null)
            interactable.enabled = true;

        vfxStartRoutine = null;
    }

    private IEnumerator StartAutoStopTimerAfterDelay()
    {
        yield return new WaitForSeconds(startStopTimerAtSeconds);

        if (stopRoutine == null)
            stopRoutine = StartCoroutine(StopLoopAfterTime());

        autoStopStartRoutine = null;
    }

    public void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Stop previous button sound if another button was active
        if (activeButton != null && activeButton != this && activeButton.audioSource != null)
        {
            activeButton.audioSource.Stop();

            if (activeButton.stopRoutine != null)
                activeButton.StopCoroutine(activeButton.stopRoutine);

            activeButton.stopRoutine = null;
            activeButton.ResetColor();
        }

        // Play this button sound
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.Play();
        }

        // Click = turn slash OFF
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

    private IEnumerator StopLoopAfterTime()
    {
        yield return new WaitForSeconds(stopAfterSeconds);

        if (audioSource != null)
            audioSource.Stop();

        stopRoutine = null;

        if (activeButton == this)
            activeButton = null;

        ResetColor();
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