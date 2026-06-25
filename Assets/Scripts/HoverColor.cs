using UnityEngine;

public class HoverColor : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.yellow;

    private Material mat;

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        mat = targetRenderer.material;
        mat.color = normalColor;
    }

    public void HoverEnter()
    {
        mat.color = hoverColor;
    }

    public void HoverExit()
    {
        mat.color = normalColor;
    }
}
