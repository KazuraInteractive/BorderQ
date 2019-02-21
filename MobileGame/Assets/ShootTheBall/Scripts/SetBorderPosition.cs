using UnityEngine;
using System.Collections;

public class SetBorderPosition : MonoBehaviour {

	float CameraOtrhoSize = 5F;
	public GameObject borderLeft;
	public GameObject borderRight;
	public GameObject borderTop;
	public GameObject borderBottom;

	void Awake()
	{
		CameraOtrhoSize = Camera.main.orthographicSize;
	}

	void Start()
	{
		UpdateBorderPosition ();
	}

	void UpdateBorderPosition()
	{
		float screenHeight = CameraOtrhoSize * 2F;
		float screenWidth = ((((float) Screen.width) / ((float) Screen.height)) * screenHeight);

		borderLeft.transform.position = new Vector3 (-(screenWidth / 2F), 0, 0);
		borderRight.transform.position = new Vector3 ((screenWidth / 2F), 0, 0);
		borderTop.GetComponent<BoxCollider2D> ().size = new Vector2 (screenWidth, 0.1F);
		borderBottom.GetComponent<BoxCollider2D> ().size = new Vector2 (screenWidth, 0.1F);
	}
}
