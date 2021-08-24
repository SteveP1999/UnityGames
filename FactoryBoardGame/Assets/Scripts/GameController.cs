using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { START, PLAYERTURN, ENDGAME }

public class GameControlelr : MonoBehaviour
{
    private Player activePlayer;
    private PlayerManager playerManager;
    public GameState gameState;
    int playersChoosen = 0;

    void Start()
    {
        gameState = GameState.START;
        StartCoroutine(characterChoose((Player)playerManager.getPlayers()[playersChoosen]));
    }

    IEnumerator characterChoose(Player player)
    {
        yield return new WaitForSeconds(2);
    }
}
