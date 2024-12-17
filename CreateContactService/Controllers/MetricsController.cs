using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace CreateContactService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        private static readonly Histogram RequestDuration = Metrics.CreateHistogram(
             "create_contact_request_duration_milliseconds",
             "Histograma de latência do endpoint de create",
             new HistogramConfiguration
             {
                LabelNames = new[] { "path", "method" },
                Buckets = Histogram.LinearBuckets(start: 10, width: 100, count: 10)
             }
        );

        private static readonly Gauge CpuUsageGauge = Metrics.CreateGauge(
            "cpu_usage_percentage_create",
            "Uso da CPU em tempo real em porcentagem"
        );

        private static readonly Gauge MemoryUsageGauge = Metrics.CreateGauge(
            "memory_usage_bytes_create",
            "Uso de memória em tempo real em bytes"
        );

        private static readonly Stopwatch Stopwatch = new();
        private readonly IHttpClientFactory _httpClientFactory;

        public MetricsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<double> MeasureCreateLatency()
        {
            var client = _httpClientFactory.CreateClient();
            var stopwatch = Stopwatch.StartNew();

            var contactData = new
            {
                Name = "Test Name",
                Email = "test@example.com",
                DDD = 11,
                Phone = "123456789"
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(contactData),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("https://localhost:7251/CreateContacts/Create", jsonContent);

            stopwatch.Stop();
            var latency = stopwatch.Elapsed.TotalSeconds;

            RequestDuration.WithLabels("/CreateContacts/Create", "POST").Observe(latency);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Request to /CreateContacts/Create failed with status code {response.StatusCode}");
            }

            return latency * 1000; 
        }

        private float GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return cpuCounter.NextValue();
        }

        private long GetMemoryUsage()
        {
            return Process.GetCurrentProcess().WorkingSet64;
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetMetrics()
        {
            var metricsBuffer = new StringBuilder();

            await using (var stream = new MemoryStream())
            {
                await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(stream);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    metricsBuffer.Append(await reader.ReadToEndAsync());
                }
            }

            return Content(metricsBuffer.ToString(), "text/plain");
        }

        [HttpGet("latency/CreateContact")]
        public async Task<IActionResult> GetCreateContactLatency()
        {
            try
            {
                var latencyInMs = await MeasureCreateLatency();
                return Ok($"CreateContact Latency: {latencyInMs} ms");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao medir a latência: {ex.Message}");
            }
        }

        [HttpGet("CPU")]
        public IActionResult GetCpuUsageMetric()
        {
            var cpuUsage = GetCpuUsage();
            CpuUsageGauge.Set(cpuUsage);

            return Ok($"Uso da CPU: {cpuUsage}%");
        }

        [HttpGet("Memory")]
        public IActionResult GetMemoryUsageMetric()
        {
            var memoryUsage = GetMemoryUsage();
            MemoryUsageGauge.Set(memoryUsage);

            return Ok($"Uso da memória: {memoryUsage / (1024 * 1024)} MB");
        }
    }
}
