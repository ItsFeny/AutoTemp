using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;

public class FileDelete : MonoBehaviour
{
    public GameObject Alert1, Alert2;
    public void Delete()
    {
        // Obtiene la ruta de la carpeta temporal del usuario
        string userTempFolder = Path.GetTempPath();

        // Obtiene la ruta de la carpeta temporal de Windows
        string windowsTempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp");

        // Llama a la función para eliminar archivos en ambas carpetas temporales
        DeleteFilesInFolders(new string[] { userTempFolder, windowsTempFolder });
    }

    void DeleteFilesInFolders(string[] paths)
    {
        foreach (string path in paths)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    // Utiliza PowerShell para eliminar todo, incluidos archivos y carpetas en uso
                    RunPowerShellCommand($"Remove-Item -Path \"{path}\" -Recurse -Force -ErrorAction SilentlyContinue");

                    UnityEngine.Debug.Log($"Todo en la carpeta {path} ha sido eliminado.");
                    Alert1.SetActive(true);
                }
                catch (System.Exception e)
                {
                    UnityEngine.Debug.LogError($"Error al eliminar archivos en {path}: {e.Message}");
                    Alert1.SetActive(true);
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning($"La carpeta {path} no existe.");
            }
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
