using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
	public class BackGroundControllerXZS : MonoBehaviour {
		[SerializeField] private bool autoScrolling = true;
		[SerializeField] private float autoScrollingSpeed = 1;

		[SerializeField] private enum Follow{FixedUpdate,Update}
		
		[SerializeField] private Follow timeBaseE;
		[FormerlySerializedAs("Background")] [SerializeField] private Renderer background;
		[SerializeField] private float speedBGG = 0.1f;
		[FormerlySerializedAs("Midground")] [SerializeField] private Renderer midground;
		[SerializeField] private float speedMGG = 0.2f;
		[FormerlySerializedAs("Forceground")] [SerializeField] private Renderer forceground;
		[SerializeField] private float speedFGG = 0.3f;

		private Camera targetT;
		private float startPosXx;
		private float x;

		// Use this for initialization
		private void Start () {
			targetT = Camera.main;
			startPosXx = targetT.transform.position.x;
		}
	
		// Update is called once per frame
		private void Update () {
			if (timeBaseE != Follow.Update)
				return;

			if (autoScrolling)
				x += Time.deltaTime * autoScrollingSpeed;
			else
				x = targetT.transform.position.x - startPosXx;

			if (background != null) {
				var offset = (x * speedBGG) % 1;
				background.material.mainTextureOffset = new Vector2 (offset, background.material.mainTextureOffset.y);
			}
			if (midground != null) {
				var offset = (x * speedMGG) % 1;
				midground.material.mainTextureOffset = new Vector2 (offset, midground.material.mainTextureOffset.y);
			}
			if (forceground != null) {
				var offset = (x * speedFGG) % 1;
				forceground.material.mainTextureOffset = new Vector2 (offset, forceground.material.mainTextureOffset.y);
			}
		}

		// Update is called once per frame
		private void FixedUpdate () {
			if (timeBaseE != Follow.FixedUpdate)
				return;

			if (autoScrolling)
				x += Time.fixedDeltaTime * autoScrollingSpeed;
			else
				x = targetT.transform.position.x - startPosXx;

			if (background != null) {
				var offset = (x * speedBGG) % 1;
				background.material.mainTextureOffset = new Vector2 (offset, background.material.mainTextureOffset.y);
			}
			if (midground != null) {
				var offset = (x * speedMGG) % 1;
				midground.material.mainTextureOffset = new Vector2 (offset, midground.material.mainTextureOffset.y);
			}
			if (forceground != null) {
				var offset = (x * speedFGG) % 1;
				forceground.material.mainTextureOffset = new Vector2 (offset, forceground.material.mainTextureOffset.y);
			}
		}
	}
}
