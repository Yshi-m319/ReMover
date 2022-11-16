using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using devWorkSpace.SoundTeam.Scripts;

public class Lift : MonoBehaviour
{
    float t;
    float d;
    float p;
    public float movePhase;
    private bool _UPorDOWN;
    private Vector3 _startPos;
    public float moveDistance;
    public static int stopMove;
    public GameObject rendoLift;
    Lift rendoCode;
    GameObject Player;
    SE se;
    // Start is called before the first frame update
    void Start()
    {       
        t = 0;
        d = 0;
        p = 0;
        _startPos = this.transform.position;
        stopMove = 0;
        Player = GameObject.Find("Player");
        if (moveDistance >= 0)
        {
            _UPorDOWN = true;
        }
        else
        {
            _UPorDOWN = false;
        }

        if (rendoLift != null)
        {
            rendoCode = rendoLift.GetComponent<Lift>();
        }

        var sfx = GameObject.Find("SE");
        se = sfx.GetComponent<SE>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movePhase == 1)
        {
            t += Time.deltaTime;

            if (t >= 0.3f)
            {
                t = 0.3f;
            }

            if (t >= 0.3f)
            {
                Player.gameObject.transform.parent = this.gameObject.transform;
                movePhase = 2;
                t = 0;
            }
            else
            {
                Player.transform.position = new Vector3(p + (d * t / 0.3f), Player.transform.position.y, Player.transform.position.z);
            }

        }
        else if (movePhase == 2)
        {
            Player.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            movePhase = 3;

            se.play(SENameList.LiftMoveFirst);
        }
        else if (movePhase == 3)
        {
            if(t == 0)
            {
                se.play(SENameList.LiftMove);
            }

            t += Time.deltaTime;
            if ((moveDistance < 0 && _UPorDOWN == true) || (moveDistance >= 0 && _UPorDOWN == false))
            {
                this.transform.position = new Vector3(this.transform.position.x, _startPos.y + moveDistance - ((-Mathf.Cos(t * Mathf.PI / 3) + 1) * moveDistance / 2), this.transform.position.z);
                if (t >= 3)
                {
                    this.transform.position = new Vector3(this.transform.position.x, _startPos.y, this.transform.position.z);
                    if (_UPorDOWN == true)
                    {
                        _UPorDOWN = false;
                    }
                    else
                    {
                        _UPorDOWN = true;
                    }

                    if (this.gameObject.transform.childCount >= 2)
                    {
                        this.gameObject.transform.GetChild(1).gameObject.transform.parent = null;
                    }
                    stopMove = 0;
                    movePhase = 0;
                    t = 0;
                    se.play(SENameList.LiftMoveFinal);
                }
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, _startPos.y + ((-Mathf.Cos(t * Mathf.PI / 3) + 1) * moveDistance / 2), this.transform.position.z);
                if (t >= 3)
                {
                    this.transform.position = new Vector3(this.transform.position.x, _startPos.y + moveDistance, this.transform.position.z);
                    if (_UPorDOWN == true)
                    {
                        _UPorDOWN = false;
                    }
                    else
                    {
                        _UPorDOWN = true;
                    }

                    if (this.gameObject.transform.childCount >= 2)
                    {
                        this.gameObject.transform.GetChild(1).gameObject.transform.parent = null;
                    }
                    stopMove = 0;
                    movePhase = 0;
                    t = 0;
                    se.play(SENameList.LiftMoveFinal);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && movePhase == 0 && ((_UPorDOWN == true && Input.GetKey(KeyCode.W)) || (_UPorDOWN == false && Input.GetKey(KeyCode.S))))
        {
            stopMove = 1;
            movePhase = 1;
            t = 0;
            d = this.transform.position.x - other.transform.position.x;
            p = other.transform.position.x;
            if (rendoLift != null)
            {
                StartCoroutine("Rendo");               
            }
        }
    }

    IEnumerator Rendo()
    {
        if (movePhase == 1)
        {
            yield return new WaitForSeconds(0.3f);
            rendoCode.movePhase = 3;
            Debug.Log("連動");
        }
    }
}
