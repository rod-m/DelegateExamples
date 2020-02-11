using System;
using Calculator;
using UnityEngine;

namespace DefaultNamespace
{
    public class OpenClosedExamples : MonoBehaviour
    {
        delegate int DoCalculation(int x, int y);

        private DoCalculation doCalulation;
        
        void Start()
        {
            // USING INTERFACE
            ICalculate calculate = new Add();
            int sum = calculate.Calculate(3, 7);
            Debug.Log(String.Format("Sum {0} - {1} = {2}", 3, 7, sum));
            calculate = new Subtract();
            sum = calculate.Calculate(3, 7);
            Debug.Log(String.Format("Subtract {0} - {1} = {2}", 3, 7, sum));
            
            
            // Delegate
            doCalulation = Calculations.Add;
            sum = doCalulation(3, 7);
            Debug.Log(String.Format("Delegate Add A {0} - {1} = {2}", 3, 7, sum));
            
            // DELEGATES same as above but using a generic built-in type called Func
            //   in,  in,  out
            Func<int, int, int> calculateDelegate;
            calculateDelegate = Calculations.Add;
            int sumD = calculateDelegate(3, 7);
            
            Debug.Log(String.Format("Delegate Add B {0} - {1} = {2}", 3, 7, sumD));

        }
    }
}