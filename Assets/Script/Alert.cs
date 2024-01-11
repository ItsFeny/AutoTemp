using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public GameObject objetoAActivar;
    public float tiempoDeActivacion = 4f;

    void Start()
    {
        ActivateObject();
    }

    public void ActivateObject()
    {
        objetoAActivar.SetActive(true);
        Invoke("DeactivateObject", tiempoDeActivacion);
    }

    private void DeactivateObject()
    {
        objetoAActivar.SetActive(false);
    }
}
