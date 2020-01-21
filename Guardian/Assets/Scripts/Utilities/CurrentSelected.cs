using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	public class CurrentSelected : MonoBehaviour
	{
		public CardVariable currentCard;

		public CardVisual cardVisual;

		Transform mouseTransform;

		public void LoadCard()
		{
			if (currentCard.value == null)
			return;

			currentCard.value.gameObject.SetActive(false);
			cardVisual.LoadCard(currentCard.value.visual.card);
			cardVisual.gameObject.SetActive(true);
		}

		public void ClosedCard()
		{
			cardVisual.gameObject.SetActive(false);
		}

		private void Start()
		{
			mouseTransform = this.transform;
			ClosedCard();
		}

		void Update()
		{
			mouseTransform.position = Input.mousePosition;
		}
	}
}