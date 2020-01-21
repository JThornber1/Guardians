using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Managers/Resource Manager")]
	public class ResourcesManager : ScriptableObject
	{
		public Elements typeElement;
		public Card[] allCards;

		Dictionary<string, Card> cardsDictionary = new Dictionary<string, Card>();

		public void Init()
		{
			cardsDictionary.Clear();

			for (int i = 0; i < allCards.Length; i++)
			{
				cardsDictionary.Add(allCards[i].name, allCards[i]);
			}
		}

		public Card GetCardInstance(string id)
		{
			Card originalCard = GetCard(id);
			if (originalCard == null)
				return null;

			Card newInst = Instantiate(originalCard);
			newInst.name = originalCard.name;
			return newInst;
		}

		Card GetCard(string id)
		{
			Card result = null;
			cardsDictionary.TryGetValue(id, out result);
			return result;
		}
	}
}