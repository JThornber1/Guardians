using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JT.GameStates
{
	[CreateAssetMenu(menuName = "Actions/OnMouseClick")]
	public class OnMouseClick : Action
	{
		public override void Execute(float d)
		{
			if (Input.GetMouseButtonDown(0))
			{

				List<RaycastResult> results = Settings.GetUIObjects();

				foreach (RaycastResult r in results)
				{
					IClicker c = r.gameObject.GetComponentInParent<IClicker>();
					if (c != null)
					{
						c.OnClick();
						break;
					}
				}
			}
		}
	}
}