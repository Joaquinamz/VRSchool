using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Carga directa (rápida)
    public void LoadByName(string cursoExtintor1)
    {
        SceneManager.LoadScene(cursoExtintor1);
    }

    // (Opcional) Carga asíncrona con pantalla de “cargando”
    public void LoadByNameAsync(string cursoExtintor1)
    {
        StartCoroutine(LoadRoutine(cursoExtintor1));
    }

    private System.Collections.IEnumerator LoadRoutine(string cursoExtintor1)
    {
        var op = SceneManager.LoadSceneAsync(cursoExtintor1);
        op.allowSceneActivation = true; // si quieres controlar el momento de activar, ponlo en false y luego true
        while (!op.isDone)
        {
            // Aquí puedes actualizar una barra de progreso si tienes una
            yield return null;
        }
    }
}
