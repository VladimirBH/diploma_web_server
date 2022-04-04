namespace WebServer.Services
{
    public class GetMetallCostsService : BackgroundService
    {
    
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Выполняем задачу пока не будет запрошена остановка приложения
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    
                }
                catch (Exception ex)
                {
                    // обработка ошибки однократного неуспешного выполнения фоновой задачи
                }
    
                await Task.Delay(5000);
            }
        }
    }
}