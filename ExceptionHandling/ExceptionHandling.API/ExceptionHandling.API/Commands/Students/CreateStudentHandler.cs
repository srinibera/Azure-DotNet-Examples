using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ExceptionHandling.API.Commands.Students
{
    public class CreateStudentHandler : IRequestHandler<CreateStudent, long>
    {
        public async Task<long> Handle(CreateStudent request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => { return 1; });
        }
    }
}
