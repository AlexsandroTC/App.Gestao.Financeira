using App.Gestao.Financeira.Domain;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Gestao.Financeira.Configuration
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        
        public Database(string path)
        {
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<Transacao>();
        }

        public Task<List<Transacao>> GetTrascaoAsync()
        {
            return _database.Table<Transacao>().ToListAsync();
        }

        public Task<int> SaveTrancaosaoAsync(Transacao transacao)
        {
            return _database.InsertAsync(transacao);
        }
    }
}