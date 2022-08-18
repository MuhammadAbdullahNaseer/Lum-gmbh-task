using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using Raven.Client.Exceptions.Database;
using Microsoft.EntityFrameworkCore;
using ReferenceMaterials_API.Models;

namespace ReferenceMaterials_API.Data
{
    public interface IRavenDbContext
    {
        public IDocumentStore store { get;}

        

    }

    //Implementing Interface:
    public class RavenDbContext:IRavenDbContext //context is what we need to access the store and to open a ravendb session. This will allow us to query content or store content
    {
        private readonly IDocumentStore _localStore;
        public IDocumentStore store => _localStore;

        private readonly PersistenceSettings _persistenceSettings;
        

        //IOptionsMonitor is the interface to retrive the objects which are set via session.config (within program file) in other classes e.g:
        public RavenDbContext(IOptionsMonitor<PersistenceSettings> settings)
        {
            //Making access to the database as:
            _persistenceSettings = settings.CurrentValue;//we can access database name and urls

            //creating a local document store 
            _localStore = new DocumentStore()
            {
                Database = _persistenceSettings.DatabaseName,
                Urls = _persistenceSettings.Urls
            };

            //Initialize the database:
            _localStore.Initialize();

            //ensuring that the database exists:
            EnsureDatabaseIsCreated();

        }

        public void EnsureDatabaseIsCreated()
        {
            //to make sure there is a database to access and if there is not database then create one
            try
            {
                //if we get statistice for the database that means thet the database exists:
                _localStore.Maintenance.ForDatabase(_persistenceSettings.DatabaseName).Send(new GetStatisticsOperation());
            }
            catch (DatabaseDoesNotExistException)
            {

                //if we were not able to retrieve the database then we can create one as:
                //entering the maintenance area of the server to create a database using create database operation as:
                _localStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(_persistenceSettings.DatabaseName)));

                throw;
            }
        }
          
    }
}
