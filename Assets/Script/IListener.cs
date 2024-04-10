using UnityEngine;
using System.Collections;

public interface IListener  {
	void IPlayY();
	void ISuccessS();
	void IPauseE ();
	void IUnPauseE();
	void IGameOverR ();
	void IOnRespawnN ();
	void IOnStopMovingOnN ();
	void IOnStopMovingOffF ();
}
