using devWorkSpace.SoundTeam.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class SceneChanger : MonoBehaviour
    {
        GameObject sfx;
        SE se;
        // Start is called before the first frame update
        void Start()
        {
            sfx = GameObject.Find("SE");
            se = sfx.GetComponent<SE>();
        }

        public void toTitle()
        {
            se.play(SENameList.Decision);
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("TempTitle");
        }
        public void toTutorial()
        {
            se.play(SENameList.Decision);
            SceneManager.LoadScene("TempTutorial");
        }
        public void toNewTutorial()
        {
            se.play(SENameList.Decision);
            SceneManager.LoadScene("devTutorial");
        }
        public void quitGame()
        {
            Application.Quit();
        }
        public void tutorialAndForest()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("devTutorialAndStage1");
        }
        public void Labo()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("devStage2");
        }
    }
}
