using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //Variável para acessar o componente de Rigidbody deste objeto
    private Rigidbody2D rigi;
    
    //Variável para acessar o componente de Audio
    private AudioSource som;
    
    //Velocidade inicial da bolinha
    private float speed = 300.0f;

    //Getters and Setters
    public float Speed{
        get{ return speed; }
        set{ speed = value; }
    }

    // Start is called before the first frame update
    //Assim que a bolinha é gerada na cena...
    void Start()
    {
        //Inicializando as variáveis
        rigi = GetComponent<Rigidbody2D>();
        som = GetComponent<AudioSource>();

        //Definindo movimento da bolinha
        MovimentoBola();
    }

    public void MovimentoBola(){

        //Variáveis para o eixo X e Y da Unity
        float x, y;
        //Direção definida (magnitude de x e y)
        Vector2 magnitude;

        /*X - bolinha sempre começa indo para o player que levou um gol. No início da partida vai para P1
        * "Check" é uma variável do script "GameManager"
        * Ela informa se o último jogador que marcou um ponto foi P1 (false) ou P2(true)
        */
        if(GameManager.gameManager.Check == false){
            x = 1.0f;
        }else{
            x = -1.0f;
        }

        /*Random.value - Escolhe um valor aleatório entre 0 e 1
        * Se > 0.5 - bolinha vai cima
        * Se < 0.5 - bolinha vai para baixo
        *
        * Random.Range(?, ?) - Define um valor aleatório entre os números informados
        */

        /*Y - Define aleatoriamente se ela vai para cima ou para baixo do centro da tela
        * O ângulo que ela segue também é aleatório
        */
        if(Random.value > 0.5f){
            y = Random.Range(1.0f, 0.1f);
        }else{
            y = Random.Range(-1.0f, -0.1f);
        }

        //Guardando a direção definida e dando o impulso inicial
        magnitude = new Vector2(x, y);
        rigi.AddForce(magnitude * Speed);
    }

    //Função ativada sempre que uma colisão é detectada, update desnecessário
    public void OnCollisionEnter2D(Collision2D collision2D){
        
        //Caso este objeto(bola) colida com o teto ou chão(tag "PdTocar"), reproduz som
        if(collision2D.gameObject.tag == "PdTocar"){
            som.Play();
        }

        /*Sempre que colide com alguma coisa, muda um pouquinho de direção 
        * O quanto muda é definido aleatoriamente, mas são sempre valores baixos
        * Impede que ela fique presa em um loop com um mesmo movimento, indo reta de um lado pro outro
        */
        rigi.velocity += new Vector2(Random.Range(0.1f, -0.1f), Random.Range(0.1f, -0.1f));
    }
}