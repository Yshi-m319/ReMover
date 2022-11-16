using System.Collections;
using System.Collections.Generic;
using devWorkSpace.SoundTeam.Scripts;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject operatePanel;
    [SerializeField] GameObject operatePanel2;
    [SerializeField] GameObject soundPanel;
    GameObject sfx;
    SE se;
    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.Find("SE");
        se = sfx.GetComponent<SE>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeInHierarchy) //ポーズ画面を閉じるとき
            {
                closePanel();
            }
            else //ポーズ画面を開くとき
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                se.play(SENameList.Decision2);
            }
        }
    }
    public void closePanel()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        operatePanel.SetActive(false);
        operatePanel2.SetActive(false);
        soundPanel.SetActive(false);
        se.play(SENameList.Cancel2);
    }
    public void openOption()
    {
        optionPanel.SetActive(true);
        se.play(SENameList.Decision);
    }
    public void closeOption()
    {
        optionPanel.SetActive(false);
        se.play(SENameList.Cancel);
    }
    public void openOperate()
    {
        operatePanel.SetActive(true);
        se.play(SENameList.Decision);
    }
    public void closeOperate()
    {
        operatePanel.SetActive(false);
        se.play(SENameList.Cancel);
    }

    public void openOperate2()
    {
        operatePanel2.SetActive(true);
        se.play(SENameList.Decision);
    }
    public void closeOperate2()
    {
        operatePanel2.SetActive(false);
        se.play(SENameList.Cancel);
    }
    public void openSound()
    {
        soundPanel.SetActive(true);
        se.play(SENameList.Decision);
    }
    public void closeSound()
    {
        soundPanel.SetActive(false);
        se.play(SENameList.Cancel);
    }
}
