namespace Calculator
{
    /*
    * Demonstrate Open Closed Principal
    * Conforms to ICalculate inteface
    */
    public class Subtract : ICalculate
    {
        #region ICalculate Members 

        public int Calculate(int x, int y)
        {
            return x - y;
        }
        
        #endregion
    }
}