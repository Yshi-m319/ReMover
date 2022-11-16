using devWorkSpace.SoundTeam.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] GameObject optionPanel;
        [SerializeField] GameObject operatePanel;
        [SerializeField] GameObject operatePanel2;
        [SerializeField] GameObject soundPanel;
        SE _se;
        // Start is called before the first frame update
        void Start()
        {
            var sfx = GameObject.Find("SE");
            _se = sfx.GetComponent<SE>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void closePanels()
        {
            Time.timeScale = 1.0f;
            optionPanel.SetActive(false);
            operatePanel.SetActive(false);
            operatePanel2.SetActive(false);
            soundPanel.SetActive(false);
            _se.play(SENameList.Cancel);
        }
        public void openOption()
        {
            optionPanel.SetActive(true);
            _se.play(SENameList.Decision);
        }
        public void closeOption()
        {
            optionPanel.SetActive(false);
            _se.play(SENameList.Cancel);
        }
        public void openOperate()
        {
            operatePanel.SetActive(true);
            _se.play(SENameList.Decision);
        }
        public void closeOperate()
        {
            operatePanel.SetActive(false);
            _se.play(SENameList.Cancel);
        }

        public void openOperate2()
        {
            operatePanel2.SetActive(true);
            _se.play(SENameList.Decision);
        }
        public void closeOperate2()
        {
            operatePanel2.SetActive(false);
            _se.play(SENameList.Cancel);
        }
        public void openSound()
        {
            soundPanel.SetActive(true);
            _se.play(SENameList.Decision);
        }
        public void closeSound()
        {
            soundPanel.SetActive(false);
            _se.play(SENameList.Cancel);
        }
    }
}
