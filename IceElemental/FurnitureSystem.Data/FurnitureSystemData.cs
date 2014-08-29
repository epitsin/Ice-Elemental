﻿namespace FurnitureSystem.Data
{
    using System;
    using System.Collections.Generic;

    using FurnitureSystem.Data.Repositories;
    using FurnitureSystem.Models;

    public class FurnitureSystemData : IFurnitureSystemData
    {
        private readonly IFurnitureSystemDbContext context;

        private readonly IDictionary<Type, object> repositories;

        public FurnitureSystemData()
            : this(new FurnitureSystemDbContext())
        {
        }

        public FurnitureSystemData(IFurnitureSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        //// should we have such property for all classes????????????????????

        public IGenericRepository<FurniturePiece> FurniturePieces
        {
            get
            {
                return this.GetRepository<FurniturePiece>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}