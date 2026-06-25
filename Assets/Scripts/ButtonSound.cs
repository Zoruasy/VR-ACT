using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sound;
    public Renderer buttonRenderer;

    public Color normalColor = Color.white;
    public Color pressedColor = Color.red;

    XRBaseInteractable interactable;

    void Awake()
    {
        // Maak een AudioSource aan als er geen is toegewezen
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f; // 3D geluid (zet naar 0 voor 2D)
        }
    }

    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogWarning($"ButtonSound: geen XRBaseInteractable gevonden op '{gameObject.name}'. Voeg een interactable component toe.");
            return;
        }

        interactable.selectEntered.AddListener(OnPressed);
    }

    void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnPressed);
    }

    void OnPressed(SelectEnterEventArgs args)
    {
        if (audioSource != null && sound != null)
            audioSource.PlayOneShot(sound);
        else if (sound != null)
            AudioSource.PlayClipAtPoint(sound, transform.position, 1f);

        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = pressedColor;
            CancelInvoke();
            Invoke(nameof(ResetColor), 0.15f);
        }
    }

    void ResetColor()   
    {
        if (buttonRenderer != null)
            buttonRenderer.material.color = normalColor;
    }
}
