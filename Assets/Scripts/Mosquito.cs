using System;
using System.Collections;
using UnityEngine;


public class Mosquito : MonoBehaviour
{
    [SerializeField]
    private GameObject deadObject;

    private Vector2 PeoplePosition;
    private int idx;
    float speed;
    float timeNextMove;
    Vector2 target;
    const float TimeMove = 0.2f;

    private int currentSorting;
    bool isDeath = true;
    public Action<int> onDead;

    private void Awake()
    {
        currentSorting = GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Update()
    {
        Movement();
    }

    public void Init(Vector2 target, float speed, Vector3 position, int index, bool fromRight)
    {
        if (fromRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.eulerAngles = Vector3.forward * 45;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.eulerAngles = Vector3.forward * -45;
        }
        PeoplePosition = target;
        this.speed = speed;
        transform.position = position;

        gameObject.GetComponent<Collider2D>().enabled = true;
        idx = index;
        isDeath = false;

        GetComponent<SpriteRenderer>().sortingOrder = currentSorting;
        deadObject.GetComponent<SpriteRenderer>().sortingOrder = currentSorting + 1;
        timeNextMove = TimeMove;
    }

    public void Movement()
    {
        if (isDeath)
            return;
        if(timeNextMove < TimeMove)
        {
            Move();
        }
        else
        {
            timeNextMove = 0;
            target = MovementBehaviour.Instance.NextMovement(transform.position, PeoplePosition);
        }
    }

    public void Move()
    {
        Vector2 move = Vector2.Lerp(transform.position, target, 0.1f * Time.deltaTime * speed);
        transform.position = move;
        timeNextMove += 0.1f * Time.deltaTime * speed;
    }


    public void Dead()
    {
        isDeath = true;
        deadObject.SetActive(true);

        gameObject.GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = currentSorting - 1;
        deadObject.GetComponent<SpriteRenderer>().sortingOrder = currentSorting;

        StartCoroutine(BackToPull());
    }

    IEnumerator BackToPull()
    {
        yield return new WaitForSeconds(2f);
        deadObject.SetActive(false);
        onDead(idx);
    }

    private void OnMouseDown()
    {
        Dead();
    }

    private void OnDisable()
    {
        deadObject.SetActive(false);
    }

    //private void OnMouseDrag()
    //{
    //    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    transform.position = pos;
    //}
}