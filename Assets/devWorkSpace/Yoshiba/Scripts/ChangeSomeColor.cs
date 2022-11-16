using UnityEditor;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class ChangeSomeColor : MonoBehaviour
    {
        [SerializeField] private Color color;

        private Material _material;
        // Start is called before the first frame update

        void Start()
        {
            this._material = this.GetComponent<Renderer>().material;
            this._material.color = color;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
