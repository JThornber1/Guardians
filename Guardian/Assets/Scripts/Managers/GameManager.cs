﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JT.GameStates;

namespace JT
{
	public class GameManager : MonoBehaviour
	{
        [System.NonSerialized]
		public PlayerHolder[] all_players;

		public PlayerHolder GetEnemyOf(PlayerHolder p)
		{
			for (int i = 0; i < all_players.Length; i++)
			{
				if (all_players[i] != p)
				{
					return all_players[i];
				}
			}

			return null;
		}

		public PlayerHolder currentPlayer;
		public CardHolders playerOneHolder;
		public CardHolders otherPlayersHolder;

		public State currentState;

		public GameObject cardPrefab;

		public int turnIndex;
		public Turn[] turns;
		public JT.GameEvent onTurnChanged;
		public JT.GameEvent onPhaseChanged;
		public JT.StringVariable turnText;

		public PlayerStatsUI[] statsUI;

        public static GameManager singleton;

        private void Awake()
        {
            singleton = this;

            all_players = new PlayerHolder[turns.Length];

            for (int i = 0; i < turns.Length; i++)
            {
                all_players[i] = turns[i].player;
            }

            currentPlayer = turns[0].player;
        }

        public void Start()
		{
			Settings.gameManager = this;

			SetupPlayers();

			CreateStartingCards();

			turnText.value = turns[turnIndex].player.username;
			onTurnChanged.Raise();
		}

		void SetupPlayers()
		{
			for (int i = 0; i < all_players.Length; i++)
			{
				if (all_players[i].isHumanPlayer)
				{
					all_players[i].currentHolder = playerOneHolder;
				}
				else
				{
					all_players[i].currentHolder = otherPlayersHolder;
				}

				if (i < 2)
				{
					all_players[i].statsUI = statsUI[i];
					statsUI[i].player.LoadPlayerOnStatsUI();
				}
			}
		}

		void CreateStartingCards()
		{
			ResourcesManager rm = Settings.GetResourcesManager();

			for (int p = 0; p < all_players.Length; p++)
			{
				for (int i = 0; i < all_players[p].startingCards.Length; i++)
				{
					GameObject go = Instantiate(cardPrefab) as GameObject;
					CardVisual v = go.GetComponent<CardVisual>();
					v.LoadCard(rm.GetCardInstance(all_players[p].startingCards[i]));
					CardInstance inst = go.GetComponent<CardInstance>();
					inst.currentLogic = all_players[p].handLogic;
					Settings.SetParentForCard(go.transform, all_players[p].currentHolder.handGrid.value);
					all_players[p].handCards.Add(inst);
				}

				Settings.RegisterEvent("Created Cards For Player" + all_players[p].username, all_players[p].playerColor);
			}
		}

		public bool switchPlayer;

		private void Update()
		{
			if (switchPlayer)
			{
				switchPlayer = false;

				playerOneHolder.LoadPlayer(all_players[0], statsUI[0]);
				otherPlayersHolder.LoadPlayer(all_players[1], statsUI[1]);
			}

			bool isComplete = turns[turnIndex].Execute();

			if (isComplete)
			{
				turnIndex++;

				if (turnIndex > turns.Length - 1)
				{
					turnIndex = 0;
				}

                //The Current Player Has Changed Here
                currentPlayer = turns[turnIndex].player;
                turns[turnIndex].OnTurnStart();
				turnText.value = turns[turnIndex].player.username;
				onTurnChanged.Raise();
			}

			if (currentState != null)
				currentState.Tick(Time.deltaTime);
		}

		public void SetState(State state)
		{
			currentState = state;
		}

		public void EndCurrentPahse()
		{
			Settings.RegisterEvent(turns[turnIndex].name + " Finished", currentPlayer.playerColor);

			turns[turnIndex].EndCurrentPhase();
		}
	}
}