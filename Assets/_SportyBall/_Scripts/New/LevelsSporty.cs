using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsSporty : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("Home");
        AudioSporty.instance.PlaySound(0);
    }
}