using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public BoxCollider2D girdArea;
    public Snake snake;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.girdArea.bounds;
        List<float> allXPos = new List<float>();
        List<float> allYPos = new List<float>();

        Vector2 snakePos = snake.transform.position;
        allXPos.Add(snakePos.x);
        allYPos.Add(snakePos.y);
        if (snake.GetSnakeSegments() != null)
        {
            foreach (Transform t in snake.GetSnakeSegments())
            {
                Vector2 pos = t.position;
                allXPos.Add(pos.x);
                allYPos.Add(pos.y);
            }
        }
        
        float x, y;
        do
        {
            x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
        } while (allXPos.Contains(x) || allYPos.Contains(y));

        this.transform.position = new Vector3(x, y, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            RandomizePosition();
        }
    }
}
