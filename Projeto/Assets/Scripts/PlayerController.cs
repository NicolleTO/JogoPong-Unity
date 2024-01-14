using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variável para acessar o componente de Rigidbody deste objeto
    private Rigidbody2D rigi;

    //Variável para acessar o componente de BoxCollider2D
    private BoxCollider2D colide;

    //Variável para acessar o componente de Audio
    private AudioSource som;

    //Velocidade dos jogadores e direção do movimento
    private float speed = 10f;
    private float direction;

    //Getters and Setters
    public float Speed{
        get{ return speed; }
        set{ speed = value; }
    }

    public float Direction{
        get{ return direction; }
        set{ direction = value; }
    }

    // Start is called before the first frame update
    public void Start(){

        rigi = GetComponent<Rigidbody2D>();
        colide = GetComponent<BoxCollider2D>();
        som = GetComponent<AudioSource>();

    }

    /*Função chamada com um tempo definido (0.02) 
    * Comando de controle de movimento (fisica do objeto) colocado aqui para evitar errinhos 
    */
    public void FixedUpdate(){
        rigi.velocity = new Vector2(rigi.velocity.x, Direction * Speed);
    }

    // Update is called once per frame
    public void Update()
    {
        /*Obtendo entradas dos jogadores 1 e/ou 2
        * N esquecer - Edit -> Project Settings -> Input Manager
        * GetAxisRaw() usado no lugar de GetAxis() para um movimento menos fluido
        */
        if(this.gameObject.tag == "Play1"){
            Direction = Input.GetAxisRaw("Controle1");
        }else{
            Direction = Input.GetAxisRaw("Controle2");
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision2D){
        
        //Sempre que a bola colidir com esse objeto, reproduz som
        if(collision2D.gameObject.tag == "Bola"){
            som.Play();
        }
    }
}
