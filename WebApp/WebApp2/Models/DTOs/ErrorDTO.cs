using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models.DTOs
{
    public class ErrorDTO
    {
        public string Error { get; set; }

        public ErrorDTO()
        {
        }

        public ErrorDTO(string error)
        {
            Error = error;
        }
    }
}
