namespace ReferenceMaterials_API.Persistence

{
        //Create a Irepository Interface to inject the class (data model) when we are initializing the class
        //creating a interface helps us in changing the database like for example mangodb in future (without having to change other layers in the database)
        public interface IRepository<T>
        {
            //Finding 1 element in the database by id
            public T Get(string id);

            //When we have a large database it is good to retrieve the data page wise instead of all data so that process is not taking too much time of the user
            //like from which page and the limit of the page
            //IEnumerable is a representation of a collection of sata
            public IEnumerable<T> GetAll(int pageSize, int pageNumber);

            //Getting the whole list
            public IEnumerable<T> GetAll();

            //Inserting or updating an object in the database
            public void InsertOrUpdate(T element);
        }


}
