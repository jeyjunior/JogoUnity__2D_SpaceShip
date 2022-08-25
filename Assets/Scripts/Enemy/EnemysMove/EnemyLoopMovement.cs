using UnityEngine;

public class EnemyLoopMovement : MonoBehaviour
{
    //TRUE: Fim do loop
    bool endHorizontalMovement = false;

    //FALSE: horizontalLoop / TRUE: verticalLoop (Definição no startMovement)
    bool axisDirection; 

    //TRUE: Direita ou Baixo / FALSE: Esquerda ou Cima
    bool loopMoving = true; 
    float speed = 1f;

    //endPosition
    EnemyStartMovement enemyStartMovement;

    //Ciclo de repetição (Apenas inimigos)
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
            if (!axisDirection) HorizontalLoopMovement();
            else VerticalLoopMovement();
        }
        else
        {
            //EndMove é ativada
            if (!this.gameObject.CompareTag("Boss"))
            {
                this.gameObject.GetComponent<EnemyEndMovement>().enabled = true;
                //LoopMove é desativada 
                this.gameObject.GetComponent<EnemyLoopMovement>().enabled = false;
            }
        }

    }

    public void AxisDirectionMovement(bool value)
    {
        axisDirection = value;
    }
    public void MaxLoopCount(int value)
    {
        loopCount = value;
    }
    void HorizontalLoopMovement()
    {
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
    void VerticalLoopMovement(){
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

                if (transform.position.y >= enemyStartMovement.endPosition && !this.gameObject.CompareTag("Boss") && loopActual >= loopCount) endHorizontalMovement = true;
                else if (transform.position.y > 3.5f) loopMoving = true;
            }
        }

    }

}
