using eCinemas.Data.Base;
using eCinemas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace eCinemas.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {     

        public ActorsService(AppDbContext context) : base(context) { }      
       
    }
}
