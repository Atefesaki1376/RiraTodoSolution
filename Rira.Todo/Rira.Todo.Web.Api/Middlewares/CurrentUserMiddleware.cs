namespace Rira.Todo.Web.Api.Middlewares
{
    public class CurrentUserMiddleware<TUserId>
        where TUserId : struct, IEquatable<TUserId>
    {
        private readonly RequestDelegate _next;
        private const string roleClaimType = "role";

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentUser<TUserId> currentUser)
        {
            if (context.User?.Identity?.IsAuthenticated ?? false)
            {
                var claim = context.User.FindFirst("sub");
                if (claim != null)
                {
                    if (typeof(TUserId) == typeof(Guid))
                    {
                        currentUser.Id = (TUserId)(object)Guid.Parse(claim.Value ?? Guid.Empty.ToString());
                    }
                    else
                    {
                        currentUser.Id = (TUserId)(object)Convert.ToInt32(claim.Value ?? "0");
                    }
                }

                currentUser.Name = context.User.Identity?.Name ?? "Unknown";
                currentUser.Roles = context.User.Claims
                    .Where(c => c.Type == roleClaimType)
                    .Select(c => c.Value)
                    .ToList();
            }

            await _next(context);
        }
    }
}