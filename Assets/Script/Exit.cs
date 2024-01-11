using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Close()
    {
        Debug.Log("Programa Cerrado Correctamente");
        Application.Quit();
    }
}
