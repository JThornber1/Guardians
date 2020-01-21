using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT.GameElements
{
	[CreateAssetMenu(menuName = "Game Elements/My Hand Card")]
	public class HandCard : GameElementLogic
	{
		public JT.GameEvent onCurrentCardSelected;
		public JT.GameStates.State holdingCard;

		public CardVariable currentCard;

		public override void OnClick(CardInstance instance)
		{
			currentCard.Set(instance);
			Settings.gameManager.SetState(holdingCard);
			onCurrentCardSelected.Raise();

			Debug.Log("Card in Hand");
		}
		
		public override void OnHighlight(CardInstance instance)
		{
		
		}


	}
}