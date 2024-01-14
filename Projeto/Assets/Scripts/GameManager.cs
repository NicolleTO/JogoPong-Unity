using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variável para acessar este script pelos outros
    public static GameManager gameManager;

    private Vector2 local;

    //Definidas como public para serem definidas pela Unity
    public GameObject ballPrefab;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    private int pontosP1;
    private int pontosP2;

    //Definidas como public para serem definidas pela Unity
    public Text PlacarP1;
    public Text PlacarP2;
    public Text Winner;

    //Usada para saber se P1(false) ou P2(true) fez o último ponto
    private bool check;

    //Getters and Setters
    public Vector2 Local{
        get{ return local; }
        set{ local = value; }
    }

    public int PontosP1{
        get{ return pontosP1; }
        set{ pontosP1 = value; }
    }

    public int PontosP2{
        get{ return pontosP2; }
        set{ pontosP2 = value; }
    }

    public bool Check{
        get{ return check; }
        set{ check = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null){
            gameManager = this;
        }

        //Mouse invisível durante a partida
        Cursor.visible = false;

        PontosP1 = 0;
        PlacarP1.text = " " + PontosP1; 
        PontosP2 = 0;
        PlacarP2.text = PontosP2 + " ";

        //Bolinha criada após um segundo do início
        Invoke("Spawn", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Se a tela de Game Over estiver desligada
        if(gameOverPanel.activeSelf == false){
            
            GameOver();

            //Ao apertar esc
            if(Input.GetKeyDown(KeyCode.Escape)){
            
                //Se a tela de pause estiver ligada
                if(pausePanel.activeSelf){
                    ResumeGame();

                //Se não
                } else{

                    pausePanel.SetActive(true);
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
            }
        //Se não
        }else{
            Cursor.visible = true;
        }
    }

    //Criar a bolinha em um lugar aleatório no eixo Y
    public void Spawn(){

        Local = new Vector2(0, Random.Range(3.0f, -3.0f));
        Instantiate(ballPrefab, Local, transform.rotation);
    }

    //Recomeçar partida
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    //Voltar ao jogo
    public void ResumeGame(){

        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void AtualizaPlacar(){


        if(Check == false){

            PontosP1++;
            PlacarP1.text = " " + PontosP1;
        
        } else{
            
            PontosP2++;
            PlacarP2.text = PontosP2 + " ";
        }

        //Bolinha volta 2 segundos após o ponto
        Invoke("Spawn", 2.0f);
        
    }

    //Assim que um dos jogadores marca 11 pts
    public void GameOver(){
        
        if(PontosP1 >= 11){
            
            gameOverPanel.SetActive(true);
            Winner.text = "P1 WON!";
            
        }else if(PontosP2 >= 11){
            
            gameOverPanel.SetActive(true);
            Winner.text = "P2 WON!";
        }
    }

    //Ir para a tela de menu
    public void NextScene(){
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}