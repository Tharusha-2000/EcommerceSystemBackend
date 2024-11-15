using Ecommerce.userManage.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.userManage.Application.Services
{
    public interface IEmailService
    {
        string SendEmail(RequestDTO request);
    }
}
