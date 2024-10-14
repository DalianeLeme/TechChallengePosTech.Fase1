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
        private static readonly Histogram RequestDuration = Metrics.CreateHistogram(
            "contacts_request_duration_milliseconds",
            "Histograma de latências para cada endpoint de ContactsController",
            new HistogramConfiguration
            {
                LabelNames = new[] { "path", "method" },
                Buckets = Histogram.LinearBuckets(start: 10, width: 100, count: 10)
            }
        );

        private static readonly Gauge CpuUsageGauge = Metrics.CreateGauge(
            "cpu_usage_percentage",
            "Uso da CPU em tempo real em porcentagem"
        );

        private static readonly Gauge MemoryUsageGauge = Metrics.CreateGauge(
            "memory_usage_bytes",
            "Uso de memória em tempo real em bytes"
        );

        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly Stopwatch UptimeStopwatch = Stopwatch.StartNew();

        public MetricasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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

        private async Task<(double, string)> MeasureLatency(string path, string method)
        {
            var client = _httpClientFactory.CreateClient();
            var stopwatch = Stopwatch.StartNew();

            HttpResponseMessage response;

            switch (method)
            {
                case "GET":
                    response = await client.GetAsync(path);
                    break;
                case "POST":
                    response = await client.PostAsync(path, null);
                    break;
                case "PUT":
                    response = await client.PutAsync(path, null);
                    break;
                case "DELETE":
                    response = await client.DeleteAsync(path);
                    break;
                default:
                    throw new ArgumentException("Invalid HTTP method");
            }

            stopwatch.Stop();
            var latency = stopwatch.Elapsed.TotalSeconds;
            var latencyInMs = latency * 1000;
            
            RequestDuration.WithLabels(path, method).Observe(latency);

            var content = await response.Content.ReadAsStringAsync();
            return (latency, content);
        }
         
        [HttpGet("metrics")]
        public async Task<IActionResult> GetMetrics()
        {
            CpuUsageGauge.Set(GetCpuUsage());

            MemoryUsageGauge.Set(GetMemoryUsage());

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
    
        [HttpGet("latency/CreateContact")]
        public async Task<IActionResult> GetCreateContactLatency()
        {
            var (latency, content) = await MeasureLatency("https://localhost:44331/Contacts/Create", "POST");
            var latencyInMs = latency * 1000;
            return Ok($"CreateContact Latency: {latencyInMs} ms\nResponse Content: {content}");
        }

        [HttpGet("latency/GetAllContacts")]
        public async Task<IActionResult> GetGetContactsLatency()
        {
            var (latency, content) = await MeasureLatency("https://localhost:44331/Contacts/GetAllContacts", "GET");
            var latencyInMs = latency * 1000;
            return Ok($"GetContacts Latency: {latencyInMs} ms\nResponse Content: {content}");
        }

        [HttpGet("latency/UpdateContact")]
        public async Task<IActionResult> GetUpdateContactLatency()
        {
            var (latency, content) = await MeasureLatency("https://localhost:44331/Contacts/Update", "PUT");
            var latencyInMs = latency * 1000;
            return Ok($"UpdateContact Latency: {latencyInMs} ms\nResponse Content: {content}");
        }

        [HttpGet("latency/DeleteContact")]
        public async Task<IActionResult> GetDeleteContactLatency()
        {
            var (latency, content) = await MeasureLatency("https://localhost:44331/Contacts/Delete/1", "DELETE");
            var latencyInMs = latency * 1000;
            return Ok($"DeleteContact Latency: {latencyInMs} ms\nResponse Content: {content}");
        }
    }
}
