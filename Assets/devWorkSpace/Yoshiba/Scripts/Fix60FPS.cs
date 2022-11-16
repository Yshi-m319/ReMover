using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class Fix60FPS : MonoBehaviour
    {
        // Start is called before the first frame update
        private const int _kFPS = 60;
        private void Awake()
        {
            Application.targetFrameRate = _kFPS;
        }
    }
}
