﻿using UnityEngine;
using UnityEngine.UI;

public class WorkerEntry : MonoBehaviour
{
    public enum State
    {
        Occupied = 0,
        Locked = 1,
        Free = 2
    }

    public Sprite Occupied, Locked, Free;
    public Image BackgroundSprite;
    public GameObject Worker;

    public void Init(State state)
    {
        ChangeState(state);
    }

    public void ChangeState(State target)
    {
        switch (target)
        {
            case State.Occupied:
                BackgroundSprite.sprite = Occupied;
                break;
            case State.Locked:
                BackgroundSprite.sprite = Locked;
                break;
            case State.Free:
                BackgroundSprite.sprite = Free;
                break;
        }

        SetWorker(target == State.Occupied);
    }

    public void SetWorker(bool active)
    {
        Worker.SetActive(active);
    }
}