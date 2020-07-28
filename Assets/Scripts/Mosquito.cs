using System;
using System.Collections;
using UnityEngine;


public class Mosquito : MonoBehaviour
{
    [SerializeField]
    private GameObject deadObject;

    private Vector2 _target;
    private int idx;
    float speed;

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

    public void Init(Vector2 target, float speed, Vector3 position, int index)
    {
        _target = target;
        this.speed = speed;
        transform.position = position;

        gameObject.GetComponent<Collider2D>().enabled = true;
        idx = index;
        isDeath = false;

        GetComponent<SpriteRenderer>().sortingOrder = currentSorting;
        deadObject.GetComponent<SpriteRenderer>().sortingOrder = currentSorting + 1;
    }

    public void Movement()
    {
        if (isDeath)
            return;
        Vector2 move = Vector2.Lerp(transform.position, _target, 0.1f * Time.deltaTime * speed);
        transform.position = move;
    }

    public void Dead()
    {
        isDeath = true;
        deadObject.active = true;

        gameObject.GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = currentSorting - 1;
        deadObject.GetComponent<SpriteRenderer>().sortingOrder = currentSorting;

        StartCoroutine(BackToPull());
    }

    IEnumerator BackToPull()
    {
        yield return new WaitForSeconds(2f);
        deadObject.active = false;
        onDead(idx);
    }

    private void OnMouseDown()
    {
        Dead();
    }

    private void OnDisable()
    {
        deadObject.active = false;
    }

    //private void OnMouseDrag()
    //{
    //    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    transform.position = pos;
    //}
}