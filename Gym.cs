using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace GymLibrary
{
    public static class Gym
    {
        public static Envir Make()
        {
            return new Envir();
        }
    }
}

