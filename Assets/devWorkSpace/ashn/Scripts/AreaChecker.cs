using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    PointCamera pointCamera;
    int pointX;
    int pointY;

    [SerializeField] int thisPointX;
    [SerializeField] int thisPointY;

    // Start is called before the first frame update
    void Start()
    {
        pointCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PointCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        pointX = pointCamera.pointX;
        pointY = pointCamera.pointY;

        if (pointX - 1 <= thisPointX)
        {
            if (thisPointX <= pointX + 1)
            {
                if (pointY - 1 <= thisPointY)
                {
                    if (thisPointY <= pointY + 1)
                    {
                        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        hide();
                    }
                }
                else
                {
                    hide();
                }
            }
            else
            {
                hide();
            }
        }
        else
        {
            hide();
        }
    }
    void hide()
    {
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
}
