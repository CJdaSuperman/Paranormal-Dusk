using UnityEngine;

public class TitleScreenHandler : MonoBehaviour
{
   void Update()
    {
        if (Input.GetKey(KeyCode.Q)) 
            Application.Quit(); 
    }
}