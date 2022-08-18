using ReferenceMaterials_API.Data;
using System.Linq;

namespace ReferenceMaterials_API.Persistence
{
        //Implementing the interface as:
        public class RavenDbRepository<T> : IRepository<T>
        {
            private readonly IRavenDbContext _context;

            public RavenDbRepository(IRavenDbContext context)
            { 
                _context=context;
               
            }
            public T Get(string id)
            {
                //starting a ravendb session to access the data within the ravendb database: (Ravendb methods are used)
                using var session = _context.store.OpenSession();//; it will close the session automatically when theb method at it's end
                var element = session.Load<T>(id);
                return element;
            }

            public IEnumerable<T> GetAll(int pageSize, int pageNumber)
            {
                //opening session: 
                using var session = _context.store.OpenSession();
                var elements = session.Query<T>()//finding the data
                    .Skip(pageSize * (pageNumber -1))//skip the pages upto the latest one, using a optimised logic here
                    .Take(pageSize)//taking the data after the skip
                    .ToList();//storing it in a list
            //if it does not find the data in the provided range, it will not cause exception, rather it will just return empty. 

            return elements;

            }

            public IEnumerable<T> GetAll()
            {
                //opening session: 
                using var session = _context.store.OpenSession();
                var elements = session.Query<T>().ToList();//finding the data
                return elements;

            }

        public void InsertOrUpdate(T element)
            {
                //the fun and good thing sbout this store method is that it will replace a element with the same id and if it has a uniqure id then it will store it as a new element
                using var session = _context.store.OpenSession();
                session.Store(element);
                session.SaveChanges();

            }
        }


}
