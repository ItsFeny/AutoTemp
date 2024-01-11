using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    // Claves para PlayerPrefs
    private string resolutionKey = "SelectedResolutionIndex";
    private string fullscreenKey = "FullscreenToggle";

    void Start()
    {
        // Llenar el Dropdown con las resoluciones disponibles
        FillResolutionDropdown();

        // Obtener la resolución seleccionada guardada en PlayerPrefs (o usar el índice predeterminado)
        int savedResolutionIndex = PlayerPrefs.GetInt(resolutionKey, 0);

        // Establecer la resolución seleccionada al inicio
        resolutionDropdown.value = savedResolutionIndex;

        // Configurar el estado del Toggle basado en PlayerPrefs
        fullscreenToggle.isOn = PlayerPrefs.GetInt(fullscreenKey, 1) == 1;

        // Suscribir el método SetResolution al evento onValueChanged del Dropdown
        resolutionDropdown.onValueChanged.AddListener(delegate {
            SetResolution();
        });

        // Suscribir el método SetFullscreen al evento onValueChanged del Toggle
        fullscreenToggle.onValueChanged.AddListener(delegate {
            SetFullscreen();
        });
    }

    void FillResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        // Obtener las resoluciones disponibles
        Resolution[] resolutions = Screen.resolutions;

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        foreach (Resolution resolution in resolutions)
        {
            string optionText = resolution.width + " x " + resolution.height;
            options.Add(new TMP_Dropdown.OptionData(optionText));
        }

        resolutionDropdown.AddOptions(options);
    }

    public void SetResolution()
    {
        // Obtener el índice de la resolución seleccionada
        int selectedIndex = resolutionDropdown.value;

        // Obtener todas las resoluciones disponibles
        Resolution[] resolutions = Screen.resolutions;

        // Asegurarse de que el índice esté en el rango correcto
        if (selectedIndex >= 0 && selectedIndex < resolutions.Length)
        {
            // Establecer la resolución seleccionada
            Screen.SetResolution(resolutions[selectedIndex].width, resolutions[selectedIndex].height, Screen.fullScreen);

            // Guardar el índice de la resolución seleccionada en PlayerPrefs
            PlayerPrefs.SetInt(resolutionKey, selectedIndex);
        }
    }

    public void SetFullscreen()
    {
        // Establecer el modo de pantalla completa según el estado del Toggle
        Screen.fullScreen = fullscreenToggle.isOn;

        // Guardar el estado del Toggle en PlayerPrefs (1 para activado, 0 para desactivado)
        PlayerPrefs.SetInt(fullscreenKey, fullscreenToggle.isOn ? 1 : 0);
    }
}
