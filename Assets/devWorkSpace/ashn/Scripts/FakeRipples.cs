using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRipples : MonoBehaviour
{
    private Vector3 _size;
    private float _width;
    private float _time = 0f;
    private Material _mat;
    // Start is called before the first frame update
    void Start()
    {
        _size.z = 0.2f;
        _mat = GetComponent<MeshRenderer>().material;
        _mat.color = new Color(1, 1, 1, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > 1.5) { _time = 0; }
        _width = _time * 4.0f / 1.5f;
        _size.x = _size.y = _width;
        transform.localScale = _size;
        _mat.color = new Color(1, 1, 1, (0.8f - 0.8f * (_time / 1.5f)));
    }
}
