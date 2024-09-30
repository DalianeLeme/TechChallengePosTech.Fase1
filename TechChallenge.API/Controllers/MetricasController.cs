using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using System.Text;

namespace TechChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricasController : ControllerBase
    {
        static readonly Histogram RequestDuration = Metrics.CreateHistogram(
            "http_request_duration_seconds",
            "Histogram of HTTP request durations in seconds",
            new HistogramConfiguration
            {
                Buckets = Histogram.LinearBuckets(start: 0.1, width: 0.1, count: 10)
            }
        );

        // Gauge para monitorar o uso da CPU
        private static readonly Gauge CpuUsageGauge = Metrics.CreateGauge(
            "cpu_usage_percentage",
            "Current CPU usage in percentage"
        );

        // Gauge para monitorar o uso de memória
        private static readonly Gauge MemoryUsageGauge = Metrics.CreateGauge(
            "memory_usage_bytes",
            "Current memory usage in bytes"
        );


        // Método para obter o uso da CPU
        private float GetCpuUsage()
        {
            // PerformanceCounter é usado para capturar o uso da CPU
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue(); // A primeira leitura sempre retorna 0, então descartamos a primeira
            System.Threading.Thread.Sleep(1000); // Pausa de 1 segundo para obter um valor correto
            return cpuCounter.NextValue();
        }

        // Método para obter o uso da memória
        private long GetMemoryUsage()
        {
            // Obtém a memória alocada no processo atual
            return Process.GetCurrentProcess().WorkingSet64;
        }

        // Endpoint para expor métricas para o Prometheus
        [HttpGet("metrics")]
        public async Task<IActionResult> GetMetrics()
        {
            // Atualiza a métrica de uso de CPU
            CpuUsageGauge.Set(GetCpuUsage());

            // Atualiza a métrica de uso de memória
            MemoryUsageGauge.Set(GetMemoryUsage());

            // Buffer para armazenar as métricas exportadas
            var metricsBuffer = new StringBuilder();

            // Coleta e exporta as métricas no formato de texto
            await using (var stream = new MemoryStream())
            {
                await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(stream);
                stream.Position = 0; // Resetar a posição do stream para leitura
                using (var reader = new StreamReader(stream))
                {
                    metricsBuffer.Append(await reader.ReadToEndAsync());
                }
            }

            // Retorna o conteúdo como texto/plain
            return Content(metricsBuffer.ToString(), "text/plain");
        }

        // Endpoint para medir latência de requisições HTTP
        [HttpGet("Latencia")]
        public IActionResult GetLatencias()
        {
            // Inicia a medição do tempo
            var stopwatch = Stopwatch.StartNew();

            // Simulação de um processamento que leva tempo
            System.Threading.Thread.Sleep(100);

            // Para a medição do tempo
            stopwatch.Stop();

            // Registra a latência no histograma
            RequestDuration
                .WithLabels(Request.Path, Request.Method)
                .Observe(stopwatch.Elapsed.TotalSeconds);

            return Ok("Latency measured!");
        }


        // Endpoint para verificar o uso da CPU em tempo real
        [HttpGet("CPU")]
        public IActionResult GetCpuUsageMetric()
        {
            var cpuUsage = GetCpuUsage();
            // Atualiza a métrica de uso de CPU
            CpuUsageGauge.Set(cpuUsage);

            return Ok($"Current CPU usage: {cpuUsage}%");
        }

        // Endpoint para verificar o uso de memória em tempo real
        [HttpGet("Memory")]
        public IActionResult GetMemoryUsageMetric()
        {
            var memoryUsage = GetMemoryUsage();
            // Atualiza a métrica de uso de memória
            MemoryUsageGauge.Set(memoryUsage);

            return Ok($"Current memory usage: {memoryUsage / (1024 * 1024)} MB");
        }
    }
}
