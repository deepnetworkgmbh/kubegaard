﻿using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using joseki.db;
using joseki.db.entities;

using Microsoft.EntityFrameworkCore;

using Serilog;

using webapp.Configuration;
using webapp.Database.Models;

namespace webapp.Database
{
    /// <summary>
    /// Keeps track of Check items to reduce amount of interactions with real database.
    /// The objects stores in memory identifiers of Checks, that are already in database.
    /// It does requests only to update expired items or query not cached yet records.
    /// The cache is expected to be used in Singleton mode.
    /// </summary>
    public class ChecksCache
    {
        private static readonly ILogger Logger = Log.ForContext<ChecksCache>();

        private readonly ConcurrentDictionary<string, CheckCacheItem> cache = new ConcurrentDictionary<string, CheckCacheItem>();

        private readonly JosekiConfiguration config;
        private readonly JosekiDbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecksCache"/> class.
        /// </summary>
        /// <param name="config">Joseki configuration object.</param>
        /// <param name="db">Joseki database instance.</param>
        public ChecksCache(ConfigurationParser config, JosekiDbContext db)
        {
            this.config = config.Get();
            this.db = db;
        }

        /// <summary>
        /// The method gets Check integer identifier from in-memory list, or if it's not there does requests to the Database.
        /// The method could do one of the following:
        /// - returns integer Id from memory if it's there;
        /// - if Id is not cached yet - queries it from the Database;
        /// - if Check is not in the Database yet - inserts it and stores the result in memory for future requests.
        /// If the cache item was not updated longer than a configured threshold - updates the record in Database.
        /// </summary>
        /// <param name="id">Check string identifier.</param>
        /// <param name="checkFactory">The factory method to generate a new Check object.</param>
        /// <returns>The integer identifier, which is used as Database Primary and Foreign Keys.</returns>
        public async Task<int> GetOrAddItem(string id, Func<Check> checkFactory)
        {
            // if item is not in cache - get it from db or add a new one;
            if (!this.cache.TryGetValue(id, out var item))
            {
                var entity = await this.db.Set<CheckEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.CheckId == id);

                if (entity == null)
                {
                    Logger.Information("Adding new Check item {CheckId} to the database", id);
                    var addedEntity = this.db.Set<CheckEntity>().Add(checkFactory().ToEntity());
                    await this.db.SaveChangesAsync();

                    entity = addedEntity.Entity;
                }

                item = this.cache.GetOrAdd(id, new CheckCacheItem
                {
                    CheckId = id,
                    Id = entity.Id,
                    UpdatedAt = entity.DateUpdated,
                });
            }

            var threshold = DateTime.UtcNow.AddDays(-this.GetItemTtl(id));
            if (item.UpdatedAt < threshold)
            {
                Logger.Information("Updating expired Check item {CheckId} in the database", id);
                var check = checkFactory();
                this.db.Set<CheckEntity>().Update(check.ToEntity());
                await this.db.SaveChangesAsync();

                item.UpdatedAt = DateTime.UtcNow;
            }

            return item.Id;
        }

        private int GetItemTtl(string id)
        {
            if (id.StartsWith("polaris"))
            {
                return this.config.Cache.PolarisCheckTtl;
            }
            else if (id.StartsWith("azure"))
            {
                return this.config.Cache.AzureCheckTtl;
            }
            else
            {
                return this.config.Cache.DefaultTtl;
            }
        }
    }

    internal class CheckCacheItem
    {
        public int Id { get; set; }

        public string CheckId { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}