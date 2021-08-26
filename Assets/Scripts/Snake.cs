using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private enum Buttons { LeftButton, RightButton }
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    private float _score;
    public Transform snakeSegmentPrefab;
    public Text scoreUI;
    public Button leftButton;
    public Button rightButton;

    private void Start()
    {
        _segments = new List<Transform> { this.transform };
        _score = 0.0f;
        leftButton.onClick.AddListener(delegate { ButtonDirection(Buttons.LeftButton); });
        rightButton.onClick.AddListener(delegate { ButtonDirection(Buttons.RightButton); });
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
        _score++;
        scoreUI.text = _score.ToString("0");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Food"))
        {
            Grow();
        }
        else if (other.tag.Equals("Obstacle"))
        {
            ResetState();
        }
    }

    private void ResetState()
    {
        // clear all snakeSegments components
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        this._score = 0.0f;
        this.scoreUI.text = this._score.ToString("0");
    }

    private void ButtonDirection(Buttons buttons)
    {
        List<Vector2> directions = new List<Vector2>() { Vector2.right, Vector2.up, Vector2.left, Vector2.down };
        int index = directions.IndexOf(_direction);
        if (buttons == Buttons.LeftButton)
        {
            index++;
            index %= directions.Count;
            _direction = directions[index];
        }
        else if (buttons == Buttons.RightButton)
        {
            index--;
            if (index < 0) index += directions.Count;
            index %= directions.Count;
            _direction = directions[index];
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
