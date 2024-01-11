using UnityEngine;
using System.Diagnostics;
using System.IO;

public class RegistryOptimizer : MonoBehaviour
{
    public GameObject Alert3, Alert4;
    public void OptimizeRegistry()
    {
        // Llama a la función para optimizar el registro
        OptimizeRegistryUsingPowerShell();
    }

    void OptimizeRegistryUsingPowerShell()
    {
        try
        {
            // Comando para ejecutar un optimizador de registro (puedes cambiar el comando según tus necesidades)
            string command = "regedit /s C:\\Ruta\\Al\\Archivo\\De\\Tu\\OptimizadorDeRegistro.reg";

            RunPowerShellCommand(command);

            UnityEngine.Debug.Log("Registro optimizado correctamente.");
            Alert3.SetActive(true);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError($"Error al optimizar el registro: {e.Message}");
            Alert4.SetActive(true);
        }
    }

    void RunPowerShellCommand(string command)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "powershell",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            UseShellExecute = false,
            Verb = "runas" // Pide permisos de administrador
        };

        Process process = new Process { StartInfo = psi };
        process.Start();

        StreamWriter sw = process.StandardInput;
        StreamReader sr = process.StandardOutput;
        StreamReader se = process.StandardError;

        sw.WriteLine(command);
        sw.Close();

        string output = sr.ReadToEnd();
        string error = se.ReadToEnd();

        process.WaitForExit();

        UnityEngine.Debug.Log($"PowerShell Output: {output}");
        if (!string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.LogError($"Error en PowerShell: {error}");
        }
    }
}
