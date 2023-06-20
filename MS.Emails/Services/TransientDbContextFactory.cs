using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MS.Emails.Entities;
using MS.Emails.Interfaces;
using MS.Emails.Respositories;

namespace MS.Emails.Services;


public class TransientDbContextFactory : DbContext
    {

        public DbSet<CodigoEmail> CodigosEmail { get; set; }
        public TransientDbContextFactory(DbContextOptions<TransientDbContextFactory> options) : base(options)
        {

        }
    }
