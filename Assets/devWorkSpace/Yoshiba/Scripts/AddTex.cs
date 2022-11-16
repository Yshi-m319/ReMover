using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class AddTex : MonoBehaviour
    {
        private Renderer _ren;
        [SerializeField] private Texture2D tex1;
        [SerializeField] private Texture2D tex2;

        private List<Color32> color1, color2;
        Texture2D tex ;
        // Start is called before the first frame update
        void Start()
        {
            _ren = GetComponent<Renderer>();
            color1 = new List<Color32>(tex1.GetPixels32());
            color2 = new List<Color32>(tex2.GetPixels32());
            tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        }

        private bool flag = false;
        // Update is called once per frame
        void Update()
        {
            if(!flag)
                setTex();
        }
        static int size = 256;
        
        void setTex()
        {
            
            var bytes = new List<Color32>();
            for (var i = 0; i < size * size; i++)
            {
                var value =addColor(color1[i]  ,color2[i]);
                bytes.Add(value);
            }
            tex.SetPixels32(bytes.ToArray());
            _ren.material.mainTexture = tex;
            tex.Apply();
        }

        private Color32 addColor(Color32 x, Color32 y)
        {
            var r = (byte)(x.r + y.r);
            var g = (byte)(x.g + y.g);
            var b = (byte)(x.b + y.b);
            var a = (byte)(x.a + y.a);

            return new Color32(r, g, b, a);
        }
    }
}
