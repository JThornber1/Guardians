using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Holders/Card Holder")]
	public class CardHolders : ScriptableObject
	{
		public JT.TransformVariable handGrid;
		public JT.TransformVariable resourcesGrid;
		public JT.TransformVariable downGrid;
		public JT.TransformVariable battleLine;

		[System.NonSerialized]
		public PlayerHolder playerHolder;

		public void SetCardOnBattleLine(CardInstance card)
		{
			Vector3 position = card.visual.gameObject.transform.position;

			Settings.SetParentForCard(card.visual.gameObject.transform, battleLine.value.transform);

			position.z = card.visual.gameObject.transform.position.z;
			position.y = card.visual.gameObject.transform.position.y;
			card.visual.gameObject.transform.position = position;
		}

		public void SetCardDown(CardInstance card)
		{
			Settings.SetParentForCard(card.visual.gameObject.transform, downGrid.value.transform);
		}

		public void LoadPlayer(PlayerHolder p, PlayerStatsUI statsUI)
		{
			if (p == null)
				return;
			playerHolder = p;
			p.currentHolder = this;

			foreach (CardInstance c in p.cardsDown)
			{
				Settings.SetParentForCard(c.visual.gameObject.transform, downGrid.value.transform);
			}

			foreach (CardInstance c in p.handCards)
			{
				if (c.visual != null)
				{
					Settings.SetParentForCard(c.visual.gameObject.transform, handGrid.value.transform);
				}
			}

			foreach (ResourceHolder c in p.resourcesList)
			{
				Settings.SetParentForCard(c.cardObject.transform, resourcesGrid.value.transform);
			}

			p.statsUI = statsUI;
			p.LoadPlayerOnStatsUI();
		}
	}
}
