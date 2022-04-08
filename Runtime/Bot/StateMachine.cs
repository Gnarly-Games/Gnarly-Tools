using System;
using System.Collections.Generic;
using System.Linq;
using Bot;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Bot
{
    public class StateMachine
    {
        private List<IState> states;
        public IState currentState;
        private Alarm _alarm;
        private bool _isRun;

        private static List<Type> _stateOrders = new List<Type>()
        {
        };

        public StateMachine()
        {
            states = new List<IState>();
        }

        public void Start()
        {
            _isRun = true;
            _alarm = new Alarm();
            _alarm.Start(Random.Range(0.3f, 0.5f));
            currentState = null;
        }

        public void Stop()
        {
            _isRun = false;
            if (currentState != null)
            {
                currentState.Stop();
                currentState = null;
            }
        }

        public void AddState(IState state)
        {
            var index = _stateOrders.IndexOf(state.GetType());
            if (index == -1)
            {
                Debug.LogError($"Missing bot state order {state.GetType()}");
                return;
            }

            states.Add(state);
            states = states.OrderBy(x => _stateOrders.IndexOf(x.GetType())).ToList();
        }


        public void Update()
        {
            if (!_isRun) return;
            if (!_alarm.Check()) return;

            _alarm.Start(Random.Range(0.3f, 0.5f));
            if (currentState == null)
            {
                NextState();
            }
            else if (!currentState.Lock)
            {
                NextState();
            }

            currentState?.Update();
        }

        private void NextState()
        {
            IState tempState = null;

            foreach (var state in states)
            {
                if (state.Check())
                {
                    tempState = state;
                    break;
                }
            }

            if (tempState == null)
            {
                currentState?.Stop();
                currentState = null;
                return;
            }

            if (currentState != tempState)
            {
                currentState = tempState;
                currentState.Start();
            }
            else
            {
                currentState.Refresh();
            }
        }
    }
}