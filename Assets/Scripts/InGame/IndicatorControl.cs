using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
	public class IndicatorControl : MonoBehaviour
	{
		public GameObject targetCamera;

		private void Start()
		{
			targetCamera = GameManager.Instance.activeCamera;
		}

		void Update()
		{
			if (targetCamera != GameManager.Instance.activeCamera)
			{
				targetCamera = GameManager.Instance.activeCamera;
			}
			transform.LookAt(targetCamera.transform.position);
		}
	}
}