using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class TestFixFPSChangeScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("testFixFPSScene1");
            }
            if (Input.GetMouseButtonDown(1))
            {
                SceneManager.LoadScene("testFixFPSScene2");
            }
        }
    }
}
