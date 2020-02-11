using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOptions : MonoBehaviour
{
	public GameObject plane;
	public GameObject canvas;

	public void ActivateOptions()
	{
		plane.SetActive(!plane.activeSelf);
		canvas.SetActive(false);
	}
}
