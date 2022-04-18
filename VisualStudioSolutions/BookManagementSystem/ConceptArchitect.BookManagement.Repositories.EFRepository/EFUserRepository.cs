using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class EFUserRepository : IUserRepository
    {
        BMSContext context;
        public EFUserRepository(BMSContext context)
        {
            this.context = context;
        }
        public async Task<User> Add(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Find(Func<User, bool> condition)
        {
            await Task.Yield();
            return context.Users.FirstOrDefault(condition);
        }
    }
}
