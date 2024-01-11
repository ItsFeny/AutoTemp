using UnityEngine;
using System.Diagnostics;

public class OpenCleanner : MonoBehaviour
{
    public void OpenCCleaner()
    {
        try
        {
            Process.Start("C:\\Program Files\\CCleaner\\CCleaner.exe");
            UnityEngine.Debug.Log("CCleaner se ha abierto correctamente.");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Error al abrir CCleaner: " + e.Message);
        }
    }
}
