using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCamera : MonoBehaviour
{
    [SerializeField] Transform playerTf;
    Vector3 pos;
    public int pointX;
    public int pointY;
    // Start is called before the first frame update
    void Start()
    {
        pos.z = this.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pointX = (int)(playerTf.position.x / 25);
        pointY = (int)(playerTf.position.y / 14);

        pos.x = pointX * 25 + 13;
        pos.y = pointY * 14 + 5;

        this.transform.position = pos;
    }
}
