using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class FixAnyFPS : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private int fps = 60;
        private void Awake()
        {
            Application.targetFrameRate = fps;
        }

        private void OnDestroy()
        {
            Application.targetFrameRate = 60;
        }
    }
}

