using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PressAnyKeyScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
