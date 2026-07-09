using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButtonSimple : MonoBehaviour
{
    public XRSimpleInteractable interactable;
    public AudioSource audioSource;

    [Header("Auto stop (loop)")]
    public float stopAfterSeconds = 262f; // 4:22 = 262 sec

    [Header("VFX (light/slash)")]
    public ParticleSystem slashVfx;
    public float startVfxAfterSeconds = 150f; // 2:31 = 151 sec

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
            interactable.enabled = false; // locked until slash starts
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
            interactable.enabled = true;

        vfxStartRoutine = null;
    }

    public void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Stop previous active button if it's another button
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

            // Start timer ONLY the first time
            if (stopRoutine == null)
            {
                stopRoutine = StartCoroutine(StopLoopAfterTime());
            }
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