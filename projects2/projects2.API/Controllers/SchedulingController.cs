
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projects2.DTO;
using projects2.Service;

namespace projects2.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingController : ControllerBase
    {
        private readonly IReportGenerator _reportGenerator;
        private readonly IReportDeliveryConfiguration _reportDeliveryConfiguration;
        private readonly ISchedulingConfigurationToolService _schedulingConfigurationToolService;

        public SchedulingController(IReportGenerator reportGenerator, IReportDeliveryConfiguration reportDeliveryConfiguration, ISchedulingConfigurationToolService schedulingConfigurationToolService)
        {
            _reportGenerator = reportGenerator;
            _reportDeliveryConfiguration = reportDeliveryConfiguration;
            _schedulingConfigurationToolService = schedulingConfigurationToolService;
        }

        [HttpPost("GenerateReport")]
        public async Task<IActionResult> GenerateReport(FileType fileType)
        {
            try
            {
                await _reportGenerator.GenerateReport(fileType);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ValidateDestination")]
        public IActionResult ValidateDestination(ReportDeliveryConfigurationDto deliveryConfiguration)
        {
            bool isValid = _reportDeliveryConfiguration.ValidateDestination(deliveryConfiguration);
            return Ok(isValid);
        }

        [HttpPost("ValidateDeliveryConfiguration")]
        public IActionResult ValidateDeliveryConfiguration(ReportDeliveryConfigurationDto deliveryConfiguration)
        {
            bool isValid = _reportDeliveryConfiguration.ValidateDeliveryConfiguration(deliveryConfiguration);
            return Ok(isValid);
        }

        [HttpGet("Run")]
        public async Task<IActionResult> Run()
        {
            try
            {
                await _schedulingConfigurationToolService.Run();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
