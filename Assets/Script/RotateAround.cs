using UnityEngine;
using System.Collections;
using Script;

public class RotateAround : MonoBehaviour, IListener {
	public enum Type{Clk, CClk}
	public Type rotateType;
	public float speed = 0.5f;
	
	// Update is called once per frame
	void Update () {
        if (isStop)
            return;

        transform.Rotate (Vector3.forward, Mathf.Abs (speed) * (rotateType == Type.CClk ? 1 : -1));
	}

    bool isStop = false;
    #region IListener implementation

    public void IPlayY()
    {
        //		throw new System.NotImplementedException ();
    }

    public void ISuccessS()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IPauseE()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IUnPauseE()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IGameOverR()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IOnRespawnN()
    {
        //		throw new System.NotImplementedException ();
    }

    public void IOnStopMovingOnN()
    {
        Debug.Log("IOnStopMovingOn");
        //		anim.enabled = false;
        isStop = true;

    }

    public void IOnStopMovingOffF()
    {
        //		anim.enabled = true;
        isStop = false;
    }

    #endregion
}
