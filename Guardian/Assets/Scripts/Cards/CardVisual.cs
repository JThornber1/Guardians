using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JT
{
	public class CardVisual : MonoBehaviour
	{
		public Card card;

		public CardVisualProperties[] properties;

		public GameObject statsHolder;
		public GameObject resourceHolder;

		public void LoadCard(Card c)
		{
			if (c == null)
				return;

			card = c;

			c.cardType.OnSetType(this);

			CloseAll();

			for (int i = 0; i < c.properties.Length; i++)
			{
				CardProperties cp = c.properties[i];

				CardVisualProperties p = GetProperty(cp.elements);

				if (p == null)
				continue;
				
				if (cp.elements is ElementsInt)
				{
					p.text.text = cp.intValue.ToString();
					p.text.gameObject.SetActive(true);
					Debug.Log("Reached Int");
				}
				else
				if (cp.elements is ElementsText)
				{
					p.text.text = cp.stringValue;
					p.text.gameObject.SetActive(true);
					Debug.Log("Reached Text");
				}
				else
				if (cp.elements is ElementsImage)
				{
					p.image.sprite = cp.sprite;
					p.image.gameObject.SetActive(true);
					Debug.Log("Reached Image");
				}
			}

		}

		public void CloseAll()
		{
			foreach (CardVisualProperties p in properties)
			{
				if (p.image != null)
					p.image.gameObject.SetActive(false);

				if (p.text != null)
					p.text.gameObject.SetActive(false);
			}
		}

		public CardVisualProperties GetProperty(Elements e)
		{
			CardVisualProperties result = null;
			
			for (int i = 0; i < properties.Length; i++)
			{
				result = properties[i];
				break;
			}

			return result;
		}
	}
}
