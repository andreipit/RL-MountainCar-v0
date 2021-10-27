using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GymLibrary
{
    public class Action : MonoBehaviour
    {
        public float Speed = 3f;
        Dictionary<string, int> actions = new Dictionary<string, int>() { { "left", 0 }, { "stop", 1 }, { "right", 2 } };

        // For test only:
        public bool AddRight;
        public bool AddLeft;
        public float VelocityX;


        public void Apply(int _ActionIndex)
        {
            switch (_ActionIndex) //actions = { 'left': 0, 'stop': 1, 'right': 2}
            {
                case 0:
                    GetComponent<Rigidbody>().AddForce(-transform.right * Speed);
                    break;
                case 1:
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    break;
                case 2:
                    GetComponent<Rigidbody>().AddForce(transform.right * Speed);
                    break;
            }
        }

        // For tests only
        void Update()
        {
            if (AddRight) Apply(actions["right"]);
            if (AddLeft) Apply(actions["left"]);
            VelocityX = GetComponent<Rigidbody>().velocity.x;
        }

    }
}
