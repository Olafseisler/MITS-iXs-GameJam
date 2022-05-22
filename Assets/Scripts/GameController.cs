using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] InsultContainer insultContainer;
    [SerializeField] RandomWordsContainer randomWordsContainer;
    [SerializeField] Opponent opponent;
    [SerializeField] Player player;
    [SerializeField] Lifecycle lifecycle;
    Word[] generatedWords;
    [SerializeField] GameObject wordbuttonPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        this.insultContainer.gameController = this;
        randomWordsContainer.gameController = this;
    }

    public void genererateRandomWords()
	{
        randomWordsContainer.GetComponent<RandomWordsContainer>().generateNewWords();
        
	}

    public void eventGetResponseFromOpponent()
	{
        opponent.getResponseFromOpponent();

    }


    public void eventDoDamageToOpponent()
	{
        player.getResponseFromPlayer(insultContainer.getWords());
        opponent.doDamage(insultContainer.getWords());
        insultContainer.removeOldInsults();
	}

    public void eventOpponentDead()
	{
        lifecycle.isOpponentAliveFlag = false;
    }

    public void allSlotsFull()
	{
        randomWordsContainer.removeOldWords();
        lifecycle.doTransition(State.DRAG_AND_SEND);
    }

    public void opponentExitScene()
	{
        opponent.startExitAnimation();
    }

    public void playerEnterScene()
	{
        player.anim.SetTrigger("EnterScene");
	}

    public void playerEnterSceneEnded()
    {
        lifecycle.doTransition(State.PLAYER_TURN);
    }

    public void opponentEnterScene()
	{
        opponent.anim.SetTrigger("EnterScene");
    }

    public void opponentLeaveSceneEnded()
    {
        lifecycle.doTransition(State.NEXT_OPPONENT);
    }

    public void sendWordToInsultContainer(Word word)
	{
        this.insultContainer.addToWordToInsult(word);
	}

    public void switchOpponentAnimal()
	{
        opponent.switchAnimal();
	}
}
