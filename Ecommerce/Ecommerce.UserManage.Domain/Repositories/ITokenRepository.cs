using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.userManage.Domain.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTtoken(IdentityUser user, List<string> roles);
    }
}
