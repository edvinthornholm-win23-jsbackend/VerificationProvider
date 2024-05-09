using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificationProvider.Data.Contexts;
using VerificationProvider.Functions;

namespace VerificationProvider.Services;

public class VerificationCleanerService(ILogger<VerificationCleanerService> logger, DataContext dataContext) : IVerificationCleanerService
{

    private readonly ILogger<VerificationCleanerService> _logger = logger;
    private readonly DataContext _context = dataContext;

    public async Task RemoveExpiredRecordAsync()
    {
        try
        {
            var expired = await _context.VerificationRequests.Where(x => x.ExpiryDate < DateTime.Now).ToListAsync();
            _context.RemoveRange(expired);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : RemoveExpiredRecordAsync.RemoveExpiredRecordAsync() :: {ex.Message}", ex);

        }
    }
}
