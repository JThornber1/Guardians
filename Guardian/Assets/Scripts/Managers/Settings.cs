using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JT
{
	public static class Settings
	{
		public static GameManager gameManager;

		private static ResourcesManager resourcesManager;

		public static ResourcesManager GetResourcesManager()
		{
			if(resourcesManager == null)
			{
				resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
				resourcesManager.Init();
			}

			return resourcesManager;
		}

		private static ConsoleHook _consoleManager;

		public static void RegisterEvent(string e, Color color)
		{
			if (_consoleManager == null)
			{
				_consoleManager = Resources.Load("ConsoleHook") as ConsoleHook;
			}

			_consoleManager.RegisterEvent(e, color);
		}

		public static List<RaycastResult> GetUIObjects()
		{
			PointerEventData pointerData = new PointerEventData(EventSystem.current)
			{
				position = Input.mousePosition
			};

			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerData, results);
			return results;
		}

		public static void DropCreatureCard(Transform c, Transform p, CardInstance cardInst)
		{
            cardInst.isFlatfooted = true;
            //Execute Any Special Card Abilities On Drop

			SetParentForCard(c, p);

			cardInst.SetFlatFooted(true);
            
			gameManager.currentPlayer.UseResourceCards(cardInst.visual.card.cost);
			gameManager.currentPlayer.DropCard(cardInst);
		}

		public static void SetParentForCard(Transform c, Transform p)
		{
			c.SetParent(p);
			c.localPosition = Vector3.zero;
			c.localEulerAngles = Vector3.zero;
			c.localScale = Vector3.one;
		}
	}
}