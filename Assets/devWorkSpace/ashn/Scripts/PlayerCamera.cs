using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 _playerPos;
    private Vector3 _myPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = player.transform.position;
        _myPos = this.transform.position;
        _myPos.x = _playerPos.x;
        this.transform.position = _myPos;
    }
}
