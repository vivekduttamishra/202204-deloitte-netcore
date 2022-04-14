using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Service
{
    public interface IDisplayTextService
    {
        public string DisplayText();
        public void AddStats(string url);
        public void Add404Urls(string url);
        public string ShowWrongUrlList();
    }
}
