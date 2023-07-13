namespace SCParking.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string connectionString)
        {
            //SqlServer = new UserDAO(connectionString);
        }
        //public IUserDAO SqlServer { get; private set; }
    }
}
