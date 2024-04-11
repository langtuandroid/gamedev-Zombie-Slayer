using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace Script
{
	public class JoystickCustom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			Both, 
			OnlyHorizontal, 
			OnlyVertical 
		}

        [Range(1, 100)]
        public int moveRangePercent = 5;
		private int movementRange = 10; 
		
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		private Vector3 mStartPos;
		private bool mUseX; // Toggle for using the x axis
		private bool mUseY; // Toggle for using the Y axis
		private CrossPlatformInputManager.VirtualAxis mHorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		private CrossPlatformInputManager.VirtualAxis mVerticalVirtualAxis; // Reference to the joystick in the cross platform input
		public Transform targetDotImage, targetRing;
		
		private void OnEnable()
		{
			CreateVirtualAxes();
		}

		private void Start()
        {
            movementRange = Screen.width * moveRangePercent / 100;
            mStartPos = targetDotImage.position;
        }

		private void UpdateVirtualAxes(Vector3 value)
		{
			var delta = mStartPos - value;
			delta.y = -delta.y;
			delta /= movementRange;
			if (mUseX)
			{
				mHorizontalVirtualAxis.Update(-delta.x);
			}

			if (mUseY)
			{
				mVerticalVirtualAxis.Update(delta.y);
			}
		}

		private void CreateVirtualAxes()
		{
			// set axes to use
			mUseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			mUseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (mUseX)
			{
				mHorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(mHorizontalVirtualAxis);
			}
			if (mUseY)
			{
				mVerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(mVerticalVirtualAxis);
			}
		}

		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;

			if (mUseX)
			{
				int delta = (int)(data.position.x - mStartPos.x);
//				Debug.LogError (delta);
//				delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (mUseY)
			{
				int delta = (int)(data.position.y - mStartPos.y);
//				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}

//			Debug.LogError (Vector3.ClampMagnitude (new Vector3 (newPos.x, newPos.y, newPos.z), MovementRange));
//			transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
			targetDotImage.transform.position = Vector3.ClampMagnitude( new Vector3(newPos.x, newPos.y, newPos.z),movementRange) + mStartPos;
			UpdateVirtualAxes(targetDotImage.transform.position);
		}


		public void OnPointerUp(PointerEventData data)
		{
//			targetDotImage.transform.position = m_StartPos;
//
			UpdateVirtualAxes(mStartPos);
			targetDotImage.gameObject.SetActive (false);
			targetRing.gameObject.SetActive (false);

		}
		
		public void OnPointerDown(PointerEventData data) {
			mStartPos = data.position;
			targetDotImage.transform.position = mStartPos;
			targetRing.transform.position = mStartPos;
			targetDotImage.gameObject.SetActive (true);
			targetRing.gameObject.SetActive (true);
		}

		private void OnDisable()
		{
			if (mUseX)
			{
				mHorizontalVirtualAxis.Remove();
			}
			if (mUseY)
			{
				mVerticalVirtualAxis.Remove();
			}
		}
	}
}