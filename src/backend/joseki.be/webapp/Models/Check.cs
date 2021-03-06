﻿using System;
using System.Collections.Generic;

namespace webapp.Models
{
    /// <summary>
    /// Check against a component.
    /// </summary>
    public class Check
    {
        /// <summary>
        /// the date of the check.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Name of the collection.
        /// kubernetes: namespace.
        /// azks: resource-group.
        /// </summary>
        public Collection Collection { get; set; }

        /// <summary>
        /// category of the check
        /// kubernetes: polaris/trivy category.
        /// azks: feature name.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// the resource of check made against.
        /// k8s: object (deployment, pod, service etc).
        /// azks: resource (keyvault etc).
        /// </summary>
        public Resource Resource { get; set; }

        /// <summary>
        /// The control name of the check.
        /// k8s: polaris `check` name.
        /// azks: azks `control` name.
        /// </summary>
        public CheckControl Control { get; set; }

        /// <summary>
        /// Result of the check.
        /// </summary>
        public CheckResult Result { get; set; }

        /// <summary>
        /// Key-value pairs with custom labels/tags/attributes.
        /// </summary>
        public Dictionary<string, string> Tags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Check"/> class.
        ///  Empty constructor.
        /// </summary>
        public Check()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Check"/> class.
        /// create unique id on constructor.
        /// </summary>
        public Check(DateTime date, Collection collection, Resource resource, string category, CheckControl control, CheckResult result)
        {
            this.Date = date;
            this.Category = category;
            this.Collection = collection;
            this.Control = control;
            this.Result = result;
            this.Resource = resource;
            this.Tags = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Check used in Overview detail.
    /// </summary>
    public class OverviewCheck : Check
    {
        /// <summary>
        /// Component of the check.
        /// </summary>
        public InfrastructureComponent Component { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewCheck"/> class.
        /// create unique id on constructor.
        /// </summary>
        public OverviewCheck(DateTime date, Collection collection, Resource resource, string category, CheckControl control, CheckResult result, InfrastructureComponent component)
            : base(date, collection, resource, category, control, result)
        {
            this.Component = component;
        }
    }
}
