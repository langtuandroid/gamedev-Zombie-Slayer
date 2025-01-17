﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

	/// <summary>
	/// Various static methods used throughout the Infinite Runner Engine and the Corgi Engine.
	/// </summary>

	public static class MMFadeZS
	{

		/// <summary>
		/// Fades the specified image to the target opacity and duration.
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="opacity">Opacity.</param>
		/// <param name="duration">Duration.</param>
		public static IEnumerator FadeImageE(Image target, float duration, Color color)
		{
			if (target==null)
				yield break;

			float alpha = target.color.a;

			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
			{
				if (target==null)
					yield break;
				Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
				target.color=newColor;
				yield return null;
			}
			target.color=color;

		}

		public static IEnumerator FadeTextT(Text target, float duration, Color color)
		{
			if (target==null)
				yield break;

			float alpha = target.color.a;

			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
			{
				if (target==null)
					yield break;
				Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
				target.color=newColor;
				yield return null;
			}			
			target.color=color;
		}

		public static IEnumerator FadeSprite(SpriteRenderer target, float duration, Color color)
		{
			if (target==null)
				yield break;

			float alpha = target.material.color.a;

			float t=0f;
			while (t<1.0f)
			{
				if (target==null)
					yield break;

				Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
				target.material.color=newColor;

				t += Time.deltaTime / duration;

				yield return null;

			}
			Color finalColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
			target.material.color=finalColor;
		}

	public static IEnumerator FadeSpriteRenderer(SpriteRenderer target, float duration, Color color)
	{
		if (target==null)
			yield break;

		float alpha = target.color.a;

		float t=0f;
		while (t<1.0f)
		{
			if (target==null)
				yield break;

			Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
			target.color=newColor;

			t += Time.deltaTime / duration;

			yield return null;

		}
		Color finalColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
		target.color=finalColor;
	}

	public static IEnumerator FadeTextureE(Material target, float duration, Color color)
	{
		if (target==null)
			yield break;

		float alpha = target.color.a;
		float r = target.color.r;
		float g = target.color.g;
		float b = target.color.b;

		float t=0f;
		while (t<1.0f)
		{
			if (target==null)
				yield break;

//			Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));

			Color newColor = new Color(Mathf.SmoothStep(r,color.r,t), Mathf.SmoothStep(g,color.g,t), Mathf.SmoothStep(b,color.b,t), Mathf.SmoothStep(alpha,color.a,t));
			target.color=newColor;

			t += Time.deltaTime / duration;

			yield return null;

		}
		Color finalColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha,color.a,t));
		target.color=finalColor;
	}

		public static IEnumerator FadeCanvasGroup(CanvasGroup target, float duration, float targetAlpha)
		{
			if (target==null)
				yield break;

			float currentAlpha = target.alpha;

			float t=0f;
			while (t<1.0f)
			{
				if (target==null)
					yield break;

				float newAlpha =Mathf.SmoothStep(currentAlpha,targetAlpha,t);
				target.alpha=newAlpha;

				t += Time.deltaTime / duration;

				yield return null;

			}
			target.alpha=targetAlpha;
		}

	}

