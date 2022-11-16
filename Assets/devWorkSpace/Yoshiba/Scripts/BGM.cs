using System;
using System.Collections;
using UnityEngine;
// ReSharper disable InconsistentNaming
namespace devWorkSpace.SoundTeam.Scripts
{
	public static class CueSheetName
	{
		
		public const string BGM1 = "BGM";	//チュートリアルと森とタイトル
		public const string BGM2 = "BGM2";	//ラボと深部
	}
	public static class BGMNameList
	{
		public enum cueID
		{
			Tutorial=6,
			Lab=100
		}
		public const string TutorialIntro = "Bgm05_Tutorial_intro";
		public const string Labo = "Bgm06_Laboratory";
	}
	public static class AisacNameList
	{
		public const string TutorialToForest =		"TtoF";
		public const string TutorialPitchChange =	"TPitchChange";
		public const string ForestPitchChange =		"FPitchChange";
		public const string LabPitchChange =		"LPitchChange";
		public const string RemPitchChange =		"RPitchChange";
		public const string MusicBox =				"Musicbox";
		public const string LabMimic =				"LMimic";
		public const string LabMimicDistance =		"LMimicDistance";
		public const string MimicReduce =			"MimicReduce";
		public const string RemMimic =				"RMimic";
		public const string RemMimicDistance =		"RMimicDistance";
	}

	
	public class BGM
	{
		private CriAtomExPlayback _playback;
		private readonly CriAtomExPlayer _player;

            
		public BGM(string cueSheetName,string cueName)
		{
			_player = new CriAtomExPlayer();
			//_playback = new CriAtomExPlayback(CriAtomExPlayback.invalidId);
			Debug.Log($"sheet:{cueSheetName}\ncueName:{cueName}");
			var currentAcb = CriAtom.GetAcb(cueSheetName);
			_player.SetCue(currentAcb,cueName);
		}
		
		public BGM(string cueSheetName,BGMNameList.cueID cueId)
		{
			_player = new CriAtomExPlayer();
			var currentAcb = CriAtom.GetAcb(cueSheetName);
			_player.SetCue(currentAcb,(int)cueId);
		}

		/**
	 * <summary>Aisacコントロール値の一括セット</summary>
	 * <param name="ctrlNames">コントロール名の配列</param>
	 * <param name="value">コントロール値</param>
	 * <example><code>
	 *	BGM bgm=new BGM("BGMxx");
	 *	var names=new[]{"TtoF","TPitchChange"};
	 *	bgm.setAisacControl(names,0.5f);
	 *	bgm.play();
	 * </code></example>
	 **/
		public void setAisacControl(string[] ctrlNames,float value=0f)
		{
			value=Mathf.Clamp01(value);
			if (ctrlNames.Length > 8)
				throw new Exception();
			foreach (var ctrlName in ctrlNames)
			{
				setAisacControl(ctrlName,value);
			}
		}
		/**
	 * <summary>Aisacコントロール値のセット</summary>
	 * <param name="ctrlName">コントロール名</param>
	 * <param name="value">コントロール値</param>
	 * <example><code>
	 *	BGM bgm=new BGM("BGMxx");
	 *	bgm.setAisacControl("TtoF",0.5f);
	 *	bgm.setAisacControl("TPitchChange",0.5f);
	 *	bgm.play();
	 * </code></example>
	 **/
		public void setAisacControl(string ctrlName,float value=0f)
		{
			value=Mathf.Clamp01(value);
			_player.SetAisacControl(ctrlName,value);
		}

		public void changeAisac(string ctrlName,float value)
        {
			setAisacControl(ctrlName, value);
			update();
        }
	
		/**
	 * <summary>Aisacコントロール値を位置情報を元に境界状に設定します</summary>
	 * <param name="ctrlName">コントロール名</param>
	 * <param name="start">境界のはじまり</param>
	 * <param name="end">境界の終わり</param>
	 * <param name="pos">変化させる値</param>
	 **/
		public void changeAisacFromPositionBorder(string ctrlName, Vector2 start, Vector2 end, Vector2 pos)
		{
			var x = Mathf.InverseLerp(start.x, end.x, pos.x);
			var y = Mathf.InverseLerp(start.y, end.y, pos.y);
			var param = Mathf.Sqrt(x * x + y * y);
			setAisacControl(ctrlName, param);
			update();
		}
		
		/**
	 * <summary>Aisacコントロール値を位置情報を元に円状に設定します</summary>
	 * <param name="ctrlName">コントロール名</param>
	 * <param name="r">境界の半径</param>
	 * <param name="point">境界の中心</param>
	 * <param name="pos">変化させる値</param>
	 **/
		public void changeAisacFromPositionPoint(string ctrlName, float r,Vector2 point,Vector2 pos,
			float value0=0f,float value1=1f)
		{
			var dis = (point - pos).magnitude;
			var border = r * pos.normalized;
			var t = Mathf.InverseLerp(r,0,dis)*value1;
			var param = Mathf.Lerp(value0, value1, t);
			setAisacControl(ctrlName, param);
			update();
		}

		/**
	 *  <summary>Aisacコントロール値を時間で増やしていく(線形補間)</summary>
	 *  <param name="ctrlName">コントロール名</param>
	 *  <param name="value">コントロール値</param>
	 *  <param name="changeTime">何秒かけて変化するか</param> 
	 **/
		public IEnumerator changeAisacFromTime(string ctrlName, float value = 0f, float changeTime = 0f)
		{
			yield return changeAisacFromTime(ctrlName, value, changeTime, (Mathf.Lerp));
		}

		/**
	 *  <summary>Aisacコントロール値を時間で増やしていく(補間関数指定)</summary>
	 *  <param name="ctrlName">コントロール名</param>
	 *  <param name="value">コントロール値</param>
	 *  <param name="changeTime">何秒かけて変化するか</param>
	 *  <param name="func">補間用関数(float f(float a,float b, float c))</param> 
	 * <example><code>
	 *	BGM bgm=new BGM("BGMxx");
	 *	bgm.setAisacControl("TtoF",0.5f);
	 *	bgm.play();
	 *	changeAisacFromTime("TtoF",0.5f,3f,ExampleMath);
	 * </code></example>
	 */
		public IEnumerator changeAisacFromTime(string ctrlName, float value, float changeTime,Func<float,float,float,float> func)
		{
			//valueを0~1に制限している
			float targetValue = Mathf.Clamp01(value);

			//changeTime秒かけて音が変化する
			for (float t = 0f; t < changeTime; t += Time.deltaTime)
			{
				//0~targetValueまでの範囲で補間する
				var deltaValue = func(0f, targetValue, Mathf.Clamp01(t / changeTime));
				setAisacControl(ctrlName, deltaValue);
				update();
				yield return null;
			}
			setAisacControl(ctrlName, targetValue);
			update();
		}

		public void setBlockId(int id)
        {
			if(_playback.id != CriAtomExPlayback.invalidId)
            {
				if(_playback.GetCurrentBlockIndex() != id)
                {
					_playback.SetNextBlockIndex(id);
                }
            }
        }
	
		public void play()
		{
			_playback = _player.Start();
		}

		public void update()
		{
			_player.Update(_playback);
		}
		public void stop()
		{
			if (_playback.status == CriAtomExPlayback.Status.Playing)
			{
				_playback.Stop();
			}
		}
	}
}
