using UnityEngine;

public class EnemyStartMovement : MonoBehaviour
{
    //Fim do movimento inicial
    //TRUE = finaliza o movimento inicial
    bool endStartMovement = false;

    [Space(10)]
    [Header("Movimento inicial Horizontal ou Vertical")]
    [Tooltip("false = horizontalMove / true = verticalMove")]
    public bool axisDirection;

    [Space(5)]
    [Header("Objeto esta na Direita ou Esquerda")]
    [Tooltip("FALSE = direita / TRUE = esquerda")]
    public bool horizontalDirection;
    //Obs.: Se axisDir for TRUE o valor dessa variavel é irrelevante para o movimento do objeto

    [Space(5)]
    [Header("Posição final")]
    [Tooltip("valor que encerra o movimento inicial")]
    public float endPosition;
    float axisSpeed = 1;


    [Space(5)]
    [Header("Habilitar movimento em Loop")]
    [Tooltip("TRUE = Ativa a class LoopMove")]
    public bool loopMove;

    

    private void Start()
    {
        GetComponent<EnemyLoopMovement>().enabled = false;
        GetComponent<EnemyEndMovement>().enabled = false;
    }

    void Update()
    {

        if (!endStartMovement)
        {
            //FALSE = executa movimento horizontal / TRUE = executa movimento vertical inicial
            if (!axisDirection) HorizontalMovement(horizontalDirection);
            else VerticalMovement();
        }
        else
        {
            if (loopMove) this.gameObject.GetComponent<EnemyLoopMovement>().enabled = true;
            else this.gameObject.GetComponent<EnemyEndMovement>().enabled = true;

            //Desativa class start
            GetComponent<EnemyStartMovement>().enabled = false;
        }

    }

    void HorizontalMovement(bool value)
    {
        //horizontalDirection define se o objeto vai para esquerda ou direita
        if (value)
        {
            transform.Translate(Vector2.left * axisSpeed * Time.deltaTime);
            if (transform.position.x < endPosition) endStartMovement = true;
        }
        else
        {
            transform.Translate(Vector2.right * axisSpeed * Time.deltaTime);
            if (transform.position.x > endPosition) endStartMovement = true;
        }
    }

    void VerticalMovement()
    {
        transform.Translate(Vector2.down * axisSpeed * Time.deltaTime);
        if (transform.position.y < endPosition) endStartMovement = true;
    }


}
