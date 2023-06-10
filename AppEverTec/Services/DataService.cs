using AppEverTec.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.Services
{
    public class DataService : IDataService
    {
        public SQLiteAsyncConnection connection;
        public string Error { get; set; }
        public DataService()
        {
#pragma warning disable CS4014 // Dado que no se esperaba esta llamada, la ejecución del método actual continuará antes de que se complete la llamada
            OpenOrCreateDB();

            ValidateRegisterChange();
#pragma warning restore CS4014 // Dado que no se esperaba esta llamada, la ejecución del método actual continuará antes de que se complete la llamada
        }
        private async Task OpenOrCreateDB()
        {
            try
            {


                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Evertec2.db");
                this.connection = new SQLiteAsyncConnection(path);
                await connection.CreateTableAsync<RegisterChange>().ConfigureAwait(false);
                await connection.CreateTableAsync<Visitor>().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }
        }

        public async Task ValidateRegisterChange()
        {
            var query = await this.connection.QueryAsync<RegisterChange>("Select * from [RegisterChange]");
            if (query.Count == 0)
            {
                var registerChange = new RegisterChange
                {
                    Id = 1,
                    DateRegister = DateTime.Now.ToString("dd MMMM,yyyy")
                };
                await this.connection.InsertAsync(registerChange);
            }
        }

        public async Task<Visitor> GetVisitor(int id)
        {
            return await this.connection.Table<Visitor>().Where(w=>w.id==id).FirstOrDefaultAsync();
        }
        public async Task<List<Visitor>> GetVisitor()
        {
            try
            {
                var query = await this.connection.QueryAsync<Visitor>("Select * from [Visitor]");
                //var visitors= await this.connection.Table<Visitor>().ToListAsync();
                return query;
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                throw;
            }
        }

        public async Task Insert<T>(T model) where T : class
        {
            try
            {
                await this.connection.InsertAsync(model);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        public async Task Update<T>(T model) where T : class
        {
            try
            {
                await this.connection.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        public async Task Delete<T>(T model) where T : class
        {
            try
            {
                await this.connection.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }
    }
}
