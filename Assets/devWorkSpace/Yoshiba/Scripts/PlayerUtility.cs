using System;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace devWorkSpace.Yoshiba.Scripts
{
	public enum Stage
	{
		Missing,
		Tutorial,
		Forest,
		Labo,
		Remine
	}
	public class PlayerUtility : MonoBehaviour
	{
		private Vector3 _pPos;
		private const string _kTUTORIAL = "devTutorialAndStage1";
		private const string _kLAB = "devStage2";

		[SerializeField] Material[] mantaMat;
		private SkinnedMeshRenderer _mat;
		private WaveShooter _ws;
		
		private Camera _camera;
		private void Start()
		{
			_mat = gameObject.transform.GetChild(2).GetChild(2).GetChild(2).GetComponent<SkinnedMeshRenderer>();
			_ws = GetComponent<WaveShooter>();
			_camera = Camera.main;
		}

		public Stage IsWhere
		{
			get
			{
				
				var sName = SceneManager.GetActiveScene().name;
				_pPos = transform.position;
				switch (sName)
				{
					case _kTUTORIAL when _pPos.x <= 150f:
						return Stage.Tutorial;
					case _kTUTORIAL when _pPos.x > 150f:
						return Stage.Forest;
					case _kLAB when _pPos.x<=200f:
						return Stage.Labo;
					case _kLAB when _pPos.x > 200f:
						return Stage.Remine;
					default:
						return Stage.Missing;
				}
			}
		}
		
		public void changeManta()
		{
			_mat.material = mantaMat[_ws.WavePitch.Num-1];
		}
		
		public Vector3 getMouseWorldPos()
		{
			var screenPos = Input.mousePosition;
			screenPos.z = (transform.position - _camera.transform.position).z;
			return _camera.ScreenToWorldPoint(screenPos);
		}
	}
}	