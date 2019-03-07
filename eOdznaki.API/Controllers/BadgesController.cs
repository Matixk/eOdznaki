using AutoMapper;
using eOdznaki.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOdznaki.Controllers
{
    public class BadgesController : ControllerBase
    {
        private readonly IBadgeRepository repository;
        private readonly ILogger<BadgesController> logger;
        private readonly IMapper mapper;

        public BadgesController(IBadgeRepository repository,
            ILogger<BadgesController> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }


    }
}
