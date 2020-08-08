using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void OnTriggerEnter(Collider other) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     
}
