using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Entities.Queries.Context.GetCurrentUserContext
{
    public class GetCurrentUserContextHandler : IRequestHandler<GetCurrentUserContextQuery, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCurrentUserContextHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<int> Handle(GetCurrentUserContextQuery request, CancellationToken cancellationToken)
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));

            return Task.FromResult(userId);
        }
    }
}
