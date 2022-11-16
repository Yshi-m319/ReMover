using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
	public class WaveShooter : MonoBehaviour
	{
		[FormerlySerializedAs("waveShot")] [SerializeField] private GameObject bullet;
		[SerializeField] private GameObject		pauseMenu;
		[SerializeField] private GameObject		goalMenu;
		[SerializeField] private GameObject		leadDirection;
			
		[SerializeField] private PitchData		wavePitch;
			
		[SerializeField] private float			bpm;
						 private float			_coolTime;
		
						 private PlayerUtility	_pUtil;

						 private SE				_se;
		//public PitchData forkPitch;

		public PitchData WavePitch
		{
			get => wavePitch;
			set => wavePitch = value;
		}

		// Start is called before the first frame update
		private void Start()
		{
			_pUtil = GetComponent<PlayerUtility>();
			_se = GameObject.FindGameObjectWithTag("SE").GetComponent<SE>();
			_coolTime = 60f / bpm; 
		}

		// Update is called once per frame
		private void Update()
		{
			_coolTime -= Time.deltaTime;
			
			var canShot = this.canShot();
			if (!canShot || !(_coolTime < 0)) return;
			
			var pPos = transform.position;
			var worldPoint = _pUtil.getMouseWorldPos();
			var angV = (worldPoint - pPos).normalized;
			var bulletPos = pPos + angV;
			var lookRotation = Quaternion.LookRotation(angV); ;
			
			
			moveAim(bulletPos,lookRotation);
			if (Input.GetMouseButtonDown(0)) 
			{
				shotWave(bulletPos,lookRotation);
				_coolTime = 60f / bpm;
			}
		}

		
		private void moveAim(Vector3 pos,Quaternion q)
		{
			leadDirection.transform.position = pos;
			leadDirection.transform.rotation = q;
		}
		
		private void shotWave(Vector3 pos,Quaternion q)
		{
			SoundWaveBehaviour.instantiate(bullet, pos, q, wavePitch);
			_se.playSoundWaveRelease(wavePitch.Num);
		}

		private bool canShot()
		{
			return !(pauseMenu.activeInHierarchy || goalMenu.activeInHierarchy);
		}
	}
}