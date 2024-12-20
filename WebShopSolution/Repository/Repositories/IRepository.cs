﻿namespace Repository.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        void Add (T item);

        void Update (T item);

        void Delete (int id);
    }
}
