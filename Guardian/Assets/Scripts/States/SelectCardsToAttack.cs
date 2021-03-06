﻿using JT.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JT
{
	[CreateAssetMenu(menuName = "Actions/SelectCardsToAttack")]
	public class SelectCardsToAttack : Action
	{
		public override void Execute(float d)
		{
			if (Input.GetMouseButtonDown(0))
			{

				List<RaycastResult> results = Settings.GetUIObjects();

				foreach (RaycastResult r in results)
				{
					CardInstance inst = r.gameObject.GetComponentInParent<CardInstance>();
					PlayerHolder p = Settings.gameManager.currentPlayer;

					if (!p.cardsDown.Contains(inst))
						return;

					if (inst.CanAttack())
					{
						p.attackingCards.Add(inst);
						p.currentHolder.SetCardOnBattleLine(inst);
					}
				}
			}
		}
	}
}
