using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionHandling.API.Commands.Students
{
    public class CreateStudent:IRequest<long>
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
