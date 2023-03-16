using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    public float _Max_Field_Of_View = 180;
    public float _Max_Acceleration;
    public float _Max_Velocity;
    //Wander Variables
     public float _Wander_Jitter;
     public float _Wander_Radius;
     public float _Wander_Distance;
     public float _WanderPriority;
    //Cohesion Variables
     public float _Cohesion_Radius;
    public float _Cohesion_Priority;

    //Alignment Variables 
    public float _Alignment_Radius;
    public float _Aligment_Priority;

    //Separation Variables
    public float _Separation_Radius;
    public float _Separation_Priority;

    //Avoidance Variables
    public float _Avoidance_Radius;
    public float _Avoidance_Priority;

}
