using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using devWorkSpace.knd.Scripts;
using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using static devWorkSpace.SoundTeam.Scripts.AisacNameList;
using static devWorkSpace.SoundTeam.Scripts.BGMNameList;

public class Warp : MonoBehaviour
{
    [SerializeField] GameObject warpPanel;
    [SerializeField] GameObject goalPanel;
    [SerializeField] GameObject sauro;

    SE _se;
    BGM _bgm;
    // Start is called before the first frame update
    void Start()
    {
        var sfx = GameObject.FindGameObjectWithTag("SE");
        _se = sfx.GetComponent<SE>();

        _bgm = GameObject.FindWithTag("BGM").GetComponent<BGMManager>().BGM;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            warpPanel.SetActive(true);
            goalPanel.SetActive(true);
            sauro.GetComponent<PlayerMove>().goal();
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        sauro.GetComponent<PlayerMove>().interval();
    //    }
    //}

    public void WarpToRemine()
    {
        sauro.transform.position = this.transform.GetChild(0).position;
        goalPanel.SetActive(false);
        warpPanel.SetActive(false);
        sauro.GetComponent<PlayerMove>()._isGoal = false;
        _bgm.setBlockId(7);
        _se.play(SENameList.Decision);
    }
}
