using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

			//CreateStartingCards();

			turns[0].OnTurnStart();

			turnText.value = turns[turnIndex].player.username;
			onTurnChanged.Raise();
		}

		void SetupPlayers()
		{
			ResourcesManager rm = Settings.GetResourcesManager();

			for (int i = 0; i < all_players.Length; i++)
			{
				//if (all_players[i].isHumanPlayer)
				//{
				//all_players[i].currentHolder = playerOneHolder;
				//}
				//else
				//{
				//all_players[i].currentHolder = otherPlayersHolder;
				//}

				all_players[i].Init();

				if (i == 0)
				{
					all_players[i].currentHolder = playerOneHolder;
				}
				else
				{
					all_players[i].currentHolder = otherPlayersHolder;
				}

				all_players[i].statsUI = statsUI[i];
				//statsUI[i].player.LoadPlayerOnStatsUI();

				all_players[i].currentHolder.LoadPlayer(all_players[i], all_players[i].statsUI);

				//Settings.RegisterEvent("Created Cards For Player" + all_players[i].username, all_players[i].playerColor);
			}
		}

		/*void CreateStartingCards()
		{

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
			}
		}*/

		public void PickNewCardFromDeck(PlayerHolder p)
		{
			if(p.allCards.Count == 0)
			{
				Debug.Log("Game Over");
				return;
			}

			ResourcesManager rm = Settings.GetResourcesManager();

			string cardId = p.allCards[0];
			p.allCards.RemoveAt(0);
			GameObject go = Instantiate(cardPrefab) as GameObject;
			CardVisual v = go.GetComponent<CardVisual>();
			v.LoadCard(rm.GetCardInstance(cardId));
			CardInstance inst = go.GetComponent<CardInstance>();
			inst.currentLogic = p.handLogic;
			Settings.SetParentForCard(go.transform, p.currentHolder.handGrid.value);
			p.handCards.Add(inst);
			Debug.Log(p.name + " " + p.currentHolder.name);
			//LoadPlayerOnHolder(p, p.currentHolder, p.statsUI);
		}

		public void LoadPlayerOnActive(PlayerHolder p)
		{
			PlayerHolder prevPlayer = playerOneHolder.playerHolder;
			LoadPlayerOnHolder(prevPlayer, otherPlayersHolder, statsUI[1]);
			LoadPlayerOnHolder(p, playerOneHolder, statsUI[0]);
		}

		public void LoadPlayerOnHolder(PlayerHolder p, CardHolders h, PlayerStatsUI ui)
		{
			h.LoadPlayer(p, ui);
		}

		//public bool switchPlayer;

		private void Update()
		{
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

			if (currentPlayer.health <= 0)
			{
				SceneManager.LoadScene(0);
			}

			turns[turnIndex].EndCurrentPhase();
		}
	}
}