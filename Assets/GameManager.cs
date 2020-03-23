using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenQuestion = 2f;

    public Text timerText;
    private float startTime;
    private float totalTime;

    public float timer;
    public int rounds;

    public GameObject questionsCanvas;
    public GameObject finalScoreCanvas;

    private SettingsMenu settingsMenu;

    private Score Score;

    void Start(){

        if (unansweredQuestions == null || unansweredQuestions.Count == 0){
            unansweredQuestions = questions.ToList<Question>();
        }
        
        if (SettingsMenu.numberOfRounds == 0){
            rounds = 5;
        }
        else{
            rounds = SettingsMenu.numberOfRounds;
        }
        Debug.Log(rounds);

        if (SettingsMenu.timerTime == 0){
            timer = 10.0f;
        }
        else{
            timer = SettingsMenu.timerTime;
        }

        Debug.Log(timer);

        startTime = Time.time + timer;

        SetCurrentQuestion();
    }

    void Update(){
        totalTime = startTime - Time.time;

        if (totalTime >= 0){
            string seconds = (totalTime % 60).ToString("f2");

            timerText.text = seconds;
        }
        else {
            timerText.text = "0.00";
            if (currentQuestion.isTrue){
                animator.SetTrigger("True");
                StartCoroutine(TransitionToNextQuestion());
            }
            else{
                animator.SetTrigger("False");
                StartCoroutine(TransitionToNextQuestion());
            }
        }
    }

    void SetCurrentQuestion(){
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        questionText.text = currentQuestion.question; 
        

        if (currentQuestion.isTrue){
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        }
        else {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion(){
        Score.round += 1;
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestion);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue(){
        timerText.text = totalTime;
        animator.SetTrigger("True");
        if (currentQuestion.isTrue){
            Score.score += 1;
        
            Debug.Log("Correct!");
        }
        else {
            
            
            Debug.Log("Wrong");
        }

        if (Score.round < rounds){
            StartCoroutine(TransitionToNextQuestion());
        }
        else {
            System.Threading.Thread.Sleep(1000);
            totalTime = 0;
            finalScoreCanvas.SetActive(true);
            questionsCanvas.SetActive(false);
            Score.round = 1;
            Score.score = 0;
        }
    
    }

    public void UserSelectFalse(){
        timerText.text = totalTime;
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue){
            Score.score += 1;
            
            
            Debug.Log("Correct!");

        }
        else {
            Debug.Log("Wrong");
        }

        if (Score.round < rounds){
            StartCoroutine(TransitionToNextQuestion());
        }
        else {
            System.Threading.Thread.Sleep(1000);
            totalTime = 0;
            finalScoreCanvas.SetActive(true);
            questionsCanvas.SetActive(false);
            Score.round = 1;
            Score.score = 0;
        }

        
    } 

    public void MainMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
