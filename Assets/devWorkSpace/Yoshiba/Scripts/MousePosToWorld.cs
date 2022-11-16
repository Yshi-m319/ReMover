using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class MousePosToWorld : MonoBehaviour
    {
        private Camera _camera;

        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            var screenPos = Input.mousePosition;
            screenPos.z = 10.0f;
            var worldPos = _camera.ScreenToWorldPoint(screenPos);
            //Debug.Log(worldPos);
            this.transform.position = worldPos;

        }
    }
}
