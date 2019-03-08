using AutoMapper;
using eOdznaki.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUsersRepository usersRepository;

        public UsersController(
            IUsersRepository usersRepository,
            IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

    }
}