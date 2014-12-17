using UnityEngine;
using System.Collections;

public class MasterGUI : MonoBehaviour {

	public ButtonClass distance, healthbar;
	public float distanceCovered, health = 1;
	public Material healthBarCut;

	void Update()
	{
		distance.content.text = (distanceCovered/1000).ToString("F2") + " KM";
		healthBarCut.SetFloat ("_Cutoff", Mathf.Lerp (1, 0, health));
	}

	void OnGUI(){
		GUI.Button (distance.AnchoredRect(), distance.content, distance.style);
		Graphics.DrawTexture (healthbar.AnchoredRect(), healthbar.content.image, healthBarCut);
	}
}

public enum Anchor{
	TopLeft,
	TopCenter,
	TopRight,
	MiddleLeft,
	MiddleCenter,
	MiddleRight,
	BottomLeft,
	BottomCenter,
	BottomRight};

[System.Serializable]
public class ButtonClass {

	public string title;
	[SerializeField]
	public Anchor anchorPoint;
	public Rect rect;
	public float scale;
	public GUIContent content;
	public GUIStyle style;
	public GameObject menuObject;
	
	public Rect AnchoredRect()
	{
		Rect output = rect;
		output.width *= scale * (Screen.width / 814.0F);
		output.height *= scale * (Screen.height / 458.0F);
		
		switch (anchorPoint)
		{
		case Anchor.TopLeft:
			output.x = (Screen.width * rect.x);
			output.y = (Screen.height * rect.y);
			break;
		case Anchor.TopCenter:
			output.x = ((Screen.width - output.width )/ 2) + (Screen.width * rect.x);
			output.y = (Screen.height * rect.y);
			break;
		case Anchor.TopRight:
			output.x = (Screen.width - output.width ) + (Screen.width * rect.x);
			output.y = (Screen.height * rect.y);
			break;
		case Anchor.MiddleLeft:
			output.x = (Screen.width * rect.x);
			output.y = (Screen.height / 2) + (Screen.height * rect.y);
			break;
		case Anchor.MiddleCenter:
			output.x = ((Screen.width - output.width )/ 2) + (Screen.width * rect.x);
			output.y = (Screen.height / 2) + (Screen.height * rect.y);
			break;
		case Anchor.MiddleRight:
			output.x = (Screen.width - output.width ) + (Screen.width * rect.x);
			output.y = (Screen.height / 2) + (Screen.height * rect.y);
			break;
		case Anchor.BottomLeft:
			output.x = (Screen.width * rect.x);
			output.y = (Screen.height - output.height) + (Screen.height * rect.y);
			break;
		case Anchor.BottomCenter:
			output.x = ((Screen.width - output.width )/ 2) + (Screen.width * rect.x);
			output.y = (Screen.height - output.height) + (Screen.height * rect.y);
			break;
		case Anchor.BottomRight:
			output.x = (Screen.width - output.width ) + (Screen.width * rect.x);
			output.y = (Screen.height - output.height) + (Screen.height * rect.y);
			break;
		default:
			break;
		}
		return output;
	}
}