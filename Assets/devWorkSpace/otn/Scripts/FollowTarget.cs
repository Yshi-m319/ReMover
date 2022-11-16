using UnityEngine;

namespace devWorkSpace.Otani.Scripts
{
    public class FollowTarget : MonoBehaviour
    {
        //追従されるターゲット
        [SerializeField] GameObject target;
        
        //このオブジェクトの位置zの値
        private float _z;
        void Start()
        {
            //初期位置のzの値を入れる
            _z = transform.position.z;
        }
    
        void Update()
        {
            var pos = target.transform.position;
            //xとyの値のみターゲットと同じにしつつ、zは初期位置から変えない
            transform.position = new Vector3(pos.x, pos.y, _z);
        }
    }
}
