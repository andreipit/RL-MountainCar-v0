using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GymLibrary
{
    public class Envir
    {
        public DiscreteAction action_space;
        public BoxObservation observation_space;

        Transform m_Finish;
        Transform m_Agent;
        Action m_AgentAction;
        Rigidbody m_AgentRB;

        
        #region Public methods

        public Envir()
        {
            m_Finish = GameObject.Find("Finish").transform;
            m_Agent = GameObject.Find("Agent").transform;
            m_AgentAction = m_Agent.GetComponent<Action>();
            m_AgentRB = m_Agent.GetComponent<Rigidbody>();
        }

        /// <summary> reset environment to initial state, return first observation </summary>
        public BoxObservation Reset()
        {
            m_Agent.position = Vector3.zero;
            m_Agent.localEulerAngles = Vector3.zero;
            return GetObserv();
        }

        /// <summary> show current environment state(a more colorful version :) ) </summary>
        public void render()
        {

        }

        /// <summary> 
        /// commit action a and return (new observation, reward, is done, info) 
        /// new observation - an observation right after commiting the action a
        /// reward - a number representing your reward for commiting action a
        /// is_done - True if the MDP has just finished, False if still in progress
        /// info - some auxilary stuff about what just happened.Ignore it for now.
        /// </summary>
        /// <param name="a"> Action index</param>
        /// <returns></returns>
        public void stepStart(int a)
        {
            m_AgentAction.Apply(a);
        }

        public Tuple<BoxObservation, float, bool, float> stepEnd()
        {
            BoxObservation observation = GetObserv();
            float reward = GetReward();
            bool is_done = (reward > 10);
            float info = 0;
            return Tuple.Create(observation, reward, is_done, info);
        }

        #endregion


        #region Private methods

        BoxObservation GetObserv()
        {
            return new BoxObservation(m_Agent.position, m_AgentRB.velocity);
        }

        float GetReward()
        {
            return 100 / (m_Agent.position - m_Finish.position).magnitude;
        }

        #endregion
    }

    public class BoxObservation
    {
        public Vector3 Pos;
        public Vector3 Velocity;
        public BoxObservation(Vector3 _Pos, Vector3 _Velocity)
        {
            Pos = _Pos;
            Velocity = _Velocity;
        }
        public string Print => "Pos=" + Pos + " Vel=" + Velocity;
    }
    public struct DiscreteAction
    {

    }
}
