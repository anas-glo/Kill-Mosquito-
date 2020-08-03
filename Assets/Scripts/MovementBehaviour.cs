using UnityEngine;

[ExecuteInEditMode][RequireComponent(typeof(PolygonCollider2D))]
public class MovementBehaviour : MonoBehaviour
{
    public Vector2[] position;
    private PolygonCollider2D pathPosition;

    public static MovementBehaviour Instance;
    private void Awake()
    {
        Instance = this;
        pathPosition = GetComponent<PolygonCollider2D>();
        position = pathPosition.GetPath(0);
    }


    public Vector2 NextMovement(Vector2 current, Vector2 target)
    {
        Vector2 next = current;
        int random = Random.Range(0, 100);
        if(random >= 90)
        {
            next = target;
        }else if(random > 20)
        {
            next = position[Random.Range(0, position.Length - 1)];
        }
        return next;
    }
}
