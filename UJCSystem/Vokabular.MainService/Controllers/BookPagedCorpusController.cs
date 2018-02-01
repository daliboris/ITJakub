﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vokabular.MainService.Core.Managers;
using Vokabular.MainService.DataContracts.Contracts.Search;
using Vokabular.RestClient.Errors;
using Vokabular.Shared.DataContracts.Search.Corpus;
using Vokabular.Shared.DataContracts.Search.Request;

namespace Vokabular.MainService.Controllers
{
    [Route("api/[controller]")]
    public class BookPagedCorpusController : Controller
    {
        private readonly BookSearchManager m_bookSearchManager;

        public BookPagedCorpusController(BookSearchManager bookSearchManager)
        {
            m_bookSearchManager = bookSearchManager;
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(CorpusSearchSnapshotsResultContract), StatusCodes.Status200OK)]
        public IActionResult SearchCorpusGetSnapshotListResult([FromBody] CorpusSearchRequestContract request)
        {
            try
            {
                var result = m_bookSearchManager.SearchCorpusGetSnapshotListByCriteria(request);
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (HttpErrorCodeException exception)
            {
                return StatusCode((int)exception.StatusCode, exception.Message);
            }
        }

        [HttpPost("snapshot/{snapshotId}/search")]
        [ProducesResponseType(typeof(List<CorpusSearchResultContract>), StatusCodes.Status200OK)]
        public IActionResult SearchCorpusInSnapshotResult(long snapshotId, [FromBody] CorpusSearchRequestContract request)
        {
            try
            {
                var result = m_bookSearchManager.SearchCorpusInSnapshotByCriteria(snapshotId, request);
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (HttpErrorCodeException exception)
            {
                return StatusCode((int)exception.StatusCode, exception.Message);
            }
        }

        [HttpPost("search-count")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        public IActionResult SearchCorpusTotalResultCount([FromBody] SearchRequestContractBase request)
        {
            try
            {
                var result = m_bookSearchManager.SearchCorpusTotalResultCount(request);
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (HttpErrorCodeException exception)
            {
                return StatusCode((int)exception.StatusCode, exception.Message);
            }
        }

    }
}