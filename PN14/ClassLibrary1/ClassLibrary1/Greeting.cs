namespace ClassLibrary1
{
    public class Greeting
    {
        private readonly string _name;

        public Greeting(string anOutput) { 
            _name = anOutput;
        }

        public string output() {
            return _name;
        }

        ~Greeting() { }
    }
}
