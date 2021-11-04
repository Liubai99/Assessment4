using UnityEngine;

public class PacStudentMove : MonoBehaviour
{
    private static PacStudentMove _instance;
    public static PacStudentMove Instance
    {
        get
        {
            return _instance;
        }
    }

    public float PacStudentspeed = 0.35f;

    public Vector2 PacStudentdest = Vector2.zero;

    private void Start()
    {
        PacStudentdest = transform.position;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void FixedUpdate()
    {
        Vector2 PacStudenttemp = Vector2.MoveTowards(transform.position, PacStudentdest, PacStudentspeed);

        GetComponent<Rigidbody2D>().MovePosition(PacStudenttemp);
  
        if ((Vector2)transform.position == PacStudentdest)
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && PacStudentValid(Vector2.left))
            {
                PacStudentdest = (Vector2)transform.position + Vector2.left;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && PacStudentValid(Vector2.right))
            {
                PacStudentdest = (Vector2)transform.position + Vector2.right;
            }
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && PacStudentValid(Vector2.up))
            {
                PacStudentdest = (Vector2)transform.position + Vector2.up;
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && PacStudentValid(Vector2.down))
            {
                PacStudentdest = (Vector2)transform.position + Vector2.down;
            }  
     
            Vector2 dir = PacStudentdest - (Vector2)transform.position;

            GetComponent<Animator>().SetFloat("DirY", dir.y);
            GetComponent<Animator>().SetFloat("DirX", dir.x);    
        }
    }

    private bool PacStudentValid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    public void BackPosition()
    {
        PacStudentdest = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Sound")
        {
            BgAudiosControler.Instance.OpneBg031();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Sound")
        {
            BgAudiosControler.Instance.OpneBg032();
        }
    }


}
