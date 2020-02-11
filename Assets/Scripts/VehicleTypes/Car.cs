using UnityEngine;

namespace VehicleTypes
{
    public abstract class Vehicle
    {
        public static int wheels = 4;
        public virtual void DescribeCar()
        {
            Debug.Log("Four wheels, engine and steering wheel.");
        }

        public virtual void ShowDetails()
        {
            Debug.Log("Standard vehicle type");
        }
    }

    public class Tank : Vehicle
    {
        new public static int wheels = 2;
        public virtual void ShowDetails()
        {
            Debug.Log("Destroys anything in one shot, moves slowly.");
        }
    }
    public class Jeep : Vehicle
    {
        public virtual void ShowDetails()
        {
            Debug.Log("Move around fast!");
        }
    }
    
}