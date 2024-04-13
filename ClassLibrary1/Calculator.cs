using ClassLibrary1;

namespace ClassLibrary1
{
    public abstract class Calculator
    {
       /* public abstract vector add(vector v1, vector v2);

        public abstract class vector { 
            public int a;
            public int b; 
            public vector(int a, int b) { 
                this.a = a; 
                this.b = b; 
            }
        } later */

        public int add (int a, int b) { return a+b; }
    }
}

namespace ABC 
{
    public class Calculator2 : Calculator 
    {
        
    }
    public class Calculator3 : Calculator
    {

    }

   /* public class vector1 : Calculator.vector later
    {
        public vector1(int a, int b) : base(a, b)
        {
        }

        override public Calculator.vector add(vector1 v1, vector1 v2)
        {
            return new vector1(v1.a + v2.a, v1.b + v2.b);
        }
    }*/
}
