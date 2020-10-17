﻿using System;
using Calculator;
using UnityEngine;

namespace DefaultNamespace
{
    public class OpenClosedExamples : MonoBehaviour
    {
        /*
         * Demonstrates the interface and delegate ways of solving the Calculation task
         */
        delegate int DoCalculation(int x, int y); // main delegate definition
        private DoCalculation doCalulation; // a delegate of the type defined above
        private int sum;
        void Start()
        {
            // USING INTERFACE
            ICalculate calculate = new Add();
            sum = calculate.Calculate(3, 7);
            Debug.Log(String.Format("Sum {0} - {1} = {2}", 3, 7, sum));
            calculate = new Subtract();
            sum = calculate.Calculate(3, 7);
            Debug.Log(String.Format("Subtract {0} - {1} = {2}", 3, 7, sum));
            
            
            // Delegate gets the Add version of the static class Calculations assigned 
            doCalulation = Calculations.Add;
            sum = doCalulation(3, 7);
            Debug.Log(String.Format("Delegate Add A {0} - {1} = {2}", 3, 7, sum));
            
            // GENERIC DELEGATES same end result as above but using a generic built-in type called Func
            //   in,  in,  out - Func allows 14 inputs and one return out type
            Func<int, int, int> calculateDelegate;
            calculateDelegate = Calculations.Add;
            sum = calculateDelegate(3, 7);
            
            Debug.Log(String.Format("Delegate Add B {0} - {1} = {2}", 3, 7, sum));

        }
    }
}