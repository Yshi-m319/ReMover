using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject OperatePanel;
    [SerializeField] GameObject OperatePanel2;
    // Start is called before the first frame update
    void Start()
    {
       // BackToMenu();
    }
    public void SelectOptionPanel()
    {
        OptionPanel.SetActive(true);
        OperatePanel.SetActive(false);
        OperatePanel2.SetActive(false);
    }
    public void SelectOperatePanel()
    {
        OptionPanel.SetActive(false);
        OperatePanel.SetActive(true);
        OperatePanel2.SetActive(false);
    }
    public void SelectOperatePanel2()
    {
        OptionPanel.SetActive(false);
        OperatePanel.SetActive(false);
        OperatePanel2.SetActive(true);
    }
    public void BackToMenu()
    {
        OptionPanel.SetActive(false);
        OperatePanel.SetActive(false);
        OperatePanel2.SetActive(false);
    }
}
