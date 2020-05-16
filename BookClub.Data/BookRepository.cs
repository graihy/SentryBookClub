using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BookClub.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookClub.Data
{
    public class BookRepository : IBookRepository
    {
       // private readonly IDbConnection _db;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        //public BookRepository(IDbConnection db, ILoggerFactory loggerFactory)
        public BookRepository(IConfiguration config, ILoggerFactory loggerFactory)
        {
            //_db = db;
            _config = config;
            //_logger = loggerFactory.CreateLogger("Database");
            _logger = loggerFactory.CreateLogger("Database");
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("BookClubDb"));
            }
        }

        public List<Book> GetAllBooks()
        {
            using (IDbConnection conn = Connection)
            {
                _logger.LogInformation("Inside the repository about to call GetAllBooks.");
                //_logger.RepoGetBooks();

                _logger.LogDebug(DataEvents.GetMany, "Debugging information for stored proc: {ProcName}", "GetAllBooks");
                //_logger.RepoCallGetMany("GetAllBooks");
                conn.Open();
                var books = conn.Query<Book>("GetAllBooks", commandType: CommandType.StoredProcedure)
                    .ToList();
                return books;
            }
        }

        public void SubmitNewBook(Book bookToSubmit, int submitter)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                conn.Execute("InsertBook", new
                {
                    bookToSubmit.Title,
                    bookToSubmit.Author,
                    bookToSubmit.Classification,
                    bookToSubmit.Genre,
                    bookToSubmit.Isbn,
                    submitter
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
