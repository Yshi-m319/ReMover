using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateController : MonoBehaviour
{
    [SerializeField] GameObject movePanel;
    [SerializeField] GameObject jumpPanel;
    [SerializeField] GameObject mousePanel;
    [SerializeField] GameObject shootPanel;
    [SerializeField] GameObject escPanel;
    [SerializeField] Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pos.transform.position.x <= 70)
        {
            if (pos.transform.position.x <= 12)
            {
                movePanel.SetActive(true);
            }
            else
            {
                movePanel.SetActive(false);
            }

            if (pos.transform.position.x > 12 && pos.transform.position.x < 25)
            {
                jumpPanel.SetActive(true);
            }
            else
            {
                jumpPanel.SetActive(false);
            }

            if (pos.transform.position.x >= 25 && pos.transform.position.x < 35)
            {
                mousePanel.SetActive(true);
            }
            else
            {
                mousePanel.SetActive(false);
            }

            if (pos.transform.position.x >= 35 && pos.transform.position.x < 47)
            {
                shootPanel.SetActive(true);
            }
            else
            {
                shootPanel.SetActive(false);
            }

            if (pos.transform.position.x >= 47 && pos.transform.position.x < 60)
            {
                escPanel.SetActive(true);
            }
            else
            {
                escPanel.SetActive(false);
            }
        }
    }
}
