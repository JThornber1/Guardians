using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayGame : MonoBehaviour
{
	public GameObject plane;
	public GameObject canvas;

	public void ActivatePlayGame()
	{
		plane.SetActive(!plane.activeSelf);
		canvas.SetActive(false);
	}
}
