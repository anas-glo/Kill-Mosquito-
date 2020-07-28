using System;
using UnityEngine;

public class Human : MonoBehaviour
{
    public Action onHumanTap;
    public Action onMosquitoEnter;
    public Vector2 Position { get; private set; }

    public static Human Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onMosquitoEnter();
    }

    private void OnMouseDown()
    {
        onHumanTap();
    }
}
