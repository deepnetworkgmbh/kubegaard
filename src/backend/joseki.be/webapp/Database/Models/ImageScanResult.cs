﻿using System;
using System.Collections.Generic;

namespace webapp.Database.Models
{
    /// <summary>
    /// The goal of image scan is verify if known CVEs (Common Vulnerabilities and Exposures) present in used by image packages.
    /// Therefore, the Scan Result consists of an array of CVEs. The array might be empty, if no vulnerabilities was found.
    /// </summary>
    public class ImageScanResult
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Full image tag, which includes registry, image name and version.
        /// For example `mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine3.10`.
        /// </summary>
        public string ImageTag { get; set; }

        /// <summary>
        /// UTC point of time when scan was performed.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// List of discovered vulnerabilities.
        /// </summary>
        public List<ImageScanToCve> FoundCVEs { get; set; }
    }
}