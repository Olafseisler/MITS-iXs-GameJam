using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    [SerializeField] GameController gameController;

    public State currentState = State.START;

    public bool isOpponentAliveFlag;
    public bool isPlayerAliveFlag;
    public bool areOpponentsLeftFlag;
    private int cycleCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        this.isOpponentAliveFlag = true;
        this.isPlayerAliveFlag = true;
        this.areOpponentsLeftFlag = true;
        doTransition(State.START);
    }

    public void doTransition(State expectedCurrent)
	{
        if(!expectedCurrent.Equals(currentState))
		{
            Debug.LogWarning("Expected to be in state " + expectedCurrent.ToString() + " was in state " + currentState.ToString());
            return;
		}


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
        return isOpponentAliveFlag;
	}

    private bool isPlayerAlive()
	{
        return isPlayerAliveFlag;
	}

    private bool areOpponentsRemaining()
	{
        return areOpponentsLeftFlag;
	}

    // Transition methods (siirded)

    private void raiseToPlayerTurnFromStart() {
        Debug.Log("raiseToPlayerTurnFromStart");
        // TODO: transition anim
        this.currentState = State.PLAYER_TURN;
        gameController.playerEnterScene();
    }
    private void raiseToPlayerTurnFromNewOpponent()
    {
        Debug.Log("raiseToPlayerTurnFromNewOpponent");
        this.currentState = State.PLAYER_TURN;
        gameController.switchOpponentAnimal();

        doTransition(State.PLAYER_TURN);
    }
    private void raiseToPlayerTurnFromPlayerHurt()
    {
        Debug.Log("raiseToPlayerTurnFromOpponentDamage");
        this.currentState = State.PLAYER_TURN;
        
        doTransition(State.PLAYER_TURN);
    }
    private void raiseToDragAndSend() {
        Debug.Log("blyat");
        gameController.genererateRandomWords();
        this.currentState = State.DRAG_AND_SEND;

    }
    private void raiseToHurtOpponent() {
        Debug.Log("hurtOpponent");
        this.currentState = State.HURT_OPPONENT;
        gameController.eventDoDamageToOpponent();
        doTransition(State.HURT_OPPONENT);
    }
    private void raiseToOpponentTurn()
	{
        Debug.Log("raiseToOpponentTurn");
		this.currentState = State.OPPONENT_TURN;

        doTransition(State.OPPONENT_TURN);
	}
	private void raiseToGetResponse() {
        Debug.Log("raiseToGetResponse");

        this.currentState = State.GET_RESPONSE;
        gameController.eventGetResponseFromOpponent();
        doTransition(State.GET_RESPONSE);
    }
    private void raiseToPlayerHurt() {
        Debug.Log("raiseToPlayerHurt");

        this.currentState = State.HURT_PLAYER;
        doTransition(State.HURT_PLAYER);
    }
    private void raiseToPlayerDeadEnd() {
        Debug.Log("raiseToPlayerDeadEnd");

        this.currentState = State.END_PLAYER_DEAD;
        doTransition(State.END_PLAYER_DEAD);
    }
    private void raiseToNextOpponent() {
        Debug.Log("raiseToNextOpponent");
        gameController.eventCheckOpponentsLeft();
        this.currentState = State.NEXT_OPPONENT;
        gameController.opponentExitScene();
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
