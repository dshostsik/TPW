namespace Data
{

    public abstract class DataAbstract
    {
        public static DataAbstract init() { 
            return new DataAPI(); 
        }
    }

    internal class DataAPI : DataAbstract
    {
        public DataAPI() { }
    }
}
