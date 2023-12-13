using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CursorHover : MonoBehaviour
{
    [SerializeField] Color _selectedColor = Color.red;

    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = _selectedColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }
}
