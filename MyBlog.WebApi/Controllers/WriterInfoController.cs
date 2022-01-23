using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.WebApi.Utility._MD5;
using MyBlog.WebApi.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;
        public WriterInfoController(IWriterInfoService iWriterInfoService)
        {
            this._iWriterInfoService = iWriterInfoService;
        }


        [HttpPost]
        public async Task<ApiResult> Create(string name,string username,string userpwd)
        {
            //数据校验省略

            WriterInfo writer = new WriterInfo
            {
                Name = name,
                UserName = username,
                //加密密码
                UserPwd = MD5Helper.MD5Encrypt32(userpwd)
            };

            //判断账号是否相同，账号必须是唯一的
            var oldWriter =await _iWriterInfoService.FindAsync(v => v.UserName == username);
            if (oldWriter!=null)
            {
                return ApiResultHelper.Error("账号已经存在");
            }

            bool b = await _iWriterInfoService.CreateAsync(writer);
            if (!b)
            {
                return ApiResultHelper.Error("添加失败");
            }

            return ApiResultHelper.Success(writer);
        }


        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writer = await _iWriterInfoService.FindAsync(id);
            bool b=await _iWriterInfoService.EditAsync(writer);
            if (!b)
            {
                return ApiResultHelper.Error("修改失败");
            }
            return ApiResultHelper.Success("修改成功");
        }


        //[AllowAnonymous]
        //[HttpGet("WriterInfo")]
        //public async Task<ActionResult<ApiResult>> GetWriterInfo()
        //{
        //    var data = await _iWriterInfoService.QueryAsync();
        //    if (data == null)
        //    {
        //        return ApiResultHelper.Error("没有更多的值");
        //    }
        //    return ApiResultHelper.Success(data);
        //}


        [AllowAnonymous]
        [HttpGet("FindWriter")]
        public async Task<ApiResult> FindWriter([FromServices]IMapper iMapper,int id)
        {
            var writer = await _iWriterInfoService.FindAsync(id);
           var writerDTO= iMapper.Map<WriterDTO>(writer);
            return ApiResultHelper.Success(writerDTO);
        }











    }
}
