using UnityEngine;

public class PacStudent : MonoBehaviour
{

    public bool ispowerpellet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PacStudent")
        {
            if (ispowerpellet)
            {
                GhostManager.Instance.OnEatPacdot(gameObject);
                GhostManager.Instance.OnEatpowerpellet();
                Destroy(gameObject);
            }
            else
            {
                GhostManager.Instance.OnEatPacdot(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
