using System.Collections;
using System.Collections.Generic;
using devWorkSpace.SoundTeam.Scripts;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject stagePanel;

    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject operatePanel;
    [SerializeField] GameObject operatePanel2;
    [SerializeField] GameObject soundPanel;
    [SerializeField] GameObject staffPanel;
    GameObject sfx;
    SE se;
    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.Find("SE");
        se = sfx.GetComponent<SE>();
    }



    public void openMain()
    {
        mainPanel.SetActive(true);
        stagePanel.SetActive(false);
        se.play(SENameList.Cancel);
    }
    public void closeMain()
    {
        mainPanel.SetActive(false);
        stagePanel.SetActive(true);
        se.play(SENameList.Decision);
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
        operatePanel.SetActive(true);
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

    public void openStaff()
    {
        staffPanel.SetActive(true);
        se.play(SENameList.Decision);
    }
    public void closeStaff()
    {
        staffPanel.SetActive(false);
        se.play(SENameList.Cancel);
    }
}
