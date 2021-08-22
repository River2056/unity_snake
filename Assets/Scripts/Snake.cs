using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform snakeSegmentPrefab;

    private void Start()
    {
        _segments = new List<Transform> { this.transform };
    }

    // Update is called once per frame
    private void Update()
    {
        string input = InputDirection();
        if (input != null && CheckDirection())
        {
            if (input.Equals("Up"))
            {
                _direction = Vector2.up;
            }
            else if (input.Equals("Down"))
            {
                _direction = Vector2.down;
            }
            else if (input.Equals("Left"))
            {
                _direction = Vector2.left;
            }
            else if (input.Equals("Right"))
            {
                _direction = Vector2.right;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = this.transform.position;

        for (int i = _segments.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                _segments[i].position = new Vector3(
                    Mathf.Round(currentPosition.x) + _direction.x,
                    Mathf.Round(currentPosition.y) + _direction.y,
                    0.0f
                );
            }
            else
            {
                _segments[i].position = _segments[i - 1].position;
            }
            
        }
        // this.transform.position = new Vector3(
        //     Mathf.Round(currentPosition.x) + _direction.x,
        //      Mathf.Round(currentPosition.y) + _direction.y,
        //     0.0f
        // );
    }

    private void Grow()
    {
        Transform snakeSegment = Instantiate(this.snakeSegmentPrefab);
        snakeSegment.position = _segments[_segments.Count - 1].position;
        
        _segments.Add(snakeSegment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Food"))
        {
            Grow();
        }
    }

    private bool CheckDirection()
    {
        if (
            (this._direction == Vector2.up && InputDirection() != null && InputDirection().Equals("Down"))
            ||
            (this._direction == Vector2.down && InputDirection() != null && InputDirection().Equals("Up"))
            ||
            (this._direction == Vector2.right && InputDirection() != null && InputDirection().Equals("Left"))
            ||
            (this._direction == Vector2.left && InputDirection() != null && InputDirection().Equals("Right"))
        )
        {
            return false;
        }

        return true;
    }

    private string InputDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            return "Up";
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            return "Down";
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            return "Left";
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            return "Right";
        }
        
        return null;
    }

    public List<Transform> GetSnakeSegments()
    {
        return this._segments;
    }
}
