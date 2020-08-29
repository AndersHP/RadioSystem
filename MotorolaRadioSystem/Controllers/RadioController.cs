using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Core.Usecases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace MotorolaRadioSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RadioController : ControllerBase
    {
        private readonly IRadioGod _radioGod;

        public RadioController(IRadioGod radioGod)
        {
            _radioGod = radioGod;
        }

        /// <summary>
        /// Gets the Radio with given Id.
        /// </summary>
        /// <param name="id">Radio Id</param>
        /// <returns>Radio</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Radio>> GetRadio([FromRoute] int id)
        {
            try
            {
                var result = await _radioGod.RetrieveRadio(id, new CancellationToken());
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        /// <summary>
        /// Gets the current location of Radio with given Id.
        /// </summary>
        /// <param name="id">Radio Id</param>
        /// <returns>Location</returns>
        [HttpGet("{id}/location")]
        public async Task<ActionResult<string>> GetRadioLocation([FromRoute] int id)
        {
            try
            {
                var radio = await _radioGod.RetrieveRadio(id, new CancellationToken());

                if (radio.CurrentLocation != null && !string.IsNullOrEmpty(radio.CurrentLocation.Id))
                {
                    return Ok(radio.CurrentLocation.Id);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Posts the Radio with Id.
        /// </summary>
        /// <param name="id">Radio Id</param>
        /// <param name="radioBody">JSON Body containing Alias : Name of Radio and AllowedLocations</param>
        /// <returns>Id of new radio</returns>
        [HttpPost("{id}")]
        public async Task<ActionResult<int>> PostRadio([FromRoute] int id, [FromBody]PostRadioRequest radioBody)
        {
            List<Location> locations = new List<Location>();
            try
            {
                if (radioBody.allowedLocations != null)
                {
                    locations = radioBody.allowedLocations.Select(s => new Location(){Id = s}).ToList();
                }

                var radio = new Radio(){Id = id, Alias = radioBody.alias, AllowedLocations = locations};

                var result = await _radioGod.CreateRadio(radio, new CancellationToken());
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
        }

        public class PostRadioRequest
        {
            public string alias { get; set; }
            public List<string> allowedLocations { get; set; }
        }

        /// <summary>
        /// Update the location of a radio station to one of its allowed locations.
        /// </summary>
        /// <param name="id">Radio Id</param>
        /// <param name="location">New location of radio</param>
        /// <returns>Radio</returns>
        [HttpPost("{id}/location")]
        public async Task<ActionResult> PostRadioLocation([FromRoute] int id, [FromBody] string location)
        {
            try
            {
                var loc = new Location() {Id = location};
                await _radioGod.SetRadioLocation(id, loc, new CancellationToken());
                return Ok();
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Could use Forbid if redirection by logic from IoC is wanted
                return StatusCode(403);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}