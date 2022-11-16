using devWorkSpace.SoundTeam.Scripts;
using UnityEngine;

namespace devWorkSpace.knd.Scripts
{
    public class PlayerMove : MonoBehaviour
    {
        float playerSpeed = 5.0f; 
        float rotateSpeed = 700f;
        float jumpHeight = 0.7f;
        private float gravityScale_ = -9.81f;
        public Vector3 _playerVelocity;
        private CharacterController _charaCon;
        private Vector3 _playerPos;
        private Vector3 addForceDownPower = new Vector3(0f, -5f, 0f);
        private bool _beforeGrounded;
        bool isGrounded;
        SE _se;

        private Animator animator;
        private string moveStr = "isMove";
        private string jumpStr = "isJump";

        private Vector3 _goalPos;
        public bool _isGoal;

        void Start()
        {
            _playerPos = this.transform.position;
            _charaCon = GetComponent<CharacterController>();
            var sfx = GameObject.Find("SE");
            _se = sfx.GetComponent<SE>();
            animator = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>();
        }
        public void goal()
        {
            _goalPos = this.transform.position;
            _playerVelocity = new Vector3(0, 0, 0);
            _playerPos = _goalPos;
            _isGoal = true;
        }

        void Update()
        {
            isGrounded = _charaCon.isGrounded;
            if (isGrounded && _playerVelocity.y < 0)     
            {
                _playerVelocity.y = 0f;                
            }

            if (Lift.stopMove != 1)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _charaCon.Move(new Vector3(-1f, 0f, 0f) * (Time.deltaTime * playerSpeed));
                    animator.SetBool(moveStr, true);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _charaCon.Move(new Vector3(1f, 0f, 0f) * (Time.deltaTime * playerSpeed));
                    animator.SetBool(moveStr, true);
                }
                else
                {
                    animator.SetBool(moveStr, false);
                }

                if (isGrounded == true)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        _playerVelocity.y = jumpHeight * -gravityScale_;
                        _se.play(SENameList.Jump);
                        animator.SetBool(jumpStr, true);
                    }
                    else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    {
                        _playerVelocity += addForceDownPower;
                        animator.SetBool(moveStr, true);
                    }
                }
            }

            if (!isGrounded && _beforeGrounded)
            {
                animator.SetBool(jumpStr, false);
            }

            _beforeGrounded = isGrounded;

            _playerVelocity.y += gravityScale_ * Time.deltaTime;
            _charaCon.Move(_playerVelocity * Time.deltaTime);

            Vector3 diff = transform.position - _playerPos;
            diff = new Vector3(diff.x, 0f, 0f);

            if (diff.magnitude > 0.01f)
            {
                diff = new Vector3(diff.x, 0f, -0.005f);
                Quaternion qua = Quaternion.LookRotation(diff);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
            }
            _playerPos = transform.position; //プレイヤーの位置を更新

            /*/
            if (isGrounded == true && _beforeGrounded == true)
            {
                se.PlayLandG();
            }

            if (isGrounded == false)
            {
                _beforeGrounded = true;
            }
            else
            {
                _beforeGrounded = false;
            }
            /*/

            if (this.transform.position.z != 0.0f)
            {
                var thisTr = this.transform;
                var p = thisTr.position;
                p.z = 0.0f;
                thisTr.position = p;
            }

            if (_isGoal)
            {
                _playerVelocity = new Vector3(0, 0, 0);
                this.transform.position = _goalPos;
            }

        }
    }
}
