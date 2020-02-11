using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMainMenuFromCredits : MonoBehaviour
{
	public GameObject plane;
	public GameObject canvas;
	public GameObject canvas2;

	public Animator mainMenu;

	public void ActivateMainMenu()
	{
		mainMenu.SetTrigger("Credits");
		plane.SetActive(!plane.activeSelf);

		StartCoroutine(WaitFor());
	}

	IEnumerator WaitFor()
	{
		canvas2.SetActive(false);
		yield return new WaitForSeconds(5f);
		canvas.SetActive(true);

		yield return null;
	}
}