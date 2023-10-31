namespace ASP.NET_Custom_Identity_Starter.Middlewares
{
    public class LastLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LastLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user != null)
                {
                    user.LogIn();
                    await userManager.UpdateAsync(user);
                }
            }

            await _next(context);
        }
    }
}
