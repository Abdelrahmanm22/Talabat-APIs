﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IResponseCacheService
    {
        //Cache Data
        Task CacheResponse(string CacheKey, object Response, TimeSpan ExpireTime);
        
        //Get Cached Data
        Task<string?> GetChachedResponse(string CacheKey);

    }
}
