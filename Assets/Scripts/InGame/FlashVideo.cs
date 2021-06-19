using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace InGame
{
	[System.Serializable]
	public class VideoItem
	{
		public Team actor1;
		public Team actor2;
		public VideoClip video;

	}
	public class FlashVideo : MonoBehaviour
	{
		public VideoPlayer videoPlayer;
		public List<VideoItem> trapVideos = new List<VideoItem>();
		public List<VideoItem> killVideos = new List<VideoItem>();

		private void Start()
		{
			gameObject.SetActive(false);
			videoPlayer.targetCamera = GameManager.Instance.activeCamera.gameObject.GetComponent<Camera>();
		}

		private void Update()
		{
			if (videoPlayer.targetCamera != GameManager.Instance.activeCamera)
			{
				videoPlayer.targetCamera = GameManager.Instance.activeCamera.gameObject.GetComponent<Camera>();
			}
		}

		public IEnumerator PlayTrapVideo(Team actor)
		{
			videoPlayer.clip = GetVideo(trapVideos, actor);
			videoPlayer.Play();

			while ((int)videoPlayer.frame < (int)videoPlayer.frameCount - 1)
			{
				yield return null;
			}

			gameObject.SetActive(false);
		}

		public IEnumerator PlayKillVideo(Team actor1, Team actor2)
		{
			videoPlayer.clip = GetVideo(killVideos, actor1, actor2);
			videoPlayer.Play();

			while ((int)videoPlayer.frame < (int)videoPlayer.frameCount - 1)
			{
				yield return null;
			}

			gameObject.SetActive(false);
		}

		public VideoClip GetVideo(List<VideoItem> videos, Team actor1, Team actor2 = Team.NONE)
		{
			if (actor2 == Team.NONE)
			{
				return videos.Find(videoItem => videoItem.actor1 == actor1).video;
			}
			else
			{
				return videos.Find(videoItem => videoItem.actor1 == actor1 && videoItem.actor2 == actor2).video;
			}
		}
	}
}