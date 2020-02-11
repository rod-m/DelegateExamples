namespace Calculator
{
    public class Add : ICalculate
    {
        /*
    * Demonstrate Open Closed Principal
    * Conforms to ICalculate inteface
    */
        #region ICalculate Members 

        public int Calculate(int x, int y)
        {
            return x + y;
        }
        #endregion
    }
}