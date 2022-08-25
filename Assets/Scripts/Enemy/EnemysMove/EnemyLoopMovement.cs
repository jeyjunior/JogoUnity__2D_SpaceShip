using UnityEngine;

public class EnemyLoopMovement : MonoBehaviour
{
    //"Fim do loop
    //TRUE = Fim do loop
    bool endHorizontalMovement = false;

    [Header("Loop sera Horizontal ou Vertical")]
    [Tooltip("FALSE = Horizontal / TRUE = Vertical")]
    public bool axisDirection;
    //Obs: Indepenente do movimento do spawn, o objeto pode fazer loop em qualquer direção
    

    //Controle da direção do objeto:
    //TRUE = Direita ou Baixo / FALSE = Esquerda ou Cima
    bool loopMoving = true; 
    float speed = 1f;

    //Coletar endPosition para encerrar o loop na posição em que StartMove encerrou
    EnemyStartMovement enemyStartMovement;

    //Controlar ciclo de repetições dos objetos (Apenas inimigos)
    int loopCount = 3;
    int loopActual = 0;


    private void Start()
    { 
        if (GetComponent<EnemyLoopMovement>().enabled) enemyStartMovement = GetComponent<EnemyStartMovement>();
    }

    void Update()
    {
        if (!endHorizontalMovement)
        {
            if (axisDirection) HorizontalMovement();
            else VerticalMovement();
        }
        else
        {
            //EndMove é ativada
            this.gameObject.GetComponent<EnemyEndMovement>().enabled = true;
            //LoopMove é desativada 
            this.gameObject.GetComponent<EnemyLoopMovement>().enabled = false; 
        }
    }

    void HorizontalMovement()
    {
        //transform.localPosition = new Vector2(SineMoving(), transform.position.y);
        if (loopMoving)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            //Essa verificação é para fazer o objeto encerrar o loop exatamente onde ele iniciou
            if (transform.position.x >= enemyStartMovement.endPosition && !this.gameObject.CompareTag("Boss") && loopActual >= loopCount)
            {
                endHorizontalMovement = true;
            }
            else if(transform.position.x >= 1.5f)
            {
                loopActual++;
                loopMoving = false;
            }
            
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if(transform.position.x <= enemyStartMovement.endPosition && !this.gameObject.CompareTag("Boss") && loopActual >= loopCount) endHorizontalMovement = true;
            else if (transform.position.x < -1.5f) loopMoving = true;
        }
    }
    void VerticalMovement(){
        {
            if (loopMoving)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);

                if (transform.position.y <= enemyStartMovement.endPosition && !this.gameObject.CompareTag("Boss") && loopActual >= loopCount)
                {
                    endHorizontalMovement = true;
                }
                else if (transform.position.y <= 0)
                {
                    loopActual++;
                    loopMoving = false;
                }

            }
            else
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);

                if (transform.position.x >= enemyStartMovement.endPosition && !this.gameObject.CompareTag("Boss") && loopActual >= loopCount) endHorizontalMovement = true;
                else if (transform.position.x > 3.5f) loopMoving = true;
            }
        }

    }
}
