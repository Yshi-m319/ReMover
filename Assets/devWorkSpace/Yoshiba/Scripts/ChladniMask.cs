using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
    [CreateAssetMenu(menuName = "Create ChladniMask"),Serializable]    
    public class ChladniMask : ScriptableObject
    {
        [SerializeField] private Texture2D tex;
        [SerializeField,Range(0,7)] int index;
        
        [NonSerialized]public float life=0f;
        [NonSerialized]public bool isActive=false;
        private static float _lim = 3f;
        public static float Lim => _lim;
        public Texture2D Tex => tex;
        public int Num => index;
        public float Alpha
        {
            get
            {
                var t = Mathf.Abs(life -_lim)/_lim;
                
                if (t == 0) t = 0.001f;
                var y = Mathf.Cos(Mathf.Pow(t, 5f) / (0.01f * Mathf.PI)) * (0.9f/t);
                return Mathf.Clamp01(y);
            }
        }

        public ChladniMask()
        {
            
        }
        
        public ChladniMask clone()
        {
            return MemberwiseClone() as ChladniMask;
        }
        
    }
}
