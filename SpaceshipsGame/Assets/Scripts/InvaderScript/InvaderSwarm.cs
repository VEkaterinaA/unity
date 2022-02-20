using UnityEngine;

public class InvaderSwarm : MonoBehaviour
{
    [System.Serializable]
    private struct InvaderType
    {
        public string name;
        public Sprite[] sprites;
        public int points;
        public int rowCount;
    }

    [Header("Spawning")]
    [SerializeField]
    private InvaderType[] invaderTypes;

    private int columnCount = 6;

    private float ySpacing=0.5f;// расстояние между каждым захватчиком в рое по оси Y

    private float xSpacing=0.5f;

    [SerializeField]
    private Transform spawnStartPoint;//левая граница
    [SerializeField]
    private Transform spawnEndPoint;//правая граница

    private float minX;//Сохраняет минимальное значение позиции X для роя


    private float speedFactor = 0.5f;//скорость, с которой захватчики перемещаются по оси X

    private Transform[,] invaders;//хранит преобразования всех созданных игровых объектов захватчиков
    private int rowCount;
    private bool isMovingRight = true;//представляет направление движения
    private float maxX;
    private float currentX;
    private float xIncrement;//это значение за кадр, которое перемещает захватчиков по оси X.
    
    private void Start()
    {
        minX = spawnStartPoint.position.x;

        GameObject swarm = new GameObject { name = "Swarm" };
        Vector2 currentPos = spawnStartPoint.position;

        foreach (var invaderType in invaderTypes)
        {
            rowCount += invaderType.rowCount;
        }//общее количество строк

        maxX = spawnEndPoint.position.x - xSpacing * (columnCount - 1);
         currentX = minX;
        invaders = new Transform[rowCount, columnCount];

        int rowIndex = 0;
        foreach (var invaderType in invaderTypes)
        {
            var invaderName = invaderType.name.Trim();
            for (int i = 0, len = invaderType.rowCount; i < len; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var invader = new GameObject() { name = invaderName };
                    invader.AddComponent<SimpleAnimator>().sprites = invaderType.sprites;
                    invader.AddComponent<BoxCollider2D>().size = new Vector2(0.4f,0.5f);
                    invader.transform.position = currentPos;
                    invader.transform.SetParent(swarm.transform);
                    invader.layer = 9;
                    invaders[rowIndex, j] = invader.transform;

                    currentPos.x += xSpacing;
                }

                currentPos.x = minX;
                currentPos.y -= ySpacing;

                rowIndex++;
            }
        }
    }
    //вычисляем xIncrement и обновляем currentX каждый кадр в зависимости от направления
    
    private void Update()
    {
        xIncrement = speedFactor * Time.deltaTime;
        if (isMovingRight)
        {
            currentX += xIncrement;
            if (currentX < maxX)
            {
                MoveInvaders(xIncrement, 0);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            currentX -= xIncrement;
            if (currentX > minX)
            {
                MoveInvaders(-xIncrement, 0);
            }
            else
            {
                ChangeDirection();
            }

        }
        if(invaders[rowCount-1,0].position.y<-3)
        {
            GameManager.Instance.TriggerGameOver();
            
        }
    }
    //перемещает каждое преобразование invaders на одно и то же значение по осям X и Y соответственно
    private void MoveInvaders(float x, float y)
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                invaders[i, j].Translate(x, y, 0);
                
            }
        }
    }

    //переключает isMovingRight и перемещает рой вниз на ySpacing количество
    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
        MoveInvaders(0, -ySpacing);
    }
}
