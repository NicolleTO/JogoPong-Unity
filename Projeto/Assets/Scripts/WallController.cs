using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{   
    //Variável para acessar o componente de Audio
    private AudioSource som;

    // Start is called before the first frame update
    public void Start(){

        som = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision2D){

        if(collision2D.gameObject.tag == "Bola"){

            //Se nenhum dos jogadores alcançou 11 pts ainda
            if((GameManager.gameManager.PontosP1 < 11) && (GameManager.gameManager.PontosP2 < 11)){
                
                //Ponto para P1
                if (this.gameObject.tag == "ParedeR"){

                    GameManager.gameManager.Check = false;
                    GameManager.gameManager.AtualizaPlacar();
                
                //Ponto para P2
                }else {
                    GameManager.gameManager.Check = true;
                    GameManager.gameManager.AtualizaPlacar();
                }

                som.Play();
                Destroy(collision2D.gameObject);

            }
        }
    }

    //Update desnecessário
}
