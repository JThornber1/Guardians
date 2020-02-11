using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCredits : MonoBehaviour
{
	public GameObject plane;
	public GameObject canvas;
	public GameObject canvas2;

	public Animator credits;

    public void ActivateCredits()
    {
		credits.SetTrigger("Credits");
		plane.SetActive(!plane.activeSelf);

		StartCoroutine(WaitFor());
	}

	IEnumerator WaitFor()
	{
		canvas.SetActive(false);
		yield return new WaitForSeconds(5f);
		canvas2.SetActive(true);

		yield return null;
	}
}