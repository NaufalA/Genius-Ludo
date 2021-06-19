using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	void Update()
	{
		if (Input.touchCount > 0)
		{
			if (Input.touchCount == 1)
			{
				Touch touch = Input.GetTouch(0);

				if (touch.phase == TouchPhase.Moved)
				{
					Quaternion target = Quaternion.Euler(0, touch.position.x, 0);
					transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f * Time.deltaTime);
				}
			}
		}
	}
}
