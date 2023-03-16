using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AI_Member :  MonoBehaviour
{
    public bool isChasing = false;
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public AI_Level_Manager _LevelController;
    public AI_Controller _Controller;

    Vector3 _Wander_Target;

    public float timer;

    private void Awake()
    {
        _LevelController = FindObjectOfType<AI_Level_Manager>();
        _Controller = FindObjectOfType<AI_Controller>();
    }
    void Start()
    {
  
        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }

    void Update()
    {
       

      
        {
            //acceleration = Combine();
            //acceleration = Vector3.ClampMagnitude(acceleration, _Controller._Max_Acceleration);
            //velocity = velocity + acceleration * Time.deltaTime;
            //velocity = Vector3.ClampMagnitude(velocity, _Controller._Max_Velocity);
            //position = position + velocity * Time.deltaTime;
            //WrapAround(ref position, -_LevelController.bounds, _LevelController.bounds);
            //transform.position = position;
            
        }
       
    }
    
    //protected Vector3 Wander()
    //{
    //    float jitter = _Controller._Wander_Jitter * Time.deltaTime;//Getting jitter variable

    //    _Wander_Target += new Vector3(0, RandomBinomial() * jitter, 0);//Creating small random Vector
    //    _Wander_Target = _Wander_Target.normalized;
    //    _Wander_Target *= _Controller._Wander_Radius; // Increasing the lenght of the vector

    //    Vector3 _Target_In_LocalSpace = _Wander_Target + new Vector3(_Controller._Wander_Distance, _Controller._Wander_Distance, 0);
    //    Vector3 _Target_In_WorldSpace = transform.TransformPoint(_Target_In_LocalSpace);
    //    _Target_In_WorldSpace -= this.position; //Stearing before we return it.
    //    return _Target_In_WorldSpace.normalized;
    //}
    //Vector3 Cohesion()
    //{
    //    Vector3 cohesionVector = new Vector3();
    //    int countMembers = 0;
    //    var neighbours = _LevelController.GetEnemyNeightbourhs(this, _Controller._Cohesion_Radius);

    //    if (neighbours.Count == 0)
    //    {
    //        return cohesionVector;
    //    }

    //    foreach (var member in neighbours)
    //    {
    //        if(isOnFOV(member.position))
    //        {
    //            //Update cohesion vector
    //            cohesionVector += member.position;
    //            countMembers++;
    //        }
    //    }
    //    if (countMembers == 0)
    //    {
    //        return cohesionVector;
    //    }

    //    cohesionVector /= countMembers;
    //    cohesionVector = cohesionVector - this.position;
    //    cohesionVector = Vector3.Normalize(cohesionVector);
    //    return cohesionVector;


    //}

    //Vector3 Alignment()
    //{
    //    Vector3 alignVector = new Vector3();
    //    var members = _LevelController.GetEnemyNeightbourhs(this, _Controller._Alignment_Radius);

    //    if(members.Count == 0)
    //    {
    //        return alignVector;
    //    }
    //    foreach( var member in members)
    //    {
    //        if(isOnFOV(member.position))
    //        {
    //            alignVector += member.velocity;
    //        }
    //    }

    //    return alignVector.normalized;

    //}

    //Vector3 Separation()
    //{
    //    Vector3 separateVector = new Vector3();
    //    var members = _LevelController.GetEnemyNeightbourhs(this, _Controller._Separation_Radius);
    //    if (members.Count == 0)
    //    {
    //        return separateVector;
    //    }
    //    foreach (var member in members)
    //    {
    //        if (isOnFOV(member.position))
    //        {
    //            Vector3 MovingTowards = this.position - member.position;
    //            //Check the magnitude
    //            if(MovingTowards.magnitude > 0)
    //            {
    //                separateVector += MovingTowards.normalized / MovingTowards.magnitude;
    //            }
    //        }
    //    }
    //    return separateVector.normalized;
    //}

    //Vector3 Avoidance()
    //{
    //    Vector3 avoidanceVector = new Vector3();
    //    var bulletList = _LevelController.GetBullets(this, _Controller._Avoidance_Radius);

    //    if (bulletList.Count == 0)
    //        return avoidanceVector;

    //    foreach(var bullet in bulletList)
    //    {
    //        avoidanceVector += Evade(bullet.position);
    //    }

    //    return avoidanceVector.normalized;
    //}

    //Vector3 Evade(Vector3 bulletTarget)
    //{
    //    Vector3 newVelocity = (position - bulletTarget).normalized * _Controller._Max_Velocity;
    //    return newVelocity - velocity;
    //}

    //virtual protected Vector3 Combine()
    //{
    //    Vector3 FinalVector = _Controller._Cohesion_Priority * Cohesion() + _Controller._WanderPriority * Wander() 
    //        + _Controller._Aligment_Priority * Alignment() + _Controller._Separation_Priority * Separation() + 
    //        _Controller._Avoidance_Priority * Avoidance();
    //    return FinalVector;
    //}
    // void WrapAround(ref Vector3 vector, float min, float max)
    //{
    //    vector.x = WrapAroundFloat(vector.x, min, max);
    //    vector.y = WrapAroundFloat(vector.y, min, max); 
    //    vector.z = WrapAroundFloat(vector.z, min, max);
    //}
    //public float WrapAroundFloat(float value, float min, float max)
    //{
    //    if (value > max)
    //        value = min;
    //    else if (value < min)
    //        value = max;
    //    return value;
    //}
    //public float RandomBinomial()

    //{
    //    return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    //}

    //bool isOnFOV(Vector3 vec)
    //{

    //    return Vector3.Angle(this.velocity, -vec - this.position) <= _Controller._Max_Field_Of_View;
    //}

}
   

