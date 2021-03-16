/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.2
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Konya_Hiermayer.Packages.Services.Attributes;
using Microsoft.AspNetCore.Authorization;
using Konya_Hiermayer.Packages.Services.Models;
using AutoMapper;
using Konya_Hiermayer.Packages.BL.Interfaces;
using Business = Konya_Hiermayer.Packages.BL.Entities;
using Microsoft.Extensions.Logging;
using Konya_Hiermayer.Packages.BL.Entities.Exceptions;
using Konya_Hiermayer.Packages.Services.Models.Exceptions;

namespace Konya_Hiermayer.Packages.Services.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class SenderApiController : ControllerBase
    { 
        private readonly IMapper mapper;
        private readonly ISenderLogic senderLogic;
        private readonly ILogger<SenderApiController> logger;

        public SenderApiController(IMapper mapper, ISenderLogic senderLogic, ILogger<SenderApiController> logger)
        {
            this.mapper = mapper;
            this.senderLogic = senderLogic;
            this.logger = logger;
        }
        /// <summary>
        /// Submit a new parcel to the logistics service. 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successfully submitted the new parcel</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/parcel")]
        [ValidateModelState]
        [SwaggerOperation("SubmitParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(NewParcelInfo), description: "Successfully submitted the new parcel")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult SubmitParcel([FromBody]Parcel body)
        { 
            try
            {
                logger.LogInformation($"Submitting new parcel {body}");
                Business.Parcel p = this.mapper.Map<Business.Parcel>(body);
                string trackingId = this.senderLogic.SubmitNewParcel(p);
                return StatusCode(200, trackingId);
            }
            catch (BusinessLayerException e)
            {
                throw new ServiceLayerException("BL Exception", e);
            }
        }
    }
}