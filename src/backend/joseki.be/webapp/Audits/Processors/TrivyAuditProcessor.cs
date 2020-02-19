﻿using System.Threading;
using System.Threading.Tasks;

using webapp.BlobStorage;

namespace webapp.Audits.Processors
{
    /// <summary>
    /// Trivy audit processor.
    /// </summary>
    public class TrivyAuditProcessor : IAuditProcessor
    {
        /// <inheritdoc />
        public Task Process(ScannerContainer container, CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}