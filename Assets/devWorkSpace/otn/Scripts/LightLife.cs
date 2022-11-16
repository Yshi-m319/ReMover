using UnityEngine;

namespace devWorkSpace.Otani.Scripts
{
    public class LightLife : MonoBehaviour
    {
        //Lightのオブジェクト
        [SerializeField] GameObject thisLight;
    
        //このオブジェクトの寿命
        private int _lifetime = 100;

        void Update()
        {
            //lifetimeの減少に従って光の明るさを段々失わせる
            this._lifetime--;
            thisLight.GetComponent<Light>().intensity = _lifetime / 100f;
        
            //lifetimeが無くなったらこのオブジェクトを消す
            if (_lifetime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
