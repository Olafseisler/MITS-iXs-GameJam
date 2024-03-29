using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] InsultContainer insultContainer;
    [SerializeField] RandomWordsContainer randomWordsContainer;
    [SerializeField] Opponent opponent;
    [SerializeField] Player player;
    [SerializeField] Lifecycle lifecycle;
	[SerializeField] GameObject wordbuttonPrefab;
    [SerializeField] WordBank wordBank;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] Image fadePane;
    [SerializeField] GameObject credits;
    public int opponentsLeft = 2;

	// Start is called before the first frame update
	void Start()
    {
        insultContainer.gameController = this;
        randomWordsContainer.gameController = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
              pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        }
    }

    public void genererateRandomWords()
	{
        randomWordsContainer.GetComponent<RandomWordsContainer>().generateNewWords(opponent);
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

    public void eventCheckOpponentsLeft()
	{
        lifecycle.areOpponentsLeftFlag = opponentsLeft > 0; 
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

    public void playerTalkAnim()
	{
        player.anim.SetTrigger("Talk");
	}

    public void playerLeaveAnim()
	{
        player.anim.SetTrigger("GameEnd");
	}

    public void playerWinAnim()
	{
        player.anim.SetTrigger("Win");
	}

    public void opponentEnterScene()
	{
        if (opponentsLeft > 0)
            opponent.anim.SetTrigger("EnterScene");

    }

    public void opponentLeaveSceneEnded()
    {
        lifecycle.doTransition(State.NEXT_OPPONENT);
        if (opponentsLeft > 0){ 
       
            switchOpponentAnimal(); 
            lifecycle.isOpponentAliveFlag = true;
        }

    }

    public void sendWordToInsultContainer(Word word)
	{
        this.insultContainer.addToWordToInsult(word);
	}

    public void switchOpponentAnimal()
	{
        opponent.switchAnimal();
	}

    public void startFade()
	{
        StartCoroutine("FadeInAndOut");
        player.anim.SetTrigger("GameEnd");

    }

    IEnumerator FadeInAndOut()
    {
        fadePane.gameObject.SetActive(true);
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            fadePane.color = new Color(0, 0, 0, i);
            yield return null;
        }

        //Temp to Fade Out
        yield return new WaitForSeconds(1);

        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            fadePane.color = new Color(0, 0, 0, i);
            yield return null;
        }
        fadePane.gameObject.SetActive(false);
        StartCoroutine("ShowCredits");
        AudioManager.instance.PlaySound("drink");
    }

    IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(4.0f);
        fadePane.gameObject.SetActive(true);
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            fadePane.color = new Color(0, 0, 0, i);
            yield return null;
        }

        credits.SetActive(true);
        //Temp to Fade Out
        yield return new WaitForSeconds(1);

        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            fadePane.color = new Color(0, 0, 0, i);
            yield return null;
        }
        fadePane.gameObject.SetActive(false);
        yield return new WaitForSeconds(8.0f);
        fadePane.gameObject.SetActive(true);
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            fadePane.color = new Color(0, 0, 0, i);
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }
}
