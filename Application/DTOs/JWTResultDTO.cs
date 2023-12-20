using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class JWTResultDTO
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
