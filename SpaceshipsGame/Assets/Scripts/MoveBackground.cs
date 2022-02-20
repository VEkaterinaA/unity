using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private const float SpeedMoveBackground = 1f;
    private Transform TransformBackground;
    private float SizeBackground;
    private float PositionBackground;
    void Start()
    {
        TransformBackground = GetComponent<Transform>();
        SizeBackground = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    void Update()
    {
        Move();
    }
    public void Move()
        {
        PositionBackground -= SpeedMoveBackground * Time.deltaTime;
        PositionBackground = Mathf.Repeat(PositionBackground, SizeBackground);
        TransformBackground.position = new Vector3(0, PositionBackground, 0);

    }
}
