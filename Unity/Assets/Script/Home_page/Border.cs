using UnityEngine;
using UnityEngine.SceneManagement;

public class Border : MonoBehaviour
{
    public GameObject Camera;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            Debug.Log("Reset");
            SceneManager.LoadScene("Home_page");
        }
    }

}
