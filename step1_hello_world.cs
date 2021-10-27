using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GymLibrary;


public class step1_hello_world : MonoBehaviour
{
    public bool Step1_reset;
    public bool Step2_observe;
    public bool Step3_action;
    public bool Step4_ai;
    bool Step4_ai_continue;

    Envir env;

    const float TIME_LIMIT = 2500;


    private void Start()
    {
        if (env == null) env = Gym.Make();
        StartCoroutine(Stepper4());
    }

    void OnDrawGizmos()
    {
        if (env == null) env = Gym.Make();

        if (Step1_reset)
        {
            Step1_reset = false;
            env.Reset();
            Debug.Log(env.observation_space);
            Debug.Log(env.action_space);
        }

        if (Step2_observe)
        {
            Step2_observe = false;
            var obs0 = env.Reset();
            Debug.Log("initial observation code: posX=" + obs0.Pos.x + " velocity=" + obs0.Velocity.magnitude);
        }

        if (Step3_action)
        {
            Step3_action = false;
            StartCoroutine(Stepper3());
        }

        if (Step4_ai)
        {
            Step4_ai = false;
            StartCoroutine(Stepper4());
        }
    }

    IEnumerator Stepper3()
    {
        env.stepStart(2);

        yield return new WaitForEndOfFrame();

        Tuple<BoxObservation, float, bool, float> newobs_Rew_Isdone_Info = env.stepEnd();

        Debug.Log("new observation code:" + newobs_Rew_Isdone_Info.Item1.Print);
        Debug.Log("reward:" + newobs_Rew_Isdone_Info.Item2);
        Debug.Log("is game over?:" + newobs_Rew_Isdone_Info.Item3);
    }


    IEnumerator Stepper4()
    {

        var s = env.Reset(); // observation 0
        Dictionary<string, int> actions = new Dictionary<string, int>() { { "left", 0 }, { "stop", 1 }, { "right", 2 } };
        var chosen_action = -1;

        //for (int t = 0; t < TIME_LIMIT; t++)
        while(true)
        {
            if (s.Velocity.x <= 0)
                chosen_action = actions["left"];
            else
                chosen_action = actions["right"];
                
            env.stepStart(chosen_action);

            //yield return new WaitForEndOfFrame();
            yield return new WaitForFixedUpdate();
            //yield return new WaitForSeconds(0.1f);

            Tuple<BoxObservation, float, bool, float> newobs_Rew_Isdone_Info = env.stepEnd();
            s = newobs_Rew_Isdone_Info.Item1;

            if (newobs_Rew_Isdone_Info.Item3)
            {
                Debug.Log("Well done!");
                GameObject.Find("Text").GetComponent<Text>().text = "Well done!";
                yield break;
            }
        }
    }

}
