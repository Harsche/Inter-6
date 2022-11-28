using UnityEngine;

public class Duto : MonoBehaviour
{
    [SerializeField] private GameObject dutoEscalavel;
    [SerializeField] private string animationName = "dutoAbrindo";

    //private PlayerMovement playerRef;
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start(){
        //playerRef = Player.Instance.PlayerMovement;
    }

    public void Open(){
        animator.SetTrigger("dutoAbrindo");
        dutoEscalavel.SetActive(true);
    }
}
