using Airline.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return [];
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
            }
        }

        public T? GetById(int id)
        {
            try
            {
                return context.Set<T>().Find(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Aircrafts list error!");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
