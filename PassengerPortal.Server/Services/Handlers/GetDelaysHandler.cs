using MediatR;
using Microsoft.EntityFrameworkCore;
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PassengerPortal.Server.Builders;

namespace PassengerPortal.Server.Handlers;

public class GetDelaysHandler : IRequestHandler<GetDelaysQuery, List<Delay>>
{
    private readonly ApplicationDbContext _context;

    public GetDelaysHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Delay>> Handle(GetDelaysQuery request, CancellationToken cancellationToken)
    {
        return await _context.Delays.ToListAsync(cancellationToken);
    }
}