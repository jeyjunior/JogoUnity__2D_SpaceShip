using UnityEngine;

public class EnemyEndMovement : MonoBehaviour
{

    void Update()
    {
        if (!this.gameObject.CompareTag("Boss"))
        {
            transform.Translate(Vector2.down * 1 * Time.deltaTime);
        }
    }

}
