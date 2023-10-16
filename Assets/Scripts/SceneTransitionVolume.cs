using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameObject used to transition to the next scene
/// </summary>
public class SceneTransitionVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     
}
