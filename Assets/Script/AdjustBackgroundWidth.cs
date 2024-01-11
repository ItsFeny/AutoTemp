using UnityEngine;

public class AdjustBackgroundWidth : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // Obtén el ancho y alto de la pantalla actual en modo retrato
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        // Calcula el aspecto de la pantalla
        float screenAspect = screenWidth / screenHeight;

        // Calcula el ancho objetivo del objeto de fondo
        float targetWidth = screenAspect * rectTransform.rect.height;

        // Ajusta el ancho del objeto de fondo
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }
}
