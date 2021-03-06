﻿namespace webapp.Database.Models
{
    /// <summary>
    /// The object describes a single CVE (Common Vulnerabilities and Exposures).
    /// </summary>
    public class CVE
    {
        /// <summary>
        /// The <see href="https://cve.mitre.org/cve/identifiers/index.html">CVE identifier</see>,
        /// which looks like `CVE-2005-2541` or `TEMP-0290435-0B57B5`.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Short CVE description.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The name of the package, where CVE was found, for example "tar" or "sysvinit-utils".
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Detailed CVE description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Highlights how dangerous the CVE is.
        /// </summary>
        public CveSeverity Severity { get; set; }

        /// <summary>
        /// In most cases, suggests to update the sources to the version of the package, that addresses the issue.
        /// </summary>
        public string Remediation { get; set; }

        /// <summary>
        /// Links to external source with more details about the CVE.
        /// </summary>
        public string References { get; set; }
    }

    /// <summary>
    /// CVE severities.
    /// </summary>
    public enum CveSeverity
    {
        /// <summary>
        /// The level is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The lowest severity in the rank. Represents only a slight damage.
        /// </summary>
        Low = 100,

        /// <summary>
        /// Medium-level problem, which could cause moderate damage or loss.
        /// </summary>
        Medium = 200,

        /// <summary>
        /// Important issue, which should be carefully reviewed.
        /// </summary>
        High = 300,

        /// <summary>
        /// Top-level issues, that often should be addressed as soon as possible.
        /// </summary>
        Critical = 400,
    }
}