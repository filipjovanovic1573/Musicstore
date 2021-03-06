﻿using Musicstore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Musicstore.Models;

namespace Musicstore.Repository {
    public abstract class Repository<C, T> : IRepository<C, T> where C : class where T : class {
        protected ApplicationDbContext DbContext { get { return new ApplicationDbContext(); } }
        public Task<int> CreateAsync(C model) {
            DbContext.Entry(model).State = System.Data.Entity.EntityState.Added;
            return Task.FromResult(DbContext.SaveChanges());
        }

        public Task<int> UpdateAsync(C model) {
            DbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            return Task.FromResult(DbContext.SaveChanges());
        }
        public abstract Task<List<C>> GetAllAsync();
        public abstract Task<List<C>> GetAsync(int ammount = 1);
        public abstract Task<C> GetByIdAsync(T id);
        public virtual void Dispose() {
            DbContext.Dispose();
        }
    }
}