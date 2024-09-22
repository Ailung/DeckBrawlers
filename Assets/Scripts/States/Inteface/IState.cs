using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(float deltaTime, GameObject gameObject);
    void Exit(float deltaTime, GameObject gameObject);
    void UpdateState(float deltaTime, GameObject gameObject);
}
