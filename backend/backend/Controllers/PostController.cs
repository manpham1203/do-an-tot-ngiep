﻿using BLL.Post;
using BO.ViewModels.Post;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private PostBLL postBLL;
        private IWebHostEnvironment iwebHostEnvironment;
        public PostController(IWebHostEnvironment _iwebHostEnvironment)
        {
            postBLL = new PostBLL();
            this.iwebHostEnvironment = _iwebHostEnvironment;
        }
        [NonAction]
        public async Task<bool> SaveFile(IFormFile file, string imgName)
        {
            var imagePath = Path.Combine(iwebHostEnvironment.ContentRootPath, "Photos", imgName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return true;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreatePostVM model)
        {
            try
            {
                var resultFromBLL = await postBLL.Create(model);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                if (resultFromBLL && (model.File != null))
                {
                    var saveFile = await SaveFile(model.File, model.Image);
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] UpdatePostVM model)
        {
            try
            {
                var resultFromBLL = await postBLL.Update(id, model);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                if (resultFromBLL && (model.File != null))
                {
                    var saveFile = await SaveFile(model.File, model.Image);
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();

            }
        }
        [HttpGet("RowsAdminDeleted")]
        public async Task<IActionResult> RowsAdminDeleted(bool deleted, string query, int limit = 10, int currentPage = 1)
        {
            try
            {
                var resultFromBLL = await postBLL.RowsAdminDeleted(deleted, query, limit, currentPage);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();

            }
        }
        [HttpGet("RowAdminById")]
        public async Task<IActionResult> RowAdminById(string id)
        {
            try
            {
                var resultFromBLL = await postBLL.RowAdminById(id);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();

            }
        }
        [HttpPut("published/{id}")]
        public async Task<IActionResult> Published(string id)
        {
            try
            {
                var resultFromBLL = await postBLL.Published(id);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();

            }
        }
        [HttpPut("deleted/{id}")]
        public async Task<IActionResult> Deleted(string id)
        {
            try
            {
                var resultFromBLL = await postBLL.Deleted(id);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();

            }
        }
        [HttpGet("SetDataUpdate")]
        public async Task<IActionResult> SetDataUpdate(string id)
        {
            try
            {
                var resultFromBLL = await postBLL.SetDataUpdate(id);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();

            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var resultFromBLL = await postBLL.DeleteFromDatabase(id);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("postcards")]
        public async Task<IActionResult> PostCards()
        {
            try
            {
                var resultFromBLL = await postBLL.PostCards();
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int i = 0; i < resultFromBLL.Count; i++)
                {
                    resultFromBLL[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase,resultFromBLL[i].Image);
                }

                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
    
        [HttpGet("postdetail")]
        public async Task<IActionResult> PostDetail(string slug)
        {
            try
            {
                var resultFromBLL = await postBLL.PostDetail(slug);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                resultFromBLL.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.Image);
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
