using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    [SerializeField] GameController gameController;

    public State currentState = State.START;
    private int lastCycle = 0;
    private int currentCycle;
    
    // Start is called before the first frame update
    void Start()
    {
        doTransition();
    }

    public void doTransition()
	{
        /*if(!expectedCurrent.Equals(currentState))
		{
            Debug.LogWarning("Expected to be in state " + expectedCurrent.ToString() + " was in state " + currentState.ToString());
            return;
		}*/

        switch (currentState)
		{
            case State.START:
                raiseToPlayerTurnFromStart();
                break;
                
            case State.PLAYER_TURN:
                raiseToDragAndSend();
                break;
            case State.DRAG_AND_SEND:
                raiseToHurtOpponent();
                break;
            case State.HURT_OPPONENT:
                if (isOpponentAlive())
				{
                    raiseToOpponentTurn();
				}
				else
				{
                    raiseToNextOpponent();
				}
                break;
            case State.HURT_PLAYER:
                if (isPlayerAlive())
				{
                    raiseToPlayerTurnFromPlayerHurt();
				} else
				{
                    raiseToPlayerDeadEnd();
				}

                break;
            case State.END_ALL_OPPONENTS_DEAD:
                // TODO: Ending GOOD
                break;
            case State.END_PLAYER_DEAD:
                // TODO: Ending BAD
                break;
            case State.GET_RESPONSE:
                raiseToPlayerHurt();
                break;
            case State.OPPONENT_TURN:
                raiseToGetResponse();
                break;
            case State.NEXT_OPPONENT:
                if(areOpponentsRemaining())
				{
                    raiseToPlayerTurnFromNewOpponent();
				} else
				{
                    raiseOpponentsAllDead();
				}
                break;
            default:
                Debug.LogError("Unknow state");
                break;
        }
        

        // player turn to drag and send
        // drag send to hurt oppo
        // hurt oppo choice
            // opponent turn
                // get response
                // hurt player choice
                    // player oof
                    // player turn
            // next opponent
                // end opponents oof
                // get new
  
	}

    // Flags / booleans (lipu meetodid)
    private bool isOpponentAlive()
	{
        return true;
	}

    private bool isPlayerAlive()
	{
        return true;
	}


    private bool areOpponentsRemaining()
	{
        return true;
	}

    // Transition methods (siirded)

    private void raiseToPlayerTurnFromStart() {
        Debug.Log("raiseToPlayerTurnFromStart");
        // TODO: transition anim
        this.currentState = State.PLAYER_TURN;

        doTransition();
    }
    private void raiseToPlayerTurnFromNewOpponent()
    {
        Debug.Log("raiseToPlayerTurnFromNewOpponent");
        this.currentState = State.PLAYER_TURN;

        doTransition();
    }
    private void raiseToPlayerTurnFromPlayerHurt()
    {
        Debug.Log("raiseToPlayerTurnFromOpponentDamage");
        this.currentState = State.PLAYER_TURN;
        
        doTransition();
    }
    private void raiseToDragAndSend() {
        Debug.Log("blyat");
        gameController.genererateRandomWords();
        this.currentState = State.DRAG_AND_SEND;

    }
    private void raiseToHurtOpponent() {
        Debug.Log("hurtOpponent");
        this.currentState = State.HURT_OPPONENT;

    }
    private void raiseToOpponentTurn()
	{
		this.currentState = State.OPPONENT_TURN;
        doTransition();
	}
	private void raiseToGetResponse() {
        this.currentState = State.GET_RESPONSE;
        doTransition();
    }
    private void raiseToPlayerHurt() {
        this.currentState = State.HURT_PLAYER;
    }
    private void raiseToPlayerDeadEnd() {
        this.currentState = State.END_PLAYER_DEAD;
        doTransition();
    }
    private void raiseToNextOpponent() {
        this.currentState = State.NEXT_OPPONENT;
        doTransition();
    }

    private void raiseOpponentsAllDead()
	{
        Debug.Log("Reached End! Game Over!");
	}

}

public enum State {
    START,
    PLAYER_TURN,
    DRAG_AND_SEND,
    HURT_OPPONENT,
    OPPONENT_TURN,
    GET_RESPONSE,
    HURT_PLAYER,
    END_PLAYER_DEAD,
    NEXT_OPPONENT,
    END_ALL_OPPONENTS_DEAD
}
