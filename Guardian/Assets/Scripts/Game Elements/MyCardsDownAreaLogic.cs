using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Areas/MyCardsDownWhenHoldingCard")]
	public class MyCardsDownAreaLogic : AreaLogic
	{
		public CardVariable card;
		public CardType creatureType;
		public CardType resourceType;

		public JT.TransformVariable areaGrid;
		public JT.TransformVariable resourceGrid;

		public GameElements.GameElementLogic cardDownLogic;

		public override void Execute()
		{
			if (card.value == null)
			return;

			Card c = card.value.visual.card;

			if (c.cardType == creatureType) //Allows the card to be placed in the grid.
			{
				Debug.Log("Place card down!");

				bool canUse = Settings.gameManager.currentPlayer.CanUseCard(c);

				if (canUse)
				{
					Settings.DropCreatureCard(card.value.transform, areaGrid.value.transform, card.value);
					card.value.currentLogic = cardDownLogic;
				}
				else
				{
					Settings.RegisterEvent("Not Enough Resources To Use Card", Color.white);
				}

				card.value.gameObject.SetActive(true);
				
				//Place Card Down
			}
			else
			if (c.cardType == resourceType)
			{
				Debug.Log("Place resource card down!");

				bool canUse = Settings.gameManager.currentPlayer.CanUseCard(c);

				if (canUse)
				{
					Settings.SetParentForCard(card.value.transform, resourceGrid.value.transform);
					card.value.currentLogic = cardDownLogic;
					Settings.gameManager.currentPlayer.AddResourceCard(card.value.gameObject);
				}
				else
				{
					Settings.RegisterEvent("Can't Drop More Than One Resource Cards Per Turn", Color.white);
				}

				card.value.gameObject.SetActive(true);
				//Place resource card down
			}
		}
	}
}