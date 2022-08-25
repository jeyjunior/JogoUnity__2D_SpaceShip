using UnityEngine;

enum InitialMovement { 
    moveToRight, 
    moveToLeft, 
    moveToDown};
enum BehaviorControl
{
    noLoopNoShooting,
    noLoopWithShooting,
    loopHorizontalNoShooting,
    loopHorizontalWithShooting,
    loopVerticalNoShooting,
    loopVerticalWithShooting,
}

public class EnemyStartMovement : MonoBehaviour
{
    //Fim do movimento inicial
    //TRUE = finaliza o movimento inicial
    bool endStartMovement = false;

    [Space(5)]
    [Header("Posição inicial do obj")]
    [SerializeField]
    InitialMovement initialMovement = new InitialMovement();

    [Space(5)]
    [Header("Comportamentos Obj")]
    [SerializeField]
    BehaviorControl behaviorControl= new BehaviorControl();

    [Space(5)]
    [Header("Max Loop Count")]
    public int maxLoopCount;

    [Space(5)]
    [Header("Posição final")]
    [Tooltip("valor que encerra o movimento inicial")]
    public float endPosition;
    float axisSpeed = 1;


    /*
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
    [Header("Habilitar movimento em Loop")]
    [Tooltip("TRUE = Ativa a class LoopMove")]
    public bool loopMove;

    [Space(5)]
    [Header("Inimigo Spawn Bullets?")]
    [Tooltip("TRUE = Ativa a class SpawnBullet")]
    public bool spawnBullet;
    */

    private void Start()
    {
        //Desabilita loopHorizontal e bullet
        EnableComponents(false, false); 
    }

    void Update()
    {
        #region Old
        /*
        if (!endStartMovement)
        {
            //FALSE = executa movimento horizontal / TRUE = executa movimento vertical inicial
            if (!axisDirection) HorizontalMovement(horizontalDirection);
            else VerticalMovement();
        }
        else
        {
            if (loopMove) EnableComponents(true, false, spawnBullet);
            else EnableComponents(false, true, spawnBullet);

                //Desativa class start
                GetComponent<EnemyStartMovement>().enabled = false;
        }
        */

        //Se o movimento inicial não foi concluido

        #endregion

        if (!endStartMovement)
        {
            switch (initialMovement)
            {
                case InitialMovement.moveToLeft:
                    HorizontalMovement(true); 
                    break;
                case InitialMovement.moveToRight:
                    HorizontalMovement(false); 
                    break;

                case InitialMovement.moveToDown:
                    VerticalMovement();
                    break;
            }
        }
        else
        {
            #region Old
            /*
            if (loopMove) EnableComponents(true, false, spawnBullet);
            else EnableComponents(false, true, spawnBullet);

            //Desativa class start
            GetComponent<EnemyStartMovement>().enabled = false;
            */
            #endregion

            switch (behaviorControl)
                {
                    case BehaviorControl.noLoopNoShooting:
                        EnableComponents(false, false);
                        break;
                    case BehaviorControl.noLoopWithShooting:
                        EnableComponents(false, true);
                        break;
                    case BehaviorControl.loopHorizontalNoShooting:
                        EnableComponents(true, false);
                        AxisDirAndMaxLoopCount(false);
                    break;
                    case BehaviorControl.loopHorizontalWithShooting:
                        EnableComponents(true, true);
                        AxisDirAndMaxLoopCount(false);
                        break;
                    case BehaviorControl.loopVerticalNoShooting:
                        EnableComponents(true, false);
                        AxisDirAndMaxLoopCount(true);
                    break;
                    case BehaviorControl.loopVerticalWithShooting:
                        EnableComponents(true, true);
                        AxisDirAndMaxLoopCount(true);
                    break;
                }

            //Desativa EnemyStartMovement
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

    void EnableComponents(bool loopM, bool spawnB)
    {
        GetComponent<EnemyLoopMovement>().enabled = loopM;
        GetComponent<EnemySpawnBullet>().enabled = spawnB;

        //if (this.gameObject.CompareTag("Boss")) GetComponent<EnemyEndMovement>().enabled = false;
        //else GetComponent<EnemyEndMovement>().enabled = true;
    }

    void AxisDirAndMaxLoopCount(bool dir) {
        //FALSE: horizontal - TRUE: vertical
        GetComponent<EnemyLoopMovement>().AxisDirectionMovement(dir);
        GetComponent<EnemyLoopMovement>().MaxLoopCount(maxLoopCount);

    }
}
