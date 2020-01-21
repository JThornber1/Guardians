using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JT.GameElements;

namespace JT
{
	[CreateAssetMenu(menuName = "Holders/Player Holder")]
	public class PlayerHolder : ScriptableObject
	{
		public Sprite potrait;

		public string username;
		public string[] startingCards;

		public Color playerColor;

		public int resourcesPerTurn = 1;

		[System.NonSerialized]
		public int resourcesDroppedThisTurn;

		[System.NonSerialized]
		public int health = 20;

		public bool isHumanPlayer;

		public PlayerStatsUI statsUI;

		public GameElementLogic handLogic;
		public GameElementLogic downLogic;

		[System.NonSerialized]
		public CardHolders currentHolder;

		[System.NonSerialized]
		public List<CardInstance> handCards = new List<CardInstance>();

		[System.NonSerialized]
		public List<CardInstance> cardsDown = new List<CardInstance>();

		[System.NonSerialized]
		public List<CardInstance> attackingCards = new List<CardInstance>();

		[System.NonSerialized]
		public List<ResourceHolder> resourcesList = new List<ResourceHolder>();

		private void OnEnable()
		{
			health = 20;
		}

		public int resourcesCount
		{
			get { return currentHolder.resourcesGrid.value.GetComponentsInChildren<CardVisual>().Length; }
		}

		public void AddResourceCard(GameObject cardObject)
		{
			ResourceHolder resourceHolder = new ResourceHolder
			{
				cardObject = cardObject
			};

			resourcesList.Add(resourceHolder);
			resourcesDroppedThisTurn++;

			Settings.RegisterEvent(username + " Drops Resources Card", Color.white);

		}

		public int NonUsedCards()
		{
			int result = 0;

			for (int i = 0; i < resourcesList.Count; i++)
			{
				if(!resourcesList[i].isUsed)
				{
					result++;
				}
			}

			return result;
		}

		public bool CanUseCard(Card c)
		{
			bool result = false;

			if (c.cardType is CreatureCard || c.cardType is SpellCard)
			{
				int currentResources = NonUsedCards();
				if (c.cost <= currentResources)
					result = true;
			}
			else
			{
				if (c.cardType is ResourceCard)
				{
					if (resourcesPerTurn - resourcesDroppedThisTurn > 0)
					{
						result = true;
					}
				}
			}

			return result;
		}

		public void DropCard(CardInstance inst)
		{
			if (handCards.Contains(inst))
				handCards.Remove(inst);

			cardsDown.Add(inst);

			Settings.RegisterEvent(username + " Used " + inst.visual.card.name + " For " + inst.visual.card.cost + " Resources", Color.white);
		}

		public List<ResourceHolder> GetUnusedResources()
		{
			List<ResourceHolder> result = new List<ResourceHolder>();

			for (int i = 0; i < resourcesList.Count; i++)
			{
				if (!resourcesList[i].isUsed)
				{
					result.Add(resourcesList[i]);
				}
			}

			return result;
		}

		public void MakeAllResourceCardsUsable()
		{
			for (int i = 0; i < resourcesList.Count; i++)
			{
				resourcesList[i].isUsed = false;
				resourcesList[i].cardObject.transform.localEulerAngles = Vector3.zero;
			}

			resourcesDroppedThisTurn = 0;
		}

		public void UseResourceCards(int amount)
		{
			Vector3 eular = new Vector3(0, 0, 90);

			List<ResourceHolder> l = GetUnusedResources();

			for (int i = 0; i < amount; i++)
			{
				l[i].isUsed = true;
				l[i].cardObject.transform.localEulerAngles = eular;
			}
		}

		public void LoadPlayerOnStatsUI()
		{
			if (statsUI != null)
			{
				statsUI.player = this;
				statsUI.UpdateAll();
			}
		}

		public void DoDamage(int v)
		{
			health -= v;

			if (statsUI != null)
				statsUI.UpdateHealth();
		}
	}
}