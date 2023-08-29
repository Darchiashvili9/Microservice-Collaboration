namespace LoyaltyProgramServiceTests.Mocks
{
    public class MocksHost : IDisposable
    {
        private readonly IHost hostForMocks;

        public MocksHost(int port)
        {
            this.hostForMocks = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x => x
                .ConfigureServices(services => services.AddControllers()).Configure(app => app.UseRouting().UseEndpoints(opt => opt.MapControllers()))
                .UseUrls($"http://localhost:{port}"))
                .Build();
            
            new Thread(() =>
            {
                this.hostForMocks.Run();
            }).Start();
        }

        public void Dispose()
        {
            this.hostForMocks.Dispose();
        }
    }
}
