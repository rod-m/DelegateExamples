namespace Calculator
{
    public class Subtract : ICalculate
    {
        /*
    * Demonstrate Open Closed Principal
    */
        #region ICalculate Members 

        public int Calculate(int x, int y)
        {
            return x - y;
        }
        
        #endregion
    }
}