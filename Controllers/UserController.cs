using Microsoft.AspNetCore.Mvc;
using EFCoreAPIDemo.Repositories;
using System.Threading.Tasks;
using EFCoreAPIDemo.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace EFCoreAPIDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userRepositories;
        private readonly IMapper _mapper;
        private IOptions<ApiBehaviorOptions> _apiBehaviorOptions;
        public UserController(IUser userRepositories, IMapper mapper, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            _userRepositories = userRepositories;
            _mapper = mapper;
            _apiBehaviorOptions = apiBehaviorOptions;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(_userRepositories.GetAll());
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetById(int userId)
        {
            User user = _userRepositories.Get(userId);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            int result = _userRepositories.Create(user);

            if(result == -1)
            {
                ModelState.AddModelError(nameof(user.EmailId), "EmailId already exist");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var newUser = _userRepositories.Get(result);

            return CreatedAtAction("GetById", new { userId = newUser.Id }, newUser);
        }

        [HttpPatch]
        [Route("{userId}")]
        public IActionResult Update([FromRoute] int userId, [FromBody] JsonPatchDocument<UserPatch> userPatch)
        {
            var user = _userRepositories.Get(userId);

            if(user == null)
            {
                return NotFound();
            }

            UserPatch patchDTO = _mapper.Map<UserPatch>(user);

            userPatch.ApplyTo(patchDTO);

            TryValidateModel(patchDTO);

            _mapper.Map(patchDTO, user);

            int result = _userRepositories.Update(user);

            if (result == -1)
            {
                ModelState.AddModelError(nameof(user.EmailId), "EmailId already exist");
                return _apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            return Ok(user);
        }
    }
}
