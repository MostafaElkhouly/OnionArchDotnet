
using Persistence.IRepository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Domain.Entities.Base;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using Domain.Configration.EntitiesProperties;

namespace Persistence.Repository.EntityRepository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        protected readonly DbSet<TEntity> DbSet;
        private readonly AppDbContext context;
        private bool isAdmin = false;

        public Repository(AppDbContext context)
        {
            DbSet = context.Set<TEntity>();
            //DbSet.AsNoTracking();
            this.context = context;
        }
        public TEntity Add(TEntity entity)
        {
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                DbSet.Add(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> AddRange(TEntity[] entities)
        {
            try
            {
                DbSet.AddRange(entities);
                return entities.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> UpdateRange(TEntity[] entities)
        {
            try
            {
                DbSet.UpdateRange(entities);
                return entities.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = !entity.IsDeleted;
            entity.DateOfCreate = DateTime.Now;
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }

        }

       



        public TEntity Update(TEntity entity)
        {
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Modified;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateDbSet(TEntity entity)
        {
            try
            {
                DbSet.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> UpdateRing(TEntity[] entities)
        {
            foreach (var item in entities)
            {
                Update(item);
            }

            return entities.ToList();
        }

        public List<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TEntity> query = null;
                if (!isAdmin)
                {
                    query = DbSet.Where(e => e.IsDeleted == false);
                }
                else
                {
                    query = DbSet;
                }

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Count()
        {
            try
            {
                if (isAdmin)
                {
                    return DbSet.Count();
                }
                else
                {
                    return DbSet.Where(e => e.IsDeleted == false).Count();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {

                IQueryable<TEntity> query = null;

                if (isAdmin)
                {
                    query = DbSet;
                }
                else
                {
                    query = DbSet.Where(e => e.IsDeleted == false);
                }

                return query.Where(predicate).Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> GetAll()
        {
            try
            {
                if (isAdmin)
                {
                    return DbSet.ToList();
                }
                else
                {
                    //return DbSet.Where(e => e.IsDeleted == false).ToList();
                    return DbSet.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = DbSet.Where(predicate);

                if (!isAdmin)
                {
                    //query = query.Where(e => e.IsDeleted == false);
                }

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }


        }

        public TEntity GetSingle(Guid id)
        {
            try
            {
                return DbSet.AsTracking().Where(e => e.Id == id).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> GetSingleAsync(Guid id)
        {
            try
            {
                return await DbSet.AsTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return DbSet.AsTracking().Where(predicate).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includeProperties = null)
        {
            try
            {
                var query = DbSet.Where(predicate);

                

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includeProperties = null)
        {
            try
            {
                var query = DbSet.Where(predicate);



                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool HasAny(TEntity entity)
        {
            try
            {
                if (isAdmin)
                {
                    return DbSet.Where(e => e == entity).Any();
                }
                return DbSet.Where(e => e == entity).Any();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HasAny(Guid pId)
        {
            try
            {
                if (isAdmin)
                {
                    return DbSet.Where(e => e.Id == pId).Any();
                }
                return DbSet.Where(e => e.Id == pId).Any();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HasAny(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                if (isAdmin)
                {
                    return DbSet.Where(predicate).Any();
                }
                return DbSet.Where(predicate).Any();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TEntity> LoadHierarchy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                if (isAdmin)
                {
                    return query.Where(predicate).ToList();
                }

                return query.Where(predicate).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (!isAdmin)
                //query = query.Where(e => e.IsDeleted == false);

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }
                else
                {
                    query = query.AsTracking();
                }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (!isAdmin)
                //query = query.Where(e => e.IsDeleted == false);

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }
                else
                {
                    query = query.AsTracking();
                }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public TEntity GetLastOrDefault(Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (!isAdmin)
                //query = query.Where(e => e.IsDeleted == false);

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }
                else
                {
                    query = query.AsTracking();
                }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).LastOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }
        public List<TEntity> ToList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            //if(!isAdmin)
            //    query = query.Where(e => e.IsDeleted == false);

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            //if(!isAdmin)
            //    query = query.Where(e => e.IsDeleted == false);

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public List<TEntity> GetAll(int count)
        {
            try
            {
                return DbSet.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region Delete
        public void DeleteArc(TEntity entity)
        {
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRange(TEntity[] entities)
        {
            foreach (var item in entities)
            {
                DeleteArc(item);
            }
        }

        public void SoftDeleteRing(TEntity[] entities)
        {
            foreach (var item in entities)
            {
                SoftDelete(item);
            }
        }


        public void SoftDeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = DbSet.Where(predicate).FirstOrDefault();
            entity.IsDeleted = true;
            entity.DateOfCreate = DateTime.Now;
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteWhere(Expression<Func<TEntity, bool>> predicate, string UserName = "")
        {
            try
            {
                var entites = DbSet.Where(predicate);
                foreach (var entity in entites)
                {
                    // entity.UserName=UserName;
                    DbSet.Attach(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteWhereSoft(Expression<Func<TEntity, bool>> predicate, string UserName = "")
        {
            try
            {
                var entites = DbSet.Where(predicate);
                foreach (var entity in entites)
                {
                    entity.UserName = UserName;
                    entity.IsDeleted = !entity.IsDeleted;
                    entity.DateOfCreate = DateTime.Now;
                    DbSet.Attach(entity).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteWhereSoftNotSwich(Expression<Func<TEntity, bool>> predicate, string UserName = "")
        {
            try
            {
                var entites = DbSet.Where(predicate);
                foreach (var entity in entites)
                {
                    entity.UserName = UserName;
                    entity.IsDeleted = true;
                    entity.DateOfCreate = DateTime.Now;
                    DbSet.Attach(entity).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteWherePermenant(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entites = DbSet.Where(predicate);
                DbSet.RemoveRange(entites);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        public List<TEntity> FromSqlRaw(string sql)
        {
            return DbSet.FromSqlRaw(sql)
                 .ToList();
        }
        public TEntity GetFirstOrDefaultRandom()
        {
            try
            {

                IQueryable<TEntity> query = DbSet;
                return query.OrderBy(r => Guid.NewGuid()).FirstOrDefault();


            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> Sumpredicate)
        {
            try
            {

                return DbSet.Where(predicate).Sum(Sumpredicate);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TStored> GetData<TStored>(string sp_name, Dictionary<string, object> parames = null
           , CommandType CommandType = System.Data.CommandType.StoredProcedure)
        {

            using var cmd = context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = sp_name;
            cmd.CommandType = CommandType;
            //cmd.CommandTimeout = 180;

            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection.Open();

            if (parames != null)
                foreach (var item in parames)
                {
                    var param1 = new SqlParameter
                    {
                        ParameterName = item.Key,
                        SqlValue = item.Value
                    };

                    cmd.Parameters.Add(param1);
                }

            try
            {

                var reader = cmd.ExecuteReader();

                if (reader == null)
                {
                    return null;
                }

                List<TStored> list = new List<TStored>();

                var columns = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns.Add(reader.GetName(i).ToLower());
                }

                while (reader.Read())
                {
                    TStored obj = Activator.CreateInstance<TStored>();

                    int i = 0;
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {

                        if (columns.Contains(prop.Name.ToLower()) &&
                            !object.Equals(reader[prop.Name], DBNull.Value))
                        {
                            try
                            {
                                if (prop.PropertyType.FullName == "System.String")
                                {
                                    prop.SetValue(obj, Convert.ToString(reader[prop.Name]), null);
                                }
                                else if (prop.PropertyType.FullName.Contains("Boolean"))
                                {
                                    prop.SetValue(obj, Convert.ToBoolean(reader[prop.Name]), null);
                                }
                                else
                                {
                                    prop.SetValue(obj, reader[prop.Name], null);
                                }


                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("Boolean"))
                                {
                                    prop.SetValue(obj, Convert.ToBoolean(reader[prop.Name]), null);

                                }
                                else
                                {
                                    try
                                    {
                                        prop.SetValue(obj, reader[prop.Name].ToString(), null);

                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }


                                }

                            }
                        }
                        i++;




                    }
                    list.Add(obj);
                }

                reader.Close();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object GetValue<TStored>(string sp_name, Dictionary<string, object> parames = null
            , CommandType CommandType = System.Data.CommandType.StoredProcedure)
        {

            using var cmd = context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = sp_name;
            cmd.CommandType = CommandType;
            //cmd.CommandTimeout = 180;

            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection.Open();

            if (parames != null)
                foreach (var item in parames)
                {
                    var param1 = new SqlParameter
                    {
                        ParameterName = item.Key,
                        SqlValue = item.Value
                    };

                    cmd.Parameters.Add(param1);
                }

            try
            {
                return cmd.ExecuteScalar();




            }
            catch (Exception)
            {
                throw;
            }
        }
        public int Execute<TStored>(string sp_name, Dictionary<string, object> parames = null
            , CommandType CommandType = System.Data.CommandType.StoredProcedure)
        {

            using var cmd = context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = sp_name;
            cmd.CommandType = CommandType;
            //cmd.CommandTimeout = 180;

            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection.Open();

            if (parames != null)
                foreach (var item in parames)
                {
                    var param1 = new SqlParameter
                    {
                        ParameterName = item.Key,
                        SqlValue = item.Value
                    };

                    cmd.Parameters.Add(param1);
                }

            try
            {

                return cmd.ExecuteNonQuery();


            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public async Task<List<TStored>> GetDataAsync<TStored>(string sp_name, Dictionary<string, object> parames = null
          , CommandType CommandType = System.Data.CommandType.StoredProcedure)
        {
            using var connection = context.Database.GetDbConnection();
            var cmd = connection.CreateCommand();
            cmd.CommandText = sp_name;
            cmd.CommandType = CommandType;
            //cmd.CommandTimeout = 180;

            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            if (parames != null)
                foreach (var item in parames)
                {
                    var param1 = new SqlParameter
                    {
                        ParameterName = item.Key,
                        SqlValue = item.Value
                    };

                    cmd.Parameters.Add(param1);
                }

            try
            {

                var reader = await cmd.ExecuteReaderAsync();

                if (reader == null)
                {
                    return null;
                }

                var table = new DataTable();
                table.Load(reader);

                return JsonConvert.DeserializeObject<List<TStored>>(DataTableToJSONWithJSONNet(table));
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable CreateDataTable<E>(IEnumerable<E> list)
        {
            Type type = typeof(E);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (E entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        /**
         * remarks I need To Review This Method Again 
         */
        public DataTable Get(string sql, Dictionary<string, string> parameters)
        {
            
            SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(pair.Key, pair.Value));
                }
            }

            var dt = new DataTable();

            var da = new SqlDataAdapter(cmd);
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);


            return dt;
        }

        public List<TResult> Get<TResult>(string sql)
        {
            List<TResult> data = new List<TResult>();
            using (SqlConnection connection = new SqlConnection(
               context.Database.GetDbConnection().ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //TResult item = GetItem<TResult>(reader);
                        //data.Add(item);
                    }
                }
            }

            return data;
        }
    }


}

