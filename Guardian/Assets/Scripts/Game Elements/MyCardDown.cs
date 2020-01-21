using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT.GameElements
{
	[CreateAssetMenu(menuName = "Game Elements/My Card Down")]
	public class MyCardDown : GameElementLogic
	{
		public override void OnClick(CardInstance instance)
		{
			Debug.Log("Card in play");
		}
		
		public override void OnHighlight(CardInstance instance)
		{
		
		}


	}
}
