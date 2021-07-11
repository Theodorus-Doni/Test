using System;
using System.Collections.Generic;
using UnityEngine;


public sealed class InputService : MonoBehaviour
{
    private IDictionary<KeyCode, Action> _inputCallbackCollection;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _inputCallbackCollection[KeyCode.A]?.Invoke();

        if (Input.GetKeyDown(KeyCode.Space))
            _inputCallbackCollection[KeyCode.Space]?.Invoke();
    }

    public void Init(IDictionary<KeyCode, Action> inputCallbackCollection) =>
        _inputCallbackCollection = inputCallbackCollection;
}
