namespace Calculator
{
    /*
     * Solution based on if else blocks
     */
    public class CalculateOrig
    {
        
        public int Calculate(int x, int y,  string action) {
            if(action == "Add") {
                return x+y; 
            } else if(action == "Sub") {
                return x-y; 
            }
            /*else if {
                //etc
            }
             Extends with extra if else statements
             To remove a method requires re-editing this class
            */
           
            return 0;
        }

    }
}