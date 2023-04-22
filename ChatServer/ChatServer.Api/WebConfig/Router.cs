using ChatServer.Api.Hubs;

namespace ChatServer.Api.WebConfig
{
	public static class Router
	{
		public static void MapAppRoute(this IEndpointRouteBuilder endpoints)
		{
			endpoints.MapControllers();
			endpoints.MapHub<ChatHub>("/realtime");
		}
	}
}
