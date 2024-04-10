using UnityEngine;

namespace Script
{
	public class AnimationHelperZS {
		
		public static float GetAnimationLength(Animator animator, string animName){
			if(animator.isInitialized){
				RuntimeAnimatorController ac = animator.runtimeAnimatorController;
				for(int i = 0; i<ac.animationClips.Length; i++){
					if(ac.animationClips[i].name == animName){
						return ac.animationClips[i].length;
					}
				}
			}
			return 0;
		}
		
		public static float GetCurrentStateTime(Animator animator, int layer){
			AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(layer);
			AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(layer);
			float myTime = myAnimatorClip[layer].clip.length * animationState.normalizedTime;
			return myTime;
		}
	}
}
