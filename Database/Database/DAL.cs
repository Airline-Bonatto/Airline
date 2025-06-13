using Airline.Database;

using AirlineAPI.Exceptions;

namespace Airline.DAL
{
    public class DAL<T> where T : class
    {
        private readonly AirlineContext context;

        public DAL(AirlineContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> List()
        {
            try
            {
                return context.Set<T>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Aircrafts list error!");
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void Register(T model)
        {
            try
            {
                context.Set<T>().Add(model);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Model register error!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void Update(T model)
        {
            try
            {
                context.Set<T>().Update(model);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Aircraft update error!");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void Remove(T model)
        {
            try
            {
                context.Set<T>().Remove(model);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Aircraft remove error!");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public T GetById(int id)
        {
            try
            {
                var entity = context.Set<T>().Find(id);
                if (entity is null)
                {
                    throw new EntityNotFoundException();
                }
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine("Aircrafts list error!");
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
